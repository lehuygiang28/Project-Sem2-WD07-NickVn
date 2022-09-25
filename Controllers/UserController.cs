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
    private const string HostName = "GoogleReCaptcha";
    // public string HostName = "localhost";
    private readonly ILogger<UserController> _logger;
    private readonly NickVn_ProjectContext _context;

    public UserController(ILogger<UserController> logger, NickVn_ProjectContext context)
    {
        _logger = logger;
        _context = context;
    }

    private bool IsLogin()
    {
        var sessionValueId = HttpContext.Session.GetInt32(SessionKeyId);
        if (sessionValueId == null)
        {
            return false;
        }
        return true;
    }

    public async Task<IActionResult> Account()
    {
        try
        {
            if (!IsLogin())
            {
                return RedirectToAction("Index", "Home");
            }
            int userId = Convert.ToInt32(HttpContext.Session.GetInt32(SessionKeyId));
            List<Oder> listAccountId = await _context.Oders.Where(o => o.UserId == userId).OrderByDescending(a => a.CreateAt).ToListAsync();

            if (listAccountId == null)
            {
                return View();
            }

            Task addList(Lienminh item, List<Lienminh> list)
            {
                Task.Run(() =>
                {
                    list.Add(item);
                });
                return Task.CompletedTask;
            }

            List<Lienminh> listProducts = new List<Lienminh>();
            foreach (Oder itemInList in listAccountId)
            {
                System.Console.WriteLine("ID LIST: " + itemInList.ProductId);
                System.Console.WriteLine("ID ");
                Lienminh item = await _context.Lienminhs.Where(o => o.Id == itemInList.ProductId).FirstAsync();
                await addList(item, listProducts);
            }

            ViewBag.listOrders = listAccountId;
            ViewBag.listProducts = listProducts;
        } catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
        }

        return View();
    }

    public async Task<IActionResult> ChangePasswordSolve(string oldPassword, string newPassword, string passwordConfirmation)
    {
        if (!IsLogin())
        {
            return RedirectToAction("Index", "Home");
        }

        if (string.IsNullOrEmpty(oldPassword) || string.IsNullOrWhiteSpace(oldPassword))
        {
            TempData["error"] = "Mật khẩu cũ chưa hợp lệ";
            return View(nameof(ChangePassword));
        }

        var currentUser = await _context.Users.Where(a => a.Id == HttpContext.Session.GetInt32(SessionKeyId)).FirstAsync();

        oldPassword = MD5.CreateMD5(oldPassword);
        if (string.Compare(oldPassword, currentUser.Password) != 0)
        {
            TempData["error"] = "Sai mật khẩu";
            return View(nameof(ChangePassword));
        }

        bool IsValidPassword(string plainText)
        {
            return true; // Disable password regex check for dev
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$");
            System.Text.RegularExpressions.Match match = regex.Match(plainText);
            return match.Success;
        }

        if (!IsValidPassword(newPassword))
        {
            TempData["error"] = "Mật khẩu mới chưa hợp lệ";
            return View(nameof(ChangePassword));
        }

        //string.Compare(newPassword, passwordConfirmation, true): neu true thi se bo qua in hoa va in thuong
        if (string.Compare(newPassword, passwordConfirmation) != 0)
        {
            TempData["error"] = "Mật khẩu nhập lại không khớp";
            return View(nameof(ChangePassword));
        }

        newPassword = MD5.CreateMD5(newPassword);

        currentUser.Password = newPassword;
        DateTime now = DateTime.Now;
        currentUser.UpdateAt = now;

        _context.Users.Update(currentUser);
        await _context.SaveChangesAsync();

        TempData["success"] = "Đổi mật khẩu thành công";
        _logger.LogInformation($"Change password successfully! ID: {currentUser.Id} - Time: {now.ToString("HH:mm:ss dd/MM/yyyy")}");

        return RedirectToAction(nameof(ChangePassword));
    }

    public IActionResult ChangePassword()
    {
        if (!IsLogin())
        {
            return RedirectToAction("Index", "Home");
        }
        return View();
    }

    public async Task<IActionResult> Profile()
    {
        if (!IsLogin())
        {
            return RedirectToAction(nameof(Login));
        }

        var idSession = HttpContext.Session.GetInt32(SessionKeyId);
        var user = await _context.Users.Where(a => a.Id == idSession).FirstAsync();
        var roles = await _context.Roles.OrderBy(a => a.Id).ToListAsync();

        TempData["roles"] = roles;
        TempData["user"] = user;
        return View();
    }

    public async Task<IActionResult> SignupSolve(User userInput, string password_confirmation)
    {
        if (IsLogin())
        {
            return RedirectToAction("Index", "Home");
        }

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
        if (IsLogin())
        {
            return RedirectToAction("Index", "Home");
        }

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
        if (IsLogin())
        {
            return RedirectToAction("Index", "Home");
        }

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

        loginUser.LastLogin = DateTime.Now;
        _context.Update(loginUser);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index), "Home");
    }

    public async Task<IActionResult> Login()
    {
        if (IsLogin())
        {
            return RedirectToAction("Index", "Home");
        }

        TempData["siteKey"] = (await _context.Googlerecaptchas.Where(a => a.HostName == HostName).FirstAsync()).SiteKey;
        return View();
    }

    public IActionResult Index()
    {
        if (IsLogin())
        {
            return RedirectToAction("Index", "Home");
        }

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
