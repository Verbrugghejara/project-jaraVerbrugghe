using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DP_project.Models
{
    
    public class CompletedNotes
    {
        public Item[] items { get; set; }
    }


    public class Item
    {
        [JsonProperty(propertyName: "task_id")]
        public string TaskId { get; set; }
        [JsonProperty(propertyName: "id")]
        public string Id { get; set; }
        [JsonProperty(propertyName: "content")]
        public string Name { get; set; }
        [JsonProperty(propertyName: "project_id")]
        public long ProjectId { get; set; }
        [JsonProperty(propertyName: "completed")]
        public bool Completed { get; set; }
    }


}
