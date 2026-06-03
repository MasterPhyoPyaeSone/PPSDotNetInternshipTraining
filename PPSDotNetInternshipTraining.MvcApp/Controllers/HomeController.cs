using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PPSDotNetInternshipTraining.EFCoreSample2.Database.AppDbContextModels;
using PPSDotNetInternshipTraining.MvcApp.Models;

namespace PPSDotNetInternshipTraining.MvcApp.Controllers;

public class HomeController : Controller
{
    // private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _dbContext;
    public HomeController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // public HomeController(ILogger<HomeController> logger)
    // {
    //     _logger = logger;
    // }


    public IActionResult Index([FromQuery] HomeRequestModel model)

    {
        HomeResponseModel res = new HomeResponseModel
        {
            PageNo = model.PageNo,
            PageSize = model.PageSize
        };
        return View(res);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
