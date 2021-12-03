using DP_project.Models;
using DP_project.Repositories;
using DP_project.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DP_project
{
    public partial class App : Application
    {
        public Project MyProject { get; set; }
        public App()
        {
            InitializeComponent();

            // MainPage = new MainPage();
            MainPage = new NavigationPage(new OverviewProjects())
            {
                BarBackgroundColor = Color.FromHex("#01B3F6"),
                BarTextColor = Color.Black
            };
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
