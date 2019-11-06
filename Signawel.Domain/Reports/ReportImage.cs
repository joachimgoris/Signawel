using System;

namespace Signawel.Domain
{
    public class ReportImage
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string ReportId { get; set; }

        public Report Report { get; set; }

        public  string ImagePath { get; set; }
    }
}
