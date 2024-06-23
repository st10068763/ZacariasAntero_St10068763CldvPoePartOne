using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace CldvPoePartOne
{
    public static class FunctionsClass
    {
        [FunctionName("FunctionsClass_Orchestrator")]
        public static async Task RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context,
            ILogger log)
        {
            try
            {
                // Get product ID from the session
                string productId = context.GetInput<string>();

                // Call inventory update activity
                bool inventoryUpdated = await context.CallActivityAsync<bool>("UpdateInventory", productId);
                if (!inventoryUpdated)
                {
                    log.LogError("Failed to update inventory.");
                    return;
                }

                // Get user email from the session
                string userEmail = context.GetInput<string>();
                string message = "Order processed successfully";

                // Call notification activity
                bool notificationSent = await context.CallActivityAsync<bool>("SendNotification", (userEmail, message));
                if (!notificationSent)
                {
                    log.LogError("Failed to send notification.");
                    return;
                }

                log.LogInformation("Order processing completed successfully.");
            }
            catch (Exception ex)
            {
                log.LogError($"Orchestrator error: {ex.Message}");
                throw;
            }
        }

        [FunctionName("UpdateInventory")]
        public static async Task<bool> UpdateInventoryAsync([ActivityTrigger] string productId, ILogger log)
        {
            try
            {
                // Simulate inventory update
                await Task.Delay(2000); // Simulate delay

                log.LogInformation($"Inventory updated for product {productId}");
                return true;
            }
            catch (Exception ex)
            {
                log.LogError($"Error updating inventory: {ex.Message}");
                return false;
            }
        }

        //// Function to send notification to the user
        //[FunctionName("SendNotification")]
        //public static async Task<bool> SendNotificationAsync(
        //               [ActivityTrigger] (string userEmail, string message) input,
        //                          ILogger log)
        //{
        //    try
        //    {
        //        string userEmail = input.userEmail;
        //        string message = input.message;

        //        // Create a SendGrid message
        //        var msg = new SendGridMessage();
        //        msg.SetFrom(new EmailAddress(""));
        //    }
        //    catch (Exception ex)
        //    {
        //        log.LogError($"Error sending notification: {ex.Message}");
        //        return false;
        //    }
        //}

        //[FunctionName("SendNotification")]
        //public static async Task<bool> SendNotificationAsync(
        //    [ActivityTrigger] (string userEmail, string message) input,
        //    ILogger log,
        //    [SendGrid(ApiKey = SendGridConfiguration.ApiKey)] IAsyncCollector<SendGridMessage> messageCollector)
        //{
        //    try
        //    {
        //        string userEmail = input.userEmail;
        //        string message = input.message;

        //        // Create a SendGrid message
        //        var msg = new SendGridMessage();
        //        msg.SetFrom(new EmailAddress("your-sender-email@example.com", "Your Sender Name"));
        //        msg.AddTo(new EmailAddress(userEmail));
        //        msg.SetSubject("Order Notification");
        //        msg.AddContent(SendGrid.MimeType.Html, message);

        //        // Send the email
        //        await messageCollector.AddAsync(msg);

        //        log.LogInformation($"Notification sent to {userEmail}: {message}");
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        log.LogError($"Error sending notification: {ex.Message}");
        //        return false;
        //    }
        //}
    }
}
