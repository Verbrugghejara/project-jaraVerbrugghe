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
    public partial class DetailsTaskPage : ContentPage
    {
        public Item MyNote { get; set; }
        public Project MyProject { get; set; }
        public DetailsTaskPage(Item note, Project project)
        {
            InitializeComponent();
            btnSave.Clicked += BtnSave_Clicked;
            btnDelete.Clicked += BtnDelete_Clicked;
            MyNote = note;
            MyProject = project;
            ShowNameTask();
        }

        private async void ShowNameTask()
        {
            List<Item> tasks = await ToDoRepository.GetTasksByIDAsync(MyNote.Id);
            lvwTaskName.ItemsSource = tasks;
        }

        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {

                await ToDoRepository.DeleteTask(MyNote.Id);
                await Navigation.PushAsync(new SingleProjectPage(MyProject));
        }

        private async void BtnSave_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(entContent.Text))
            {
                string content = entContent.Text;

                Item task1 = MyNote;
                task1.Name = content;
                await ToDoRepository.UpdateTask(task1);
                await Navigation.PushAsync(new SingleProjectPage(MyProject));
            }
        }


    }
}