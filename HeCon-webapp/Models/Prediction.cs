using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HeCon_webapp.Models
{
    public class Prediction
    {
        [JsonProperty(PropertyName = "positive_probability")]
        public double positive_probability { get; set; }

        [JsonProperty(PropertyName = "negative_probability")]
        public double negative_probability { get; set; }


    }
}