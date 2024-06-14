namespace MsNotificaciones.Models
{
    public class EmailModel
    {
        public string DestinationEmail { get; set; }
        public string DestinationName { get; set; }
        public string EmailSubject { get; set; }
        public string NewEventDate { get; set; }
        public string EventImageUrl { get; set; }
        public string EventUrl { get; set; }
    }
}