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
        public string Name 
        {
            get
            {
                Random random = new Random();
                string r = "";
                int i;
                for (i = 1; i < 11; i++)
                {
                    r += random.Next(0, 9).ToString();
                }
                return r;
            }
                
        }
        [JsonProperty(propertyName: "project_id")]
        public uint ProjectId { get; set; }
        [JsonProperty(propertyName: "completed")]
        public bool Completed { get; set; }
    }
}
