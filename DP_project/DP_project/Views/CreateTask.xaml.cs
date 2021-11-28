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
    public partial class CreateTask : ContentPage
    {
        public Note MyNote { get; set; }
        public Project MyProject { get; set; }
        public CreateTask(Note note, Project project)
        {
            InitializeComponent();
            btnCreate.Clicked += BtnCreate_Clicked;
            MyNote = note;
            MyProject = project;
        }

        private async void BtnCreate_Clicked(object sender, EventArgs e)
        {
            string content = entContent.Text;
            Debug.WriteLine(MyNote.ProjectId);
            Debug.WriteLine("hoi");
            Note note = MyNote;
            note.Name = content;
            await ToDoRepository.CreateTask(note);
            await Navigation.PushAsync(new SingleProjectPage(MyProject));
        }
    }
}