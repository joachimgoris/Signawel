
namespace Signawel.Domain.Reports
{
    public class ReportImage : Entity
    {
        public string ReportId { get; set; }

        public Report Report { get; set; }

        public string ImageId { get; set; }
        public Image Image { get; set; }
    }
}
