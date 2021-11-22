using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DP_project.Models;
using DP_project.Repositories;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DP_project.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OverviewProjects : ContentPage
    {
        public OverviewProjects()
        {
            InitializeComponent();
            ShowProjects();
            TestRepositories();
        }

        private async void TestRepositories()
        {
            //5335833201
            List<Note> tasks = await ToDoRepository.GetTasksAsync("2278575903");
            ////var taskid = tasks[0].Id;
            ////await ToDoListRepository.DeleteTask(taskid);
            ////List<ToDoTask> tasks1 = await ToDoListRepository.GetTasksAsync();

            Debug.WriteLine("Test tasks--------------------------");
            Debug.WriteLine($"De aantal tasks zijn {tasks.Count()}");
            foreach (var itemt in tasks)
            {
                Debug.WriteLine(itemt.Id);
                Debug.WriteLine(itemt.Name);
                Debug.WriteLine(itemt.SectionId);
                Debug.WriteLine("project id");
                Debug.WriteLine(itemt.ProjectId);
            }

            List<Project> projects = await ToDoRepository.GetProjectsAsync();

            //Projects project1 = projects[4];
            //project1.Name = "project nr 7";
            //await ToDoListRepository.CreateProject(project1);


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
            ////ToDoTask toDoTask = tasks[12];
            ////toDoTask.Name = "Veranderd";
            ////await ToDoListRepository.UpdateTask(toDoTask);

            ////ToDoTask task1 = tasks[0];
            ////task1.Name = "Eindelijk het werkt";
            ////await ToDoListRepository.CreateTask(task1);

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
            lvwProjects.ItemsSource = projects;
        }

        private void lvwProjects_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (lvwProjects.SelectedItem != null)
            {
                Project project = (Project)lvwProjects.SelectedItem;
                Navigation.PushAsync(new SingleProjectPage(project));
                lvwProjects.SelectedItem = null;

            }

        }
    }



}

