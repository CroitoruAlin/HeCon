using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HeCon_webapp.Models
{
    public class RequestObject
    {
        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }
    }
}