using Microsoft.AspNetCore.Identity.UI.Services;

namespace ASM2_AppDev.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //logic gửi mail
            return Task.CompletedTask;
        }
    }
}
