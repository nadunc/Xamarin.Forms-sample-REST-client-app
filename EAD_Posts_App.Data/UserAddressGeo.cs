using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAD_Posts_App.Data
{
    public class UserAddressGeo
    {
        [JsonProperty(PropertyName = "lat")]
        public double? Latitude { get; set; }

        [JsonProperty(PropertyName = "lng")]
        public double? Longitude { get; set; }
    }
}
