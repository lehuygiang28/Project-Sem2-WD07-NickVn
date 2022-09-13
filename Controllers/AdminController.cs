using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project_Sem2_WD07_NickVn.Models;
using Microsoft.EntityFrameworkCore;

namespace Project_Sem2_WD07_NickVn.Controllers;

public class AdminController : Controller
{
    private readonly ILogger<AdminController> _logger;
    private readonly NickVn_ProjectContext _context;
    private const string SessionKeyAdminId = "_AdminId";
    private const string SessionKeyAdminRole = "_AdminRole";
    private const string SessionKeyAdminUserName = "_AdminUserName";
    private const string NotLogin = null;

    public AdminController(ILogger<AdminController> logger, NickVn_ProjectContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> LoginSolve(string UserName, string Password)
    {
        if (string.IsNullOrEmpty(UserName) || string.IsNullOrWhiteSpace(UserName))
        {
            TempData["error"] = "Username must be input";
            return View(nameof(Login));
        }
        if (string.IsNullOrEmpty(Password) || string.IsNullOrWhiteSpace(Password))
        {
            TempData["error"] = "Password must be input";
            return View(nameof(Login));
        }

        User inputUser = new User();
        Password = MD5.CreateMD5(Password);

        // Get role id
        var roleid = await _context.Roles.OrderBy(a => a.RoleNameEn == "admin").FirstAsync();


        if (!await _context.Users.AnyAsync(u => u.UserName == UserName && u.Password == Password && u.RoleId == roleid.RoleId))
        {
            TempData["error"] = "Username or Password is incorrect";
            return View(nameof(Login));
        }

        var loginUser = await _context.Users.Where(u => u.UserName == UserName && u.Password == Password && u.RoleId == roleid.RoleId).FirstAsync();

        if (loginUser == null)
        {
            TempData["error"] = "Has error, try again";
            return View(nameof(Login));
        }

        HttpContext.Session.SetInt32(SessionKeyAdminId, loginUser.Id);
        HttpContext.Session.SetInt32(SessionKeyAdminRole, loginUser.RoleId);
        HttpContext.Session.SetString(SessionKeyAdminUserName, loginUser.UserName);

        var id = HttpContext.Session.GetInt32(SessionKeyAdminId);
        var role = HttpContext.Session.GetInt32(SessionKeyAdminRole);
        var uname = HttpContext.Session.GetString(SessionKeyAdminUserName);

        _logger.LogInformation("Session key ID: {id}", id);
        _logger.LogInformation("Session key Role Id: {role}", role);
        _logger.LogInformation("Session key UName: {uname}", uname);

        return RedirectToAction(nameof(Index), "Admin");
    }

    public bool IsLogin(HttpContext context)
    {
        if (context.Session.GetString(SessionKeyAdminUserName) == null)
        {
            return false;
        }
        // TempData["adUserName"] = HttpContext.Session.GetString
        return true;
    }

    public IActionResult Login()
    {
        if (IsLogin(HttpContext))
        {
            _logger.LogInformation("Is login, returning to INDEX view ...");
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    public IActionResult Index()
    {
        if (!IsLogin(HttpContext))
        {
            _logger.LogInformation("Not Login, returning to login view ...");
            return View(nameof(Login));
        }

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
