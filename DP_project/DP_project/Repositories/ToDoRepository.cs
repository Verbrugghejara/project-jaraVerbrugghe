using DP_project.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DP_project.Repositories
{
    public static class ToDoRepository
    {

        private const string _APIKEY = "e09b016182b27a22bfbe62714993cbb76e7ce042";
        private const string _URL = "https://api.todoist.com/rest/v1/";





        private static HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            var reuqestId = Guid.NewGuid();
            client.DefaultRequestHeaders.Add("X-Request-Id", reuqestId.ToString());
            client.DefaultRequestHeaders.Add("accept", "application/json");
            //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer" + " " + _APIKEY);


            //client.DefaultRequestHeaders.Add("X-Request-Id", myuuid.ToString());
            return client;

        }
        private static string AddTopic(string topic)
        {
            return $"{_URL}{topic}";
        }
        private static string AddTopic2(string topic, string id)
        {
            return $"{_URL}{topic}/{id}";
        }
        private static string AddTopic3(string topic, uint id)
        {
            return $"{_URL}{topic}/{id}";
        }
        //private static string AddTopicWithId(string topic, string id)
        //{
        //    return $"{_URL}{topic}?project_id={id}";
        //}

        //------------------------------------------------ TASKS -------------------------------------------------
        public static async Task<List<Note>> GetAllTasksAsync()

        {
            using (HttpClient client = GetClient())
            {
                try
                {
                    string url = AddTopic("tasks");
                    string json = await client.GetStringAsync(url);
                    if (json != null)
                    {
                        //var o = JObject.Parse(json);
                        Debug.WriteLine("klaar");
                        var jsonString = JsonConvert.DeserializeObject<List<Note>>(json);
                       


                    }
                    return null;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        public static async Task<List<Note>> GetTasksByProjectIdAsync(uint id)

        {
            List<Note> lists = new List<Note>();
            using (HttpClient client = GetClient())
            {
                try
                {
                    string url = AddTopic("tasks");
                    string json = await client.GetStringAsync(url);
                    if (json != null)
                    {
                        //var o = JObject.Parse(json);
                        Debug.WriteLine("klaar");
                        var jsonString = JsonConvert.DeserializeObject<List<Note>>(json);
                        foreach (var itemt in jsonString)
                        {
                            if (itemt.ProjectId == id)
                            {
                                lists.Add(itemt);
                            }
                        }


                    }
                    return lists;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }
        public static async Task<List<Note>> GetTasksByIDAsync(string id)
        {
            using (HttpClient client = GetClient())
            {
                try
                {
                    string url = AddTopic2("tasks", id);
                    string json = await client.GetStringAsync(url);
                    if (json != null)
                    {
                        //var o = JObject.Parse(json);
                        return JsonConvert.DeserializeObject<List<Note>>("[" + json + "]");
                    }
                    return null;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }
        public static async Task UpdateTask(Note toDoTask)
        {
            using (HttpClient client = GetClient())
            {
                try
                {
                    string url = AddTopic2("tasks", toDoTask.Id);


                    string json = JsonConvert.SerializeObject(toDoTask); //serialize is om van variabele naar een string te gaan
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, content);

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("Something went wrong with the update of task");
                    }
                    Debug.WriteLine("gelukt");
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }
        public static async Task CreateTask(Note toDoTask)
        {
            using (HttpClient client = GetClient())
            {
                try
                {
                    string url = AddTopic("tasks");


                    string json = JsonConvert.SerializeObject(toDoTask); //serialize is om van variabele naar een string te gaan
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, content);

                    if (!response.IsSuccessStatusCode)
                    {
                        var result = response.StatusCode.ToString();
                        Debug.WriteLine(result);
                    }
                    Debug.WriteLine("gelukt");
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }
        public static async Task DeleteTask(string id)
        {
            using (HttpClient client = GetClient())
            {
                try
                {
                    string url = AddTopic2("tasks", id);

                    var response = await client.DeleteAsync(url);

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("Something went wrong with the update of ToDoList");
                    }
                    Debug.WriteLine("gelukt");
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }
        public static async Task<List<Note>> GetTasksCompletedAsync()

        {
            using (HttpClient client = GetClient())
            {
                try
                {
                    string url = "https://api.todoist.com/sync/v8/completed/get_all";
                    string json = await client.GetStringAsync(url);
                    if (json != null)
                    {
                        //var o = JObject.Parse(json);
                        Debug.WriteLine("klaar");
                        var jsonString = JsonConvert.DeserializeObject<List<Note>>(json);



                    }
                    return null;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }
        public static async Task<List<Item>> GetTasksCompletedByProjectIdAsync(uint project_id)

        {
            using (HttpClient client = GetClient())
            {
                try
                {
                    List<Item> lists = new List<Item>();
                    string url = "https://api.todoist.com/sync/v8/completed/get_all";
                    string json = await client.GetStringAsync(url);
                    string newJson = "[" + json + "]";


                    if (json != null)
                    {
                        //var o = JObject.Parse(json);
                        Debug.WriteLine("klaar");
                        var jsonString = JsonConvert.DeserializeObject<List<CompletedNotes>>(newJson);
                        foreach (var itemt in jsonString)
                        {
                            foreach (var item in itemt.items)
                            {
                                if (item.project_id == project_id)
                                {
                                    lists.Add(item);
                                }
                            }


                        }


                    }
                    return lists;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        public static async Task TaskCloseAsync(string id)
        {
            using (HttpClient client = GetClient())
            {
                try
                {
                    string url = AddTopic2("tasks", id);


                    string json = JsonConvert.SerializeObject(id); //serialize is om van variabele naar een string te gaan
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url + "/close", null);

                    if (!response.IsSuccessStatusCode)
                    {
                        var result = response.StatusCode.ToString();
                        Debug.WriteLine(result);
                    }
                    Debug.WriteLine("gelukt");
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }
        public static async Task TaskReopenAsync(string id)
        {
            using (HttpClient client = GetClient())
            {
                try
                {
                    string url = AddTopic2("tasks", id);


                    string json = JsonConvert.SerializeObject(id); //serialize is om van variabele naar een string te gaan
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url + "/reopen", null);

                    if (!response.IsSuccessStatusCode)
                    {
                        var result = response.StatusCode.ToString();
                        Debug.WriteLine(result);
                    }
                    Debug.WriteLine("gelukt");
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        //------------------------------------------------ PROJECTS -------------------------------------------------
        public static async Task<List<Project>> GetProjectsAsync()
        {
            using (HttpClient client = GetClient())
            {
                try
                {
                    string url = AddTopic("projects");
                    string json = await client.GetStringAsync(url);
                    if (json != null)
                    {
                        //var o = JObject.Parse(json);
                        return JsonConvert.DeserializeObject<List<Project>>(json);
                    }
                    return null;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }
        public static async Task<List<Project>> GetProjectByIdAsync(uint id)
        {
            using (HttpClient client = GetClient())
            {
                try
                {
                    string url = AddTopic3("projects", id);
                    string json = await client.GetStringAsync(url);
                    if (json != null)
                    {
                        //var o = JObject.Parse(json);
                        return JsonConvert.DeserializeObject<List<Project>>("[" + json + "]");
                    }
                    return null;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }
        public static async Task UpdateProject(Project project)
        {
            using (HttpClient client = GetClient())
            {
                try
                {

                    //int n = project.Id;
                    //uint i = Convert.ToUInt32(n);
                    string url = AddTopic3("projects", project.Id);


                    string json = JsonConvert.SerializeObject(project); //serialize is om van variabele naar een string te gaan
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, content);

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("Something went wrong with the update of ToDoList");
                    }
                    Debug.WriteLine("gelukt");
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }
        public static async Task CreateProject(Project project)
        {
            using (HttpClient client = GetClient())
            {
                try
                {
                    string url = AddTopic("projects");


                    string json = JsonConvert.SerializeObject(project); //serialize is om van variabele naar een string te gaan
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, content);

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("Something went wrong with the update of ToDoList");
                    }
                    Debug.WriteLine("gelukt");
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }
        public static async Task DeleteProject(string id)
        {
            using (HttpClient client = GetClient())
            {
                try
                {
                    string url = AddTopic2("projects", id);

                    var response = await client.DeleteAsync(url);

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("Something went wrong with the update of ToDoList");
                    }
                    Debug.WriteLine("gelukt");
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        //------------------------------------------------ SECTION -------------------------------------------------

        //public static async Task<List<Section>> GetAllSectionsAsync(string id)
        //{
        //    using (HttpClient client = GetClient())
        //    {
        //        try
        //        {
        //            string url = AddTopicWithId("sections",id);
        //            string json = await client.GetStringAsync(url);
        //            if (json != null)
        //            {
        //                //var o = JObject.Parse(json);
        //                return JsonConvert.DeserializeObject<List<Section>>(json);
        //            }
        //            return null;
        //        }
        //        catch (Exception ex)
        //        {

        //            throw ex;
        //        }
        //    }
        //}
        //public static async Task<List<Section>> GetSectiontByIdAsync(string id)
        //{
        //    using (HttpClient client = GetClient())
        //    {
        //        try
        //        {
        //            string url = AddTopic2("sections", id);
        //            string json = await client.GetStringAsync(url);
        //            if (json != null)
        //            {
        //                //var o = JObject.Parse(json);
        //                return JsonConvert.DeserializeObject<List<Section>>("[" + json + "]");
        //            }
        //            return null;
        //        }
        //        catch (Exception ex)
        //        {

        //            throw ex;
        //        }
        //    }
        //}
        //public static async Task UpdateSection(Section section)
        //{
        //    using (HttpClient client = GetClient())
        //    {
        //        try
        //        {
        //            string url = AddTopic2("sections", section.Id);


        //            string json = JsonConvert.SerializeObject(section); //serialize is om van variabele naar een string te gaan
        //            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
        //            var response = await client.PostAsync(url, content);

        //            if (!response.IsSuccessStatusCode)
        //            {
        //                throw new Exception("Something went wrong with the update of ToDoList");
        //            }
        //            Debug.WriteLine("gelukt");
        //        }
        //        catch (Exception ex)
        //        {

        //            throw ex;
        //        }
        //    }
        //}
        //public static async Task CreateSection(Section section)
        //{
        //    using (HttpClient client = GetClient())
        //    {
        //        try
        //        {
        //            string url = AddTopic("sections");


        //            string json = JsonConvert.SerializeObject(section); //serialize is om van variabele naar een string te gaan
        //            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
        //            var response = await client.PostAsync(url, content);

        //            if (!response.IsSuccessStatusCode)
        //            {
        //                throw new Exception("Something went wrong with the create of section ");
        //            }
        //            Debug.WriteLine("gelukt");
        //        }
        //        catch (Exception ex)
        //        {

        //            throw ex;
        //        }
        //    }
        //}
        //public static async Task DeleteSection(string id)
        //{
        //    using (HttpClient client = GetClient())
        //    {
        //        try
        //        {
        //            string url = AddTopic2("Sections", id);

        //            var response = await client.DeleteAsync(url);

        //            if (!response.IsSuccessStatusCode)
        //            {
        //                throw new Exception("Something went wrong with the update of ToDoList");
        //            }
        //            Debug.WriteLine("gelukt");
        //        }
        //        catch (Exception ex)
        //        {

        //            throw ex;
        //        }
        //    }
        //}

    }
}
