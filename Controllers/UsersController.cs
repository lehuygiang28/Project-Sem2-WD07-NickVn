using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project_Sem2_WD07_NickVn.Models;

namespace Project_Sem2_WD07_NickVn.Controllers;

public class UsersController : Controller
{
    private readonly ILogger<UsersController> _logger;

    public UsersController(ILogger<UsersController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View(nameof(Login));
    }

    public IActionResult Login()
    {
        return View();
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
