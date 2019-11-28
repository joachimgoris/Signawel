using Newtonsoft.Json;
using System.Collections.Generic;

namespace Signawel.Mobile.Models
{
    public class AddressToCoordinate
    {
        [JsonProperty("LocationResult")]
        public List<LocationResult> locationResult { get; set; }
    }


}
