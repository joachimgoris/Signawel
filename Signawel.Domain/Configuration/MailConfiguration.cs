namespace Signawel.Domain.Configuration
{
    public class MailConfiguration
    {
        public string Host { get; set; }

        public string Sender { get; set; }

        public string SenderName { get; set; }

        public string Password { get; set; }

        public int Port { get; set; }

        public string FrontEndUrl { get; set; }
    }
}
