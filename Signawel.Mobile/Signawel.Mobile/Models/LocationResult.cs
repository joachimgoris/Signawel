using Newtonsoft.Json;

namespace Signawel.Mobile.Models
{
    public class LocationResult
    {
        public string Municipality { get; set; }
        public string Zipcode { get; set; }
        public string Thoroughfarename { get; set; }
        public string Housenumber { get; set; }
        public string ID { get; set; }
        public string FormattedAddress { get; set; }
        [JsonProperty("Location")]
        public LocationAsCoordinates location { get; set; }
        public string LocationType { get; set; }
    }


}
