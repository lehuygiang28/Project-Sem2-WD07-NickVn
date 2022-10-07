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
    private const string HostName = "GoogleReCaptcha";
    // public string HostName = "localhost";
    private readonly ILogger<UserController> _logger;
    private readonly NickVn_ProjectContext _context;

    public UserController(ILogger<UserController> logger, NickVn_ProjectContext context)
    {
        _logger = logger;
        _context = context;
    }

    private async Task GenerateChargeCard()
    {
        try
        {
            List<TheNapDatum> listCard = new List<TheNapDatum>();
            TheNapDatum card;
            while (listCard.Count < 10)
            {
                card = new TheNapDatum();
                card.TelecomName = RandomTelecom();
                card.Amount = (int)RandomAmount(card.TelecomName);
                card.Pin = RandomString(13);
                card.Serial = RandomString(13);
                card.IsUse = false;
                card.CreateAt = DateTime.Now;
                card.UpdateAt = card.CreateAt;

                bool isExists = await _context.ChargeHistories.AnyAsync(i => i.Telecom == card.TelecomName && i.Amount == card.Amount && i.Pin == card.Pin && i.Serial == card.Serial);

                if (isExists == true)
                {
                    continue;
                }
                else if (listCard.Count >= 1)
                {
                    isExists = listCard.Any(i => i.TelecomName == card.TelecomName && i.Amount == card.Amount && i.Pin == card.Pin && i.Serial == card.Serial);
                    if (isExists == true)
                    {
                        continue;
                    }
                }

                listCard.Add(card);
            }
            _context.TheNapData.RemoveRange(await _context.TheNapData.OrderBy(a => a.Id).ToListAsync());
            await _context.TheNapData.AddRangeAsync(listCard);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
        }
    }

    private decimal RandomAmount(string telecom)
    {
        Random random = new Random();
        decimal[] dec = { 10000, 20000, 30000, 50000, 100000, 200000, 300000, 500000 };
        List<decimal> decs = new List<decimal>();
        switch (telecom)
        {
            case "VIETTEL":
            case "ZING":
            case "GATE":
            case "VCOIN":
                decs.AddRange(new List<decimal> { 10000, 20000, 30000, 50000, 100000, 200000, 300000, 500000, 1000000 });
                break;
            case "MOBIFONE":
            case "VINAPHONE":
                decs.AddRange(new List<decimal> { 10000, 20000, 30000, 50000, 100000, 200000, 300000, 500000 });
                break;
            case "VIETNAMMOBILE":
                decs.AddRange(new List<decimal> { 10000, 20000, 50000, 100000, 200000, 300000, 500000 });
                break;
            case "GARENA":
                decs.AddRange(new List<decimal> { 20000, 50000, 100000, 200000, 500000 });
                break;
            default:
                decs.AddRange(new List<decimal> { 20000, 50000, 100000, 200000, 500000 });
                break;
        }
        return decs.ElementAt(RandomInt(0, decs.Count - 1));
    }

    private string RandomString(int length)
    {
        Random random = new Random();
        const string chars = "0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    private int RandomInt(int min, int max)
    {
        Random rd = new Random();
        return (int)rd.NextInt64(min, max);
    }

    private string RandomTelecom()
    {
        Random random = new Random();
        string[] tel = { "VIETTEL", "MOBIFONE", "VINAPHONE", "VIETNAMMOBILE", "ZING", "GATE", "VCOIN", "GARENA" };
        return tel[RandomInt(0, tel.Length - 1)];
    }

    public async Task<IActionResult> TheNap(int? renew)
    {
        var list = await _context.TheNapData.OrderBy(a => a.Amount).ToListAsync();
        if (list == null || list.Count() <= 0 || renew == 1)
        {
            await GenerateChargeCard();
            return RedirectToAction(nameof(TheNap));
        }
        ViewBag.card = list;
        return View();
    }

    private async Task RenewUserInformation()
    {
        var user = await _context.Users.Where(u => u.Id == HttpContext.Session.GetInt32(SessionKeyId)).FirstAsync();
        if (user != null)
        {
            HttpContext.Session.SetInt32(SessionKeyMoney, Convert.ToInt32(user.Money));
        }
    }

    private async Task<bool> IsLogin()
    {
        var sessionValueId = HttpContext.Session.GetInt32(SessionKeyId);
        if (sessionValueId == null)
        {
            return false;
        }
        await RenewUserInformation();
        return true;
    }

    public string? LayMenhGiaThe(string telecom_key)
    {
        string? htmlMenhGia = "";

        string gia10k = @"<option value='10000'>10.000 ₫</option>";
        string gia20k = @"<option value='20000'>20.000 ₫</option>";
        string gia30k = @"<option value='30000'>30.000 ₫</option>";
        string gia50k = @"<option value='50000'>50.000 ₫</option>";
        string gia100k = @"<option value='100000'>100.000 ₫</option>";
        string gia200k = @"<option value='200000'>200.000 ₫</option>";
        string gia300k = @"<option value='300000'>300.000 ₫</option>";
        string gia500k = @"<option value='500000'>500.000 ₫</option>";
        string gia1000k = @"<option value='1000000'>1.000.000 ₫</option>";

        switch (telecom_key)
        {
            case "VIETTEL":
            case "ZING":
            case "GATE":
            case "VCOIN":
                htmlMenhGia = gia10k + gia20k + gia30k + gia50k + gia100k + gia200k + gia300k + gia500k + gia1000k;
                break;
            case "MOBIFONE":
            case "VINAPHONE":
                htmlMenhGia = gia10k + gia20k + gia30k + gia50k + gia100k + gia200k + gia300k + gia500k;
                break;
            case "VIETNAMMOBILE":
                htmlMenhGia = gia10k + gia20k + gia50k + gia100k + gia200k + gia300k + gia500k;
                break;
            case "GARENA":
                htmlMenhGia = gia20k + gia50k + gia100k + gia200k + gia500k;
                break;
            default:
                htmlMenhGia = null;
                break;
        }

        return htmlMenhGia;
    }

    public async Task<IActionResult> NapTheSolve(int? id, string? UserName, string telecom, decimal? amount, string? pin, string? serial, int? type_charge)
    {
        decimal? amountInput = amount;
        var SecretKey = (await _context.Googlerecaptchas.Where(a => a.HostName == HostName).FirstAsync()).SecretKey;
        if (!await RecaptchaServices.Validate(Request, SecretKey))
        {
            _logger.LogInformation("Captcha Validate: FALSE");
            ModelState.AddModelError(string.Empty, "Xác minh bạn không phải là robot");
            // ViewBag.Error = "Xác minh bạn không phải là robot";
            TempData["error"] = "Xác minh bạn không phải là robot";
            return RedirectToAction(nameof(NapThe));
        }
        else
        {
            _logger.LogInformation("Captcha Validate: TRUE");
        }

        if (pin == null || serial == null)
        {
            TempData["error"] = "Vui lòng nhập mã thẻ và seri";
            return RedirectToAction(nameof(NapThe));
        }
        
        if (amountInput == null)
        {
            TempData["error"] = "Hãy chọn mệnh giá thẻ!";
            return RedirectToAction(nameof(NapThe));
        }

        bool isValidCard = await _context.TheNapData.AnyAsync(t => t.TelecomName == telecom && t.Pin == pin && t.Serial == serial);
        if (isValidCard == false)
        {
            TempData["error"] = "Thông tin thẻ không chính xác! Vui lòng thử lại.";
            return RedirectToAction(nameof(NapThe));
        }

        var napthe = await _context.TheNapData.Where(t => t.TelecomName == telecom && t.Pin == pin && t.Serial == serial).FirstAsync();
        if(napthe.IsUse == true)
        {
            TempData["error"] = "Thẻ đã được sử dụng.";
            return RedirectToAction(nameof(NapThe));
        }

        if (id == null || UserName == null)
        {
            TempData["error"] = "Có lỗi xảy ra! Hãy đăng nhập và thử lại!";
            return RedirectToAction(nameof(NapThe));
        }

        var user = await _context.Users.Where(u => u.Id == id && u.UserName == UserName).FirstAsync();
        if (user == null)
        {
            TempData["error"] = "Có lỗi xảy ra! Hãy đăng nhập và thử lại!";
            return RedirectToAction(nameof(NapThe));
        }
        bool isWrongAmount = false;
        if (napthe.Amount != amountInput)
        {
            isWrongAmount = true;
            amountInput = napthe.Amount / 100 * 40;
            TempData["error"] = "Chọn sai mệnh giá thẻ, bị trừ 60% giá trị thẻ. Bạn nhận được " + amountInput;
        }

        user.Money += (decimal)amountInput;

        string typeCharge = "";
        if (type_charge != null && type_charge == 1)
        {
            typeCharge = "Nạp thẻ cào";
        }
        // Write history charge
        ChargeHistory history = new ChargeHistory();
        history.UserId = (int)id;
        history.Telecom = telecom;
        history.Amount = (decimal)napthe.Amount;
        history.MoneyReceived = (decimal)amountInput;
        history.Pin = pin;
        history.Serial = serial;
        history.TypeCharge = typeCharge;
        history.CreateAt = DateTime.Now;
        history.UpdateAt = DateTime.Now;
        history.Result = "Thành công";
        if (isWrongAmount == true)
        {
            history.Note = $"Sai mệnh giá, mệnh giá gốc {history.Amount}, mệnh giá chọn {amount}, nhận được {history.MoneyReceived}";
            history.Result += ", sai mệnh giá";
        }

        // Change status of card to used
        napthe.IsUse = true;

        await _context.ChargeHistories.AddAsync(history);
        _context.TheNapData.Update(napthe);
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        TempData["success"] = "Nạp thẻ thành công! Bạn nhận được: " +  history.MoneyReceived;

        HttpContext.Session.SetInt32(SessionKeyMoney, (int)user.Money);
        _logger.LogInformation($"Update user session money\nID: {user.Id} - Money: {user.Money} - Time: {DateTime.Now}");

        return RedirectToAction(nameof(NapThe));
    }

    public async Task<IActionResult> NapThe()
    {
        if (!await IsLogin())
        {
            return RedirectToAction(nameof(Login));
        }
        TempData["siteKey"] = (await _context.Googlerecaptchas.Where(i => i.Id == 1).FirstAsync()).SiteKey;

        var id = HttpContext.Session.GetInt32(SessionKeyId);
        var query = from his in _context.ChargeHistories
                    where his.UserId == id
                    orderby his.CreateAt descending
                    select his;

        bool historyCount = await query.AnyAsync();
        System.Console.WriteLine("HISTORY: " + historyCount.ToString());
        if (historyCount == false)
        {
            ViewBag.history = null;
            return View();
        }

        var history = await query.Take(6).ToListAsync();

        ViewBag.history = history;
        return View();
    }

    public async Task<IActionResult> Account()
    {
        if (!await IsLogin())
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

        return View();
    }

    public async Task<IActionResult> ChangePasswordSolve(string oldPassword, string newPassword, string passwordConfirmation)
    {
        if (!await IsLogin())
        {
            return RedirectToAction("Index", "Home");
        }

        if (string.IsNullOrEmpty(oldPassword) || string.IsNullOrWhiteSpace(oldPassword))
        {
            TempData["error-change-pw"] = "Mật khẩu cũ không được để trống";
            return View(nameof(ChangePassword));
        }

        var currentUser = await _context.Users.Where(a => a.Id == HttpContext.Session.GetInt32(SessionKeyId)).FirstAsync();

        oldPassword = MD5.CreateMD5(oldPassword);
        if (string.Compare(oldPassword, currentUser.Password) != 0)
        {
            TempData["error-change-pw"] = "Sai mật khẩu";
            return View(nameof(ChangePassword));
        }

        bool IsValidPassword(string plainText)
        {
            // return true; // Disable password regex check for dev
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$");
            System.Text.RegularExpressions.Match match = regex.Match(plainText);
            return match.Success;
        }

        if (!IsValidPassword(newPassword))
        {
            TempData["error-change-pw"] = "Mật khẩu mới chưa hợp lệ";
            return View(nameof(ChangePassword));
        }

        //string.Compare(newPassword, passwordConfirmation, true): neu true thi se bo qua in hoa va in thuong
        if (string.Compare(newPassword, passwordConfirmation) != 0)
        {
            TempData["error-change-pw"] = "Mật khẩu nhập lại không khớp";
            return View(nameof(ChangePassword));
        }

        newPassword = MD5.CreateMD5(newPassword);

        currentUser.Password = newPassword;
        DateTime now = DateTime.Now;
        currentUser.UpdateAt = now;

        _context.Users.Update(currentUser);
        await _context.SaveChangesAsync();

        TempData["success-change-pw"] = "Đổi mật khẩu thành công";
        _logger.LogInformation($"Change password successfully! ID: {currentUser.Id} - Time: {now.ToString("HH:mm:ss dd/MM/yyyy")}");

        return RedirectToAction(nameof(ChangePassword));
    }

    public async Task<IActionResult> ChangePassword()
    {
        if (!await IsLogin())
        {
            return RedirectToAction("Index", "Home");
        }
        return View();
    }

    public async Task<IActionResult> Profile()
    {
        if (!await IsLogin())
        {
            return RedirectToAction(nameof(Login));
        }

        var idSession = HttpContext.Session.GetInt32(SessionKeyId);
        var user = await _context.Users.Where(a => a.Id == idSession).FirstAsync();
        var roles = await _context.Roles.OrderBy(a => a.Id).ToListAsync();

        ViewData["roles"] = roles;
        ViewData["user"] = user;
        return View();
    }

    public async Task<IActionResult> SignupSolve(User userInput, string password_confirmation)
    {
        if (await IsLogin())
        {
            return RedirectToAction("Index", "Home");
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

        string defaultAvatarUrl = @"storage/images/unknown-avatar.jpg";
        string defaultCoverUrl = @"storage/images/unknown-cover.jpg";

        // int ROLE_ADMIN = 100;
        int ROLE_USER = 1;

        userInput.LastName = localTime.ToString("yyyyMMdd'T'HHmmss");
        userInput.CreateAt = localTime;
        userInput.UpdateAt = localTime;
        userInput.ImgSrc = defaultAvatarUrl;
        userInput.CoverImgSrc = defaultCoverUrl;
        userInput.RoleId = ROLE_USER;

        await _context.AddAsync(userInput);
        await _context.SaveChangesAsync();

        TempData["success"] = "Đăng ký thành công! Đăng nhập để tiếp tục";
        _logger.LogInformation("Sign up successfull UserName: " + userInput.UserName);

        return View(nameof(Signup));
    }

    public async Task<IActionResult> Signup()
    {
        if (await IsLogin())
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
        if (await IsLogin())
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
        if (await IsLogin())
        {
            return RedirectToAction("Index", "Home");
        }

        TempData["siteKey"] = (await _context.Googlerecaptchas.Where(a => a.HostName == HostName).FirstAsync()).SiteKey;
        return View();
    }

    public async Task<IActionResult> Index()
    {
        if (await IsLogin())
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
