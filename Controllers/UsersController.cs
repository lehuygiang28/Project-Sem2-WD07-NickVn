using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project_Sem2_WD07_NickVn.Models;
using Microsoft.EntityFrameworkCore;

namespace Project_Sem2_WD07_NickVn.Controllers;

public class UsersController : Controller
{
    public const string SessionKeyName = "_Name";
    public const string SessionKeyId = "_Id";
    public const string SessionKeyMoney = "_Money";
    private readonly ILogger<UsersController> _logger;
    private readonly NickVn_ProjectContext _context;

    public UsersController(ILogger<UsersController> logger, NickVn_ProjectContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> LoginSolve(string UserName, string Password)
    {
        if (string.IsNullOrEmpty(UserName) || string.IsNullOrWhiteSpace(UserName))
        {
            ViewBag.Error = "User Name can not be null";
            return View(nameof(Login));
        }
        if (string.IsNullOrEmpty(Password) || string.IsNullOrWhiteSpace(Password))
        {
            ViewBag.Error = "Password can not be null";
            return View(nameof(Login));
        }

        User inputUser = new User();
        inputUser.UserName = UserName;
        inputUser.Password = MD5.CreateMD5(Password);

        if(!await _context.Users.AnyAsync(u => u.UserName == inputUser.UserName && u.Password == inputUser.Password))
        {
            ViewBag.Error = "UserName or Password is incorrect!";
            return View(nameof(Login));
        }

        var loginUser = await _context.Users.Where(u => u.UserName == inputUser.UserName && u.Password == inputUser.Password).FirstAsync();
        
        if (loginUser == null)
        {
            ViewBag.Error = "Có lỗi xảy ra";
            return View(nameof(Login));
        }

        HttpContext.Session.SetInt32(SessionKeyId, loginUser.Id);
        HttpContext.Session.SetString(SessionKeyName, loginUser.UserName);
        HttpContext.Session.SetInt32(SessionKeyMoney, Convert.ToInt32(loginUser.Money));

        var id = HttpContext.Session.GetInt32(SessionKeyId);
        var uname = HttpContext.Session.GetString(SessionKeyName);
        var money = HttpContext.Session.GetInt32(SessionKeyMoney);

        _logger.LogInformation("Session key ID: {id}", id);
        _logger.LogInformation("Session key UName: {uname}", uname);
        _logger.LogInformation("Session key Money: {money}", money);

        return RedirectToAction(nameof(Index), "Home");
    }

    public IActionResult Login()
    {
        return View();
    }
    
    public IActionResult Index()
    {
        return View(nameof(Login));
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
