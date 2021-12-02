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
        public DateTime completed_date { get; set; }
        public string content { get; set; }
        public long id { get; set; }
        public object meta_data { get; set; }
        public long project_id { get; set; }
        public long task_id { get; set; }
        public int user_id { get; set; }
    }


}
