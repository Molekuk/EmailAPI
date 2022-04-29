using System;

namespace EmailAPI.Models
{
    public class SentMessage
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Recipient { get; set; }
        public string FailedMessage { get; set; }
        public string EmailSent { get; set; }
        public string Result { get; set; }
    }
}
