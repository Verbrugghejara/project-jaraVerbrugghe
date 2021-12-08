using DP_project.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        //------------------------------------------------ TASKS -------------------------------------------------
        public static async Task<List<Item>> GetTasksByProjectIdAsync(uint id)

        {
            List<Item> lists = new List<Item>();
            using (HttpClient client = GetClient())
            {
                try
                {
                    string url = AddTopic("tasks");
                    string json = await client.GetStringAsync(url);
                    if (json != null)
                    {
                        //var o = JObject.Parse(json);
                        var jsonString = JsonConvert.DeserializeObject<List<Item>>(json);
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
        public static async Task<List<Item>> GetTasksByIDAsync(string id)
        {
            using (HttpClient client = GetClient())
            {
                string url = AddTopic2("tasks", id);
                string json = await client.GetStringAsync(url);
                if (json != null)
                {
                    //var o = JObject.Parse(json);
                    return JsonConvert.DeserializeObject<List<Item>>("[" + json + "]");
                }
                return null;

            }
        }
        public static async Task UpdateTask(Item toDoTask)
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
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }
        public static async Task CreateTask(Item toDoTask)
        {
            using (HttpClient client = GetClient())
            {
                try
                {
                    string url = AddTopic("tasks");


                    string json = JsonConvert.SerializeObject(toDoTask); //serialize is om van variabele naar een string te gaan
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, content);

                    
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
                        var jsonString = JsonConvert.DeserializeObject<List<CompletedNotes>>(newJson);
                        foreach (var itemt in jsonString)
                        {
                            foreach (var item in itemt.items)
                            {
                                if (item.ProjectId == project_id)
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
        public static async Task<List<Item>> GetAllTasksAsync(uint project_id)

        {
            using (HttpClient client = GetClient())
            {
                List<String> ids = new List<String>();
                List<Item> newList = new List<Item>();
                List<Item> cList = await GetTasksCompletedByProjectIdAsync(project_id);
                List<Item> List = await GetTasksByProjectIdAsync(project_id);
                var list = cList.Concat(List);

                foreach (var itemt in list)
                {
                    if (itemt.TaskId == null)
                    {
                        //List<Item> tasks1 = await GetTasksByIDAsync(itemt.TaskId);
                        ids.Add(itemt.Id);
                    }
                    else
                    {
                        ids.Add(itemt.TaskId);
                    }

                }
                foreach (var item in ids)
                {
                    List<Item> tasks = await GetTasksByIDAsync(item);
                    foreach (var o in tasks)
                    {
                        newList.Add(o);
                    }
                }
                    
                return newList;
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

                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }
        public static async Task<List<Project>> GetProjectsWithPercentageAsync(int number)
        {
            using (HttpClient client = GetClient())
            {
                try
                {
                    List<Project> newprojects = new List<Project>();
                    List<Project> projects = await GetProjectsAsync();
                    foreach (var item in projects)
                    {
                        List<Item> tasks = await ToDoRepository.GetTasksCompletedByProjectIdAsync(item.Id);

                        List<Item> items = await ToDoRepository.GetTasksByProjectIdAsync(item.Id);
                        int alltasks = item.CountofTasks = items.Count + tasks.Count;
                        int complete = item.CountofComplete = tasks.Count;
                        double percentage = 0.0;
                        int minNumber=0;
                        int maxNumber=0;
                        if (complete!=0)
                        {
                            percentage = (double)complete/ (double)alltasks *100;
                        }
                        if (number == 0)
                        {
                            minNumber = number;
                            maxNumber = number+25;
                        }
                        if (number == 25)
                        {
                            minNumber = number;
                            maxNumber = number + 24;
                        }
                        if (number == 50)
                        {
                            minNumber = number;
                            maxNumber = number + 24;
                        }
                        if (number == 75)
                        {
                            minNumber = number;
                            maxNumber = number + 24;
                        }
                        if (number == 100)
                        {
                            minNumber = number;
                            maxNumber = number;
                        }

                        if (minNumber <= percentage && maxNumber >= percentage )
                        {
                            newprojects.Add(item);
                        }
                    }


                    return newprojects;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }
       
    }
}
