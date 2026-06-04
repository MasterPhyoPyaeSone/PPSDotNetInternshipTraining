using Microsoft.AspNetCore.Mvc;
using PPSDotNetInternshipTraining.HighChart.Models;

namespace PPSDotNetInternshipTraining.HighChart.Controllers;

public class HighChartController : Controller
{
    public IActionResult Index()
    {
        var chartData = new List<ChartDataModel>
            {
                new ChartDataModel 
                { 
                    Name = "North America", 
                    Y = 45.0 
                },
                new ChartDataModel 
                { 
                    Name = "Europe", 
                    Y = 30.0, 
                    Sliced = true, 
                    Selected = true 
                },
                new ChartDataModel 
                { 
                    Name = "Asia Pacific", 
                    Y = 15.0 
                },
                new ChartDataModel 
                { 
                    Name = "Latin America", 
                    Y = 7.0 
                },
                new ChartDataModel 
                { 
                    Name = "Middle East & Africa", 
                    Y = 3.0 
                }
            };
        return View(chartData);
    }
}