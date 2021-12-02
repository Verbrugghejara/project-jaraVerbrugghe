using DP_project.Models;
using DP_project.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Item task = new Item();
            await Navigation.PushAsync(new CreateTask(task, MyProject));
            lvwSections.SelectedItem = null;
           
        }

        private async void ShowSingleProject()
        {
            //uint n = MyProject.Id;
            //uint i = Convert.ToUInt32(n);
            
            List<Project> project = await ToDoRepository.GetProjectByIdAsync(MyProject.Id);
            List<Item> items = await ToDoRepository.GetTasksCompletedByProjectIdAsync(MyProject.Id);
            List<Item> tasks = await ToDoRepository.GetTasksByProjectIdAsync(MyProject.Id);
            lvwSections.ItemsSource = tasks;
            lvwSections.ItemsSource = items;
            lvwProjectName.ItemsSource = project;
        }

        private void lvwSections_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (lvwSections.SelectedItem != null)
            {
                Item note = (Item)lvwSections.SelectedItem;
                Navigation.PushAsync(new DetailsTaskPage(note, MyProject));
            }
        }

        private async void Checkbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var cb = (CheckBox)sender;
            var item = (Item)cb.BindingContext;
            var id = item.Id;
            if (cb.IsChecked == true)
            {
                
                await ToDoRepository.TaskCloseAsync(id);
                Debug.WriteLine("true");
            }
            Debug.WriteLine("false");
        }
    }
}