namespace EmailAAPI.Models
{
    public class EmailConfiguration
    {
        
        public string SmtpServer { get; set; }

        
        public string SmtpUsername { get; set; }

       
        public string SmtpPassword { get; set; }

        
        public int SmtpPort { get; set; }

        
        public bool SmtpUseSSL { get; set; }
    }
}
