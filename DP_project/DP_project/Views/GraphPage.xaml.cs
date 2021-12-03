using DP_project.Models;
using DP_project.Repositories;
using Microcharts;
using SkiaSharp;
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
    public partial class GraphPage : ContentPage
    {
        List<AreaOdersCount> orderCounts;
        public GraphPage()
        {
            InitializeComponent();
            DisplayCharts();
        }

        private async void DisplayCharts()
        {
            orderCounts = new List<AreaOdersCount>();
            var entries = new List<ChartEntry>();
            List<Project> projects = await ToDoRepository.GetProjectsAsync();

            foreach (var project in projects)
            {
                List<Item> tasks = await ToDoRepository.GetTasksCompletedByProjectIdAsync(project.Id);
                List<Item> items = await ToDoRepository.GetTasksByProjectIdAsync(project.Id);
                int CountCompletedTasks = tasks.Count;
                int CountAllTasks = items.Count+tasks.Count;
                if (CountCompletedTasks == 0)
                {
                    int percent = 0;
                    orderCounts.Add(new AreaOdersCount { Area = project.Name, OrderCount = 1 });
                }
                else
                {
                    int percent = 100 * CountCompletedTasks / CountAllTasks;

                    orderCounts.Add(new AreaOdersCount { Area = project.Name, OrderCount = percent });
                }
            }
            foreach (var data in orderCounts)
            {
                Random ran = new Random();

                SKColor randomColor = SKColor.FromHsv(ran.Next(256), ran.Next(256), ran.Next(256));
                var entry = new ChartEntry(data.OrderCount)
                {
                    Label = data.Area,
                    ValueLabel = data.OrderCount.ToString()+"%",
                    Color = randomColor
                };
                entries.Add(entry);
            }
            
            var chart = new BarChart()
            {
                Entries = entries,
                LabelTextSize = 40,
                LabelColor = SKColor.Parse("#000000"),
                MaxValue = 100,
                ValueLabelOrientation = Orientation.Horizontal,
                LabelOrientation = Orientation.Vertical,
            };
            this.chartViewBar.Chart = chart;
        }
        public class AreaOdersCount
        {
            public string Area { get; set; }
            public int OrderCount { get; set; }
        }
    }
}