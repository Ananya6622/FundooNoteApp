using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class MessageService
    {
        public async Task SendMessageQueue(string Email, string Token)
        {
            ServiceBusClient client = new ServiceBusClient("Endpoint=sb://servicebusfundoo.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=rVhUNHrsopVwqaGutPxFRvCXXGuA2tA+K+ASbMbHzYM=");
            ServiceBusSender sender = client.CreateSender("reset-password");

            

            try
            {
                ServiceBusMessage message = new ServiceBusMessage();
                message.Body = BinaryData.FromString("Token for reset-password:" + Token);
                message.Subject = "fundoo notes reset link:";
                message.To = Email;


                await sender.SendMessageAsync(message);

                //SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                //System.Net.NetworkCredential credential = new System.Net.NetworkCredential("ananyaetti6622@gmail.com", "oiyz onhn fcjd bhro");

                //smtpClient.EnableSsl = true;
                //smtpClient.UseDefaultCredentials = false;
                //smtpClient.Credentials = credential;
                //MailMessage emailMessage = new MailMessage();
                //emailMessage.From = new MailAddress("ananyaetti6622@gmail.com");
                //emailMessage.To.Add(Email);
                //emailMessage.Subject = "FundooNotes reset Link";
                //emailMessage.Body = "This is your token for resetting the password: " + Token;



                //smtpClient.Send(emailMessage);


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message: {ex.Message}");
            }
        }
    }
}
