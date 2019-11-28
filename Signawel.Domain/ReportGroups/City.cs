using System.Collections.Generic;

namespace Signawel.Domain.ReportGroups
{
    public class City : Entity
    {
        public string Name { get; set; }
        public List<CityReportGroup> CityReportGroups { get; set; }

        public City()
        {

        }

        public City(string name)
        {
            this.Name = name;
        }
    }
}