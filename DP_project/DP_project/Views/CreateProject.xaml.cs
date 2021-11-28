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
    public partial class CreateProject : ContentPage
    {
        public CreateProject()
        {
            InitializeComponent();
            btnCreate.Clicked += BtnCreate_Clicked;
        }

        private async void BtnCreate_Clicked(object sender, EventArgs e)
        {
            string content = entContent.Text;
            Debug.WriteLine("hoi");
            Project project = new Project();
            project.Name = content;
            uint vOut = Convert.ToUInt32(GenerateNumber());
            project.Id = vOut;
            await ToDoRepository.CreateProject(project);
            await Navigation.PushAsync(new OverviewProjects());
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
    }
}