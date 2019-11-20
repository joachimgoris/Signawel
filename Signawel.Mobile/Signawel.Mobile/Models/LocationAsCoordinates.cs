using Newtonsoft.Json;

namespace Signawel.Mobile.Models
{
    public class LocationAsCoordinates
    {
        [JsonProperty("Lat_WGS84")]
        public string LatWGS84 { get; set; }
        [JsonProperty("Lon_WGS84")]
        public string LonWGS84 { get; set; }

        public string XLambert72 { get; set; }
        public string YLambert72 { get; set; }
    }


}
