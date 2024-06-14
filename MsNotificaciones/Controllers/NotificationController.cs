using Microsoft.AspNetCore.Mvc;
using MsNotificaciones.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace MsNotificaciones.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {
        [Route("ModificacionEmail")]
        [HttpPost]
        public async Task<ActionResult> SendEmail(EmailModel data)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("Jhonier.1701814263@ucaldas.edu.co", "Eventos Manizales");
            var to = new EmailAddress(data.DestinationEmail, data.DestinationName);

            var msg = new SendGridMessage();
            msg.SetFrom(from);
            msg.AddTo(to);
            msg.SetTemplateId("d-83efbb9a828b4bafbf9096f73b6e1036");
            msg.SetSubject(data.EmailSubject);

            msg.SetTemplateData(new
            {
                name = data.DestinationName,
                eventImageUrl = data.EventImageUrl,
                newEventDate = data.NewEventDate,
                eventUrl = data.EventUrl,
                emailSubject = data.EmailSubject
            });

            var response = await client.SendEmailAsync(msg);

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                return Ok("Email sent successfully");
            }
            else
            {
                return BadRequest("Error sending email");
            }
        }
    }
}
