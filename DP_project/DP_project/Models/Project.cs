using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DP_project.Models
{
    public class Project
    {
        [JsonProperty(propertyName: "id")]
        public uint Id { get; set; }
        [JsonProperty(propertyName: "name")]
        public string Name { get; set; }
        [JsonProperty(propertyName: "favorite")]
        public Boolean Favorite { get; set; }
    }
}
