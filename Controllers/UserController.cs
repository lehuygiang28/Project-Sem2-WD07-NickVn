using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project_Sem2_WD07_NickVn.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Project_Sem2_WD07_NickVn.Controllers;

public class UserController : Controller
{
    public const string SessionKeyName = "_Name";
    public const string SessionKeyId = "_Id";
    public const string SessionKeyMoney = "_Money";
    public const string HostName = "GoogleReCaptcha";
    // public string HostName = "localhost";
    private readonly ILogger<UserController> _logger;
    private readonly NickVn_ProjectContext _context;

    public UserController(ILogger<UserController> logger, NickVn_ProjectContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> SignupSolve(User userInput, string password_confirmation)
    {
        if (!ModelState.IsValid)
        {
            TempData["error"] = "Đã xảy ra lỗi! Vui lòng thử lại";
            return View(nameof(Signup));
        }
        
        var SecretKey = (await _context.Googlerecaptchas.Where(a => a.HostName == HostName).FirstAsync()).SecretKey;
        if (!await RecaptchaServices.Validate(Request, SecretKey))
        {
            _logger.LogInformation("Captcha Validate: FALSE");
            ModelState.AddModelError(string.Empty, "Xác minh bạn không phải là robot");
            TempData["error"] = "Xác minh bạn không phải là robot";
            return View(nameof(Signup));
        }
        else
        {
            _logger.LogInformation("Captcha Validate: TRUE");
        }

        bool isExistsUserName = await _context.Users.AnyAsync(u => u.UserName == userInput.UserName);
        bool isExistsPhone = await _context.Users.AnyAsync(u => u.Phone == userInput.Phone);
        bool isExistsEmail = await _context.Users.AnyAsync(u => u.Email == userInput.Email);

        if (isExistsUserName)
        {
            TempData["error"] = "Tài khoản đã được sử dụng";
            return View(nameof(Signup));
        }
        else if (isExistsPhone)
        {
            TempData["error"] = "Số điện thoại đã được sử dụng";
            return View(nameof(Signup));
        }
        else if (isExistsEmail)
        {
            TempData["error"] = "Email đã được sử dụng";
            return View(nameof(Signup));
        }

        if (userInput.UserName.Length < 8)
        {
            TempData["error"] = "Tài khoản phải ít nhất 8 kí tự";
            return View(nameof(Signup));
        }

        bool IsValidPassword(string plainText)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$");
            System.Text.RegularExpressions.Match match = regex.Match(plainText);
            return match.Success;
        }

        if (!IsValidPassword(userInput.Password))
        {
            TempData["error"] = "Mật khẩu chưa hợp lệ";
            return View(nameof(Signup));
        }

        if (String.Compare(userInput.Password, password_confirmation, false) != 0)
        {
            TempData["error"] = "Mật khẩu nhập lại không khớp";
            return View(nameof(Signup));
        }

        bool IsValidEmail(string plainText)
        {
            try
            {
                System.Net.Mail.MailAddress mail = new System.Net.Mail.MailAddress(plainText);
                return true;
            }
            catch
            {
                return false;
            }
        }

        if (!IsValidEmail(userInput.Email))
        {
            TempData["error"] = "Email chưa đúng định dạng";
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

        TempData["success"] = "Đăng ký thành công! Đăng nhập để tiếp tục";
        _logger.LogInformation("Sign up successfull UserName: " + userInput.UserName);

        return View(nameof(Signup));
    }

    public async Task<IActionResult> Signup()
    {
        var SiteKey = await _context.Googlerecaptchas.Where(k => k.HostName == HostName).FirstAsync();

        TempData["siteKey"] = SiteKey.SiteKey;
        return View();
    }

    public IActionResult Logout()
    {
        if (HttpContext.Session.GetInt32(SessionKeyId) != null)
        {
            HttpContext.Session.Clear();
        }
        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> LoginSolve(string UserName, string Password)
    {
        var SecretKey = (await _context.Googlerecaptchas.Where(a => a.HostName == HostName).FirstAsync()).SecretKey;
        if (!await RecaptchaServices.Validate(Request, SecretKey))
        {
            _logger.LogInformation("Captcha Validate: FALSE");
            ModelState.AddModelError(string.Empty, "Xác minh bạn không phải là robot");
            // ViewBag.Error = "Xác minh bạn không phải là robot";
            TempData["error"] = "Xác minh bạn không phải là robot";
            return RedirectToAction(nameof(Login));
            // return View(nameof(Login));
        }
        else
        {
            _logger.LogInformation("Captcha Validate: TRUE");
        }
        
        if (string.IsNullOrEmpty(UserName) || string.IsNullOrWhiteSpace(UserName))
        {
            TempData["error"] = "Tài khoản không được để trống";
            return View(nameof(Login));
        }
        if (string.IsNullOrEmpty(Password) || string.IsNullOrWhiteSpace(Password))
        {
            TempData["error"] = "Mật khẩu không được để trống";
            return View(nameof(Login));
        }

        User inputUser = new User();
        Password = MD5.CreateMD5(Password);

        if (!await _context.Users.AnyAsync(u => u.UserName == UserName && u.Password == Password))
        {
            TempData["error"] = "Tài khoản hoặc mật khẩu không chính xác";
            return View(nameof(Login));
        }

        var loginUser = await _context.Users.Where(u => u.UserName == UserName && u.Password == Password).FirstAsync();

        if (loginUser == null)
        {
            TempData["error"] = "Có lỗi xảy ra";
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

    public async Task<IActionResult> Login()
    {
        TempData["siteKey"] = (await _context.Googlerecaptchas.Where(a => a.HostName == HostName).FirstAsync()).SiteKey;
        return View();
    }

    public IActionResult Index()
    {
        return RedirectToAction(nameof(Login));
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
