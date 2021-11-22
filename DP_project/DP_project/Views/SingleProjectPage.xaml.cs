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
        public SingleProjectPage(Project project)
        {
            InitializeComponent();
            MyProject = project;
            ShowSingleProject();
        }

        private async void ShowSingleProject()
        {
            List<Note> tasks = await ToDoRepository.GetTasksAsync(MyProject.Id);
            lvwSections.ItemsSource = tasks;
        }
    }
}