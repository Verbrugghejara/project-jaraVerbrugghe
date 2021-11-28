using DP_project.Models;
using DP_project.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DP_project.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SingleProjectPage : ContentPage
    {
        
        public Project MyProject { get; set; }
        public Note MyNote { get; set; }
        public  SingleProjectPage(Project project)
        {
            InitializeComponent();
            MyProject = project;
            ShowSingleProject();
            btnCreate.Clicked += btnCreate_Clicked;
        }


        private async void btnCreate_Clicked(object sender, EventArgs e)
        {
            //uint n = MyProject.Id;
            //uint i = Convert.ToUInt32(n);
            List<Note> tasks = await ToDoRepository.GetTasksAsync(MyProject.Id);
            await Navigation.PushAsync(new CreateTask(tasks[0], MyProject));
            lvwSections.SelectedItem = null;
           
        }

        private async void ShowSingleProject()
        {
            //uint n = MyProject.Id;
            //uint i = Convert.ToUInt32(n);
            List<Project> project = await ToDoRepository.GetProjectByIdAsync(MyProject.Id);
            List<Note> tasks = await ToDoRepository.GetTasksAsync(MyProject.Id);
            lvwSections.ItemsSource = tasks;
            lvwProjectName.ItemsSource = project;
        }

        private void lvwSections_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (lvwSections.SelectedItem != null)
            {
                Note note = (Note)lvwSections.SelectedItem;
                Navigation.PushAsync(new DetailsTaskPage(note, MyProject));
            }
        }

    }
}