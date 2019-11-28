
namespace Signawel.Domain.ReportGroups
{
    public class CityReportGroup
    {
        public string CityId { get; set; }
        public City City { get; set; }

        public string ReportGroupId { get; set; }
        public ReportGroup ReportGroup { get; set; }
    }
}
