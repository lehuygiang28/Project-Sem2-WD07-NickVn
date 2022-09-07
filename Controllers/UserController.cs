using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project_Sem2_WD07_NickVn.Models;
using Microsoft.EntityFrameworkCore;

namespace Project_Sem2_WD07_NickVn.Controllers;

public class UserController : Controller
{
    public const string SessionKeyName = "_Name";
    public const string SessionKeyId = "_Id";
    public const string SessionKeyMoney = "_Money";
    private readonly ILogger<UserController> _logger;
    private readonly NickVn_ProjectContext _context;

    public UserController(ILogger<UserController> logger, NickVn_ProjectContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> SignupSolve(User userInput, string password_confirmation)
    {
        // if (!ModelState.IsValid)
        // {
        //     ViewBag.Error = "Đã xảy ra lỗi! Vui lòng thử lại.";
        //     return View(nameof(Signup));
        // }

        bool isExistsUserName = await _context.Users.AnyAsync(u => u.UserName == userInput.UserName);
        bool isExistsPhone = await _context.Users.AnyAsync(u => u.Phone == userInput.Phone);
        bool isExistsEmail = await _context.Users.AnyAsync(u => u.Email == userInput.Email);

        if(isExistsUserName)
        {
            ViewBag.Error = "Tài khoản đã được sử dụng";
            return View(nameof(Signup));
        }
        else if(isExistsPhone)
        {
            ViewBag.Error = "Số điện thoại đã được sử dụng";
            return View(nameof(Signup));
        }
        else if(isExistsEmail)
        {
            ViewBag.Error = "Email đã được sử dụng";
            return View(nameof(Signup));
        }

        if(userInput.UserName.Length < 8)
        {
            ViewBag.Error = "Tài khoản phải ít nhất 8 kí tự";
            return View(nameof(Signup));
        }

        bool IsValidPassword(string plainText)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$");
            System.Text.RegularExpressions.Match match = regex.Match(plainText);
            return match.Success;
        }

        if(!IsValidPassword(userInput.Password))
        {
            ViewBag.Error = "Mật khẩu chưa hợp lệ";
            return View(nameof(Signup));
        }

        if (String.Compare(userInput.Password, password_confirmation, false) != 0)
        {
            ViewBag.Error = "Mật khẩu nhập lại không khớp";
            return View(nameof(Signup));
        }
        
        bool IsValidEmail(string plainText)
        {
            try
            {
                System.Net.Mail.MailAddress mail = new System.Net.Mail.MailAddress(plainText);
                return true;
            } catch {
                return false;
            }
        }

        if(!IsValidEmail(userInput.Email)){
            ViewBag.Error = "Email chưa đúng định dạng";
            return View(nameof(Signup));
        }

        userInput.Password = MD5.CreateMD5(userInput.Password);
        userInput.FirstName = "NickVn";
        DateTime localTime = DateTime.Now;

        userInput.LastName = localTime.ToString("yyyyMMdd'T'HHmmss");
        userInput.CreateAt = localTime;
        userInput.UpdateAt = localTime;

        await _context.AddAsync(userInput);
        await _context.SaveChangesAsync();

        ViewBag.Success = "Đăng ký thành công! Đăng nhập để tiếp tục";
        _logger.LogInformation("Sign up successfull UserName: " + userInput.UserName);

        return View(nameof(Signup));
    }

    public IActionResult Signup()
    {
        return View();
    }

    public IActionResult Logout()
    {
        if (HttpContext.Session.GetInt32(SessionKeyId) != null){
            HttpContext.Session.Clear();
        }
        return RedirectToAction("Index", "Home");
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
