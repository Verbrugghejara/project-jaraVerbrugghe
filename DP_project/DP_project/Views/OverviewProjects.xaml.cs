using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DP_project.Models;
using DP_project.Repositories;
using Microcharts;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DP_project.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OverviewProjects : ContentPage
    {

        public List<Project> MyProject { get; set; }
        public OverviewProjects(List<Project> projects)
        {
            InitializeComponent();
            ShowProjects();
            TestRepositories();
            btnCreate.Clicked += btnCreate_Clicked;
            btnGraph.Clicked += btnGraph_Clicked;
            ShowPicker();
            MyProject = projects;
        }

        private void ShowPicker()
        {
            var percentages = new List<string>();
            percentages.Add("0%");
            percentages.Add("25%");
            percentages.Add("50%");
            percentages.Add("75%");
            percentages.Add("100%");
            var picker = new Picker { Title = "Select a percentage" };
            picker.ItemsSource = percentages;
        }

        private async void btnGraph_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GraphPage());

        }

        private async void btnCreate_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateProject());
            lvwProjects.SelectedItem = null;
        }

        public string GenerateNumber()
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
        private async void TestRepositories()
        {

            //await ToDoRepository.TaskCloseAsync("5369881243");
            //5335833201
            //List<Item> tasks = await ToDoRepository.GetAllTasksAsync(2278576258);
            //foreach (var itemt in tasks)
            //{
            //    Debug.WriteLine(itemt.Id);
            //    Debug.WriteLine(itemt.Name);
            //    Debug.WriteLine("project id");
            //    Debug.WriteLine(itemt.ProjectId);
            //    Debug.WriteLine(itemt.Completed);
            //}
            List<Project> projects = await ToDoRepository.GetProjectsAsync();
            foreach (var project in projects)
            {

                Debug.WriteLine(project.CountofTasks);

            }
            //////var taskid = tasks[0].Id;
            //////await ToDoListRepository.DeleteTask(taskid);
            //////List<ToDoTask> tasks1 = await ToDoListRepository.GetTasksAsync();

            //List<Item> taskss = await ToDoRepository.GetTasksByProjectIdAsync(2278576258);
            //foreach (var itemt in taskss)
            //{
            //    Debug.WriteLine(itemt.Id);
            //    Debug.WriteLine(itemt.Name);
            //    Debug.WriteLine("project id");
            //    Debug.WriteLine(itemt.ProjectId);
            //}
            Debug.WriteLine("Test tasks--------------------------");
            //Debug.WriteLine($"De aantal tasks zijn {tasks.Count()}");
            //foreach (var itemt in tasks)
            //{
            //    Debug.WriteLine(itemt.Id);
            //    Debug.WriteLine(itemt.Name);
            //    Debug.WriteLine("project id");
            //    Debug.WriteLine(itemt.ProjectId);
            //    Debug.WriteLine(itemt.Completed);
            //}

            //List<Project> projects = await ToDoRepository.GetProjectsAsync();

            //Project project1 = projects[2];
            //project1.Id = GenerateNumber().ToString();
            //project1.Name = "laatste project";
            //await ToDoRepository.CreateProject(project1);


            //var projectid = projects[1].Id;
            //await ToDoListRepository.DeleteProject(projectid);
            //List<Projects> projects1 = await ToDoListRepository.GetProjectsAsync();

            //Debug.WriteLine("Test projects--------------------");
            //Debug.WriteLine($"De aantal projects zijn {projects.Count()}");
            //foreach (var itemp in projects)
            //{
            //    Debug.WriteLine(itemp.Id);
            //    Debug.WriteLine(itemp.Name);
            //}
            ////Projects project = projects[3];
            ////project.Name = "Update";
            ////await ToDoListRepository.UpdateProject(project);


            //Debug.WriteLine("Test projects by id-------------------");
            //List<Projects> projectsByID = await ToDoListRepository.GetProjectByIdAsync(projects[0].Id);
            //foreach (var item in projectsByID)
            //{
            //    Debug.WriteLine(item.Id);
            //    Debug.WriteLine(item.Name);
            //}
            //Note toDoTask = tasks[1];
            //toDoTask.Name = "note veranderd";
            //await ToDoRepository.UpdateTask(toDoTask);
            ///
            //Note note = new Note();
            //note.Id = GenerateNumber().ToString();
            //note.Name = "start";
            //note.ProjectId = 2278576258;
            //await ToDoRepository.CreateTask(note);

            //Debug.WriteLine("Test task by id------------------------");
            //List<ToDoTask> task = await ToDoListRepository.GetTasksByIDAsync(tasks[0].Id);
            //foreach (var itemt in task)
            //{
            //    Debug.WriteLine(itemt.Id);
            //    Debug.WriteLine(itemt.Name);
            //}

            //Debug.WriteLine("Test create sectie by id------------------------");
            //List<Section> sections = await ToDoListRepository.GetAllSectionsAsync("2278576258");
            //Debug.WriteLine($"De aantal sections zijn {sections.Count()}");


            //Section newSection = sections[0];
            //newSection.Name = "2de sectie";
            //await ToDoListRepository.CreateSection(newSection);
            //List<Section> sections2 = await ToDoListRepository.GetAllSectionsAsync("2278576258");

            //await ToDoListRepository.DeleteSection(sections2[1].Id);
            //Section updateSectie = sections2[1];
            //updateSectie.Name = "update sectie";
            //await ToDoListRepository.UpdateSection(updateSectie);
            //List<Section> sections1 = await ToDoListRepository.GetAllSectionsAsync("2278576258");
            //Debug.WriteLine($"De aantal secties zijn {sections1.Count()}");

            //Debug.WriteLine("Test sections ------------------------");
            //foreach (var itemt in sections1)
            //{
            //    Debug.WriteLine(itemt.Id);
            //    Debug.WriteLine(itemt.Name);
            //}
            //Debug.WriteLine("Test section by id------------------------");
            //List<Section> sectionId = await ToDoListRepository.GetSectiontByIdAsync(sections1[0].Id);
            //foreach (var itemt in sectionId)
            //{
            //    Debug.WriteLine(itemt.Id);
            //    Debug.WriteLine(itemt.Name);
            //}
        }

        private async void ShowProjects()
        {
            List<Project> projects = await ToDoRepository.GetProjectsAsync();
            foreach (var project in projects)
            {
                List<Item> tasks = await ToDoRepository.GetTasksCompletedByProjectIdAsync(project.Id);
                
                    List<Item> items = await ToDoRepository.GetTasksByProjectIdAsync(project.Id);
                project.CountofTasks = items.Count+tasks.Count;
                project.CountofComplete = tasks.Count;

                
            }
            if (this.MyProject.Count==0)
            {

                lvwProjects.ItemsSource = projects;
            }
            else
            {
                lvwProjects.ItemsSource = MyProject;
            }
        }

        private async void lvwProjects_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (lvwProjects.SelectedItem != null)
            {
                Project project = (Project)lvwProjects.SelectedItem;
                await Navigation.PushAsync(new SingleProjectPage(project));
                lvwProjects.SelectedItem = null;

            }

        }
        private async void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            string selectedIndex = picker.SelectedItem.ToString();
            string result = selectedIndex.Remove(selectedIndex.Length - 1);
            int cijfer = Int32.Parse(result);
            Console.WriteLine(cijfer); 
            
            List<Project> projectss = await ToDoRepository.GetProjectsWithPercentageAsync(cijfer);
            MyProject = projectss;
            await Navigation.PushAsync(new OverviewProjects(projectss));
        }
    }



}

