using System;
using System.Collections.Generic;
using System.Text;

namespace Signawel.Domain.Configuration
{
    public class MailConfiguration
    {
        public string Host { get; set; }
        public string Sender { get; set; }

        public string Password { get; set; }
        public int Port { get; set; }
    }
}
