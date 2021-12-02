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
        public int CountofTasks { get; set; }
        public int CountofComplete { get; set; }
        public int CountofIncomplete
        {
            get
            {
                if (CountofTasks == 0)
                {
                    CountofTasks = 1;
                }
                return CountofTasks - CountofComplete; }
        }
    }
}
