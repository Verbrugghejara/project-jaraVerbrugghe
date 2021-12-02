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
        [JsonProperty(propertyName: "id")]
        public string Id { get; set; }
        [JsonExtensionData]
        private Dictionary<String, JToken> dict_extra_json_data = new Dictionary<string, JToken>();

        [OnDeserialized]
        private void ProcessExtraData(StreamingContext context)
        {

            JToken gezocht = dict_extra_json_data["items"];
            Id = (String)gezocht.SelectToken("id");


        }
        [JsonProperty(propertyName: "content")]
        public string Name { get; set; }

        [OnDeserialized]
        private void ProcessExtraData2(StreamingContext context)
        {

            JToken gezocht = dict_extra_json_data["items"];
            Name = (String)gezocht.SelectToken("content");


        }
        [JsonProperty(propertyName: "project_id")]
        public uint ProjectId { get; set; }

        [OnDeserialized]
        private void ProcessExtraData3(StreamingContext context)
        {

            JToken gezocht = dict_extra_json_data["items"];
            ProjectId = (uint)gezocht.SelectToken("project_id");


        }
    }
}
