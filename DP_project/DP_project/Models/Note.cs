using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DP_project.Models
{
    public class Note
    {
        [JsonProperty(propertyName: "id")]
        public string Id { get; set; }
        [JsonProperty(propertyName: "content")]
        public string Name { get; set; }
        [JsonProperty(propertyName: "project_id")]
        public uint ProjectId { get; set; }
    }
}
