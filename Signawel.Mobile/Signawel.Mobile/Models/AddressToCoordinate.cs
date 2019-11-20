using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Signawel.Mobile.Models
{
    public class AddressToCoordinate
    {
        [JsonProperty("LocationResult")]
        public List<LocationResult> locationResult { get; set; }
    }


}
