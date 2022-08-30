using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project_Sem2_WD07_NickVn.Models;

namespace Project_Sem2_WD07_NickVn.Controllers;

public class UsersController : Controller
{
    private readonly ILogger<UsersController> _logger;
    private readonly NickVn_ProjectContext _context;

    public UsersController(ILogger<UsersController> logger, NickVn_ProjectContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult LoginSolve()
    {
        return NotFound();
    }

    public IActionResult Login()
    {
        return View();
    }
    
    public IActionResult Index()
    {
        return Login();
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
