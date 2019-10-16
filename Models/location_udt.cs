using System;
using Newtonsoft.Json;

namespace getting_started_with_apollo_csharp.Models
{
    public class location_udt
    {
        [JsonProperty(PropertyName = "x_coordinate")]
        public double X_Coordinate { get; set; }
        [JsonProperty(PropertyName = "y_coordinate")]
        public double Y_Coordinate { get; set; }        
        [JsonProperty(PropertyName = "z_coordinate")]
        public double Z_Coordinate { get; set; }
    }

}