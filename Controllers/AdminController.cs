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
    private const string HostName = "GoogleReCaptcha";
    private const string NotLogin = null;
    private readonly IWebHostEnvironment _hostEnvironment;
    private const int DISABLE_OR_DELETE = 0;

    public AdminController(ILogger<AdminController> logger, NickVn_ProjectContext context, IWebHostEnvironment hostEnvironment)
    {
        this._logger = logger;
        this._context = context;
        this._hostEnvironment = hostEnvironment;
    }
    
    private string RandomString(int length)
    {
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghiklmnopqrstuvwxyz";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    private decimal RandomDecimal(int min, int max)
    {
        Random rd = new Random();
        return rd.NextInt64(min, max);
    }

    public async Task<IActionResult> GenerateProductData(int? s)
    {
        if(!await IsLogin())
        {
            return RedirectToAction(nameof(Index));
        }
        var watch = System.Diagnostics.Stopwatch.StartNew();
        int? dataCount = s;
        // Define number of data
        if(dataCount == null || dataCount.Equals(null))
        {
            dataCount = 3;
        }
        Random random = new Random();
        string[] rankRandomArray = {"Chưa Rank", "Sắt", "Đồng", "Bạc", "Vàng", "Bạch Kim", "Kim Cương", "Cao Thủ", "Đại Cao Thủ", "Thách Đấu"};

        string[] urlImage ={
            @"/storage/images/0qBPw7AiOQ_1632531413.jpg",
            @"/storage/images/CM9q56zAnM_1632531413.jpg",
            @"/storage/images/pTWDgoJQuz_1632531413.jpg",
            @"/storage/images/zwRCsqMtyo_1632531413.jpg",
            @"/storage/images/HLpEr7ojZm_1632531414.jpg",
            @"/storage/images/xxuC88f0h9_1632531414.jpg"
        };
        int urlImageLength = urlImage.Length;

        int lienMinhID;
        var lastLienMinhID = await _context.Lienminhs.OrderBy(a => a.Id).LastAsync();
        if(lastLienMinhID == null)
        {
            lienMinhID = 1;
        }
        else
        {
            lienMinhID = lastLienMinhID.Id;
        }

        int lastImgID;
        var varimg = await _context.Images.OrderBy(b => b.ImgId).LastAsync();
        if (varimg == null)
        {
            lastImgID = 1;
        }
        else
        {
            lastImgID = varimg.ImgId;
        }

        // Generate `lienminh` clone data
        for (int i = 0; i <= dataCount; i++)
        {
            lienMinhID++;
            Lienminh newProduct = new Lienminh();

            newProduct.Id = lienMinhID;
            newProduct.Name = "Liên Minh";
            newProduct.ProductUserName = "shop" + RandomString(6);
            newProduct.ProductUserPassword = "passw" + RandomString(8);
            newProduct.Publisher = "Garena";
            newProduct.PriceAtm = RandomDecimal(10000, int.MaxValue/100);
            newProduct.Champ = (int)RandomDecimal(1, 150);
            newProduct.Skin = (int)RandomDecimal(1, 555);
            newProduct.Rank = rankRandomArray[random.Next(0, rankRandomArray.Length)];
            newProduct.Status = "Trắng Thông Tin";
            newProduct.Note = 0;
            newProduct.ImgThumb = @"/storage/images/FSPfB05HiR_1632531414.jpg";
            newProduct.ImgSrc = @"/storage/images/0qBPw7AiOQ_1632531413.jpg";
            newProduct.Sold = Lienminh.NOT_SOLD;

            // Generate clone image path
            for (int j = 0; j < urlImageLength; j++)
            {
                lastImgID++;
                Image productImage = new Image();

                productImage.ImgId = lastImgID;
                productImage.LienminhId = lienMinhID;
                productImage.ImgLink = urlImage[j];
                // System.Console.WriteLine($"ID: {productImage.ImgId}");
                // System.Console.WriteLine($"LOL ID: {productImage.LienminhId}");
                // System.Console.WriteLine($"Index {j}: " + productImage.ImgLink);

                // Save image path clone to database
                await _context.Images.AddAsync(productImage);
                await _context.SaveChangesAsync();
            }

            // Save data to database
            await _context.Lienminhs.AddAsync(newProduct);
            await _context.SaveChangesAsync();

            // Log data
            // System.Console.WriteLine("IMG TOTAL: " + urlImageLength);
            // System.Console.WriteLine("Id: " + newProduct.Id);
            // System.Console.WriteLine("ProductUserName: " + newProduct.ProductUserName);
            // System.Console.WriteLine("ProductUserPassword: " + newProduct.ProductUserPassword);
            // System.Console.WriteLine("NEW PRICE: "+newProduct.PriceAtm);
            // System.Console.WriteLine("Champ: " + newProduct.Champ);
            // System.Console.WriteLine("Skin: " + newProduct.Skin);
            // System.Console.WriteLine("Rank: " + newProduct.Rank);
            // System.Console.WriteLine("---------------------");
        }
        watch.Stop();
        var elapseTime = watch.Elapsed;
        System.Console.WriteLine("---------------------");
        System.Console.WriteLine($"Data count: {dataCount}");
        System.Console.WriteLine($"Generate Data Done, time: {elapseTime}");
        System.Console.WriteLine("---------------------");
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Products(int page, string? SearchKey, int? id, decimal? price, int? status)
    {
        if (!await IsLogin())
        {
            return RedirectToAction(nameof(Index));
        }

        decimal priceSearchMin = decimal.Zero;
        decimal priceSearchMax = decimal.Zero;
        switch (price)
        {
            case 1:
                priceSearchMin = 0;
                priceSearchMax = 50000;
                break;
            case 2:
                priceSearchMin = 50000;
                priceSearchMax = 200000;
                break;
            case 3:
                priceSearchMin = 200000;
                priceSearchMax = 500000;
                break;
            case 4:
                priceSearchMin = 500000;
                priceSearchMax = 1000000;
                break;
            case 5:
                priceSearchMin = 1000000;
                priceSearchMax = decimal.MaxValue;
                break;
            case 6:
                priceSearchMin = 5000000;
                priceSearchMax = decimal.MaxValue;
                break;
            case 7:
                priceSearchMin = 10000000;
                priceSearchMax = decimal.MaxValue;
                break;
            default:
                priceSearchMin = 0;
                priceSearchMax = decimal.MaxValue;
                break;
        }

        var query = from lienMinh in _context.Lienminhs
                    where lienMinh.Sold == (status == null ? Lienminh.NOT_SOLD : status)
                    && lienMinh.Name.Contains(SearchKey == null ? string.Empty : SearchKey)
                    && (lienMinh.PriceAtm >= priceSearchMin && lienMinh.PriceAtm < priceSearchMax)
                    select lienMinh;


        int totalPage = 0;
        int totalRecord = 0;
        int pageSize = 9;

        totalRecord = await query.CountAsync();
        totalPage = (totalRecord / pageSize) + ((totalRecord % pageSize) > 0 ? 1 : 0);

        List<Lienminh> listLienMinhAcc = await query.OrderBy(prod => prod.Id)
                                                    .Skip((page - 1) * pageSize)
                                                    .Take(pageSize).ToListAsync();

        ViewBag.listLienMinhAcc = listLienMinhAcc;
        ViewBag.totalPage = totalPage;
        ViewBag.currentPage = page;
        return View();
    }

    //Delete Category
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var searchId = await _context.Categories.AnyAsync(c => c.Id == id);
        if (!searchId)
        {
            _logger.LogInformation("Can not delete unknown ID: " + id);
            return Redirect(Request.Headers["Referer"].ToString());
        }

        var find = await _context.Categories.Where(c => c.Id == id).FirstAsync();
        find.Status = DISABLE_OR_DELETE;

        _context.Categories.Update(find);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Categories));
    }

    public async Task<IActionResult> EditCategorySolve(int id, string? Name, string? Title, string? ActionString, int? Total, string? Note, IFormFile? ImgSaleOff, IFormFile? ImgSrc)
    {
        Category otherCategory = new Category();
        try
        {
            otherCategory = await _context.Categories.Where(c => c.Id == id).FirstAsync();
            otherCategory.Name = Name == null ? otherCategory.Name : Name;
            otherCategory.Title = Title == null ? otherCategory.Title : Title;
            otherCategory.Action = ActionString == null ? otherCategory.Action : ActionString;
            otherCategory.Total = Total == null ? otherCategory.Total : (int)Total;
            otherCategory.Note = Note == null ? otherCategory.Note : Note;

            try
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName;
                string extension;
                string path;
                string slashImg = @"\storage\images";

                IFormFile posted;
                if (ImgSaleOff != null)
                {
                    posted = (IFormFile)ImgSaleOff;
                    fileName = Path.GetFileNameWithoutExtension(posted.FileName);
                    extension = Path.GetExtension(posted.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    // path = Path.Combine("/img", fileName);
                    path = Path.Combine(wwwRootPath + slashImg, fileName);
                    System.Console.WriteLine("PATHHHHHHHHHHHH" + path);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await posted.CopyToAsync(fileStream);
                    }
                    otherCategory.ImgSaleOff = Path.Combine(slashImg, fileName);
                    // product.ImagePath += Path.Combine("/img/" + fileName) + "\n";
                    _logger.LogInformation("IMG URL local: " + path);
                    _logger.LogInformation("IMG URL: " + otherCategory.ImgSaleOff);
                }

                if (ImgSrc != null)
                {
                    posted = (IFormFile)ImgSrc;
                    fileName = Path.GetFileNameWithoutExtension(posted.FileName);
                    extension = Path.GetExtension(posted.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    // path = Path.Combine("/img", fileName);
                    path = Path.Combine(wwwRootPath + slashImg, fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await posted.CopyToAsync(fileStream);
                    }
                    otherCategory.ImgSrc = Path.Combine(slashImg, fileName);
                    // product.ImagePath += Path.Combine("/img/" + fileName) + "\n";
                    _logger.LogInformation("IMG URL local: " + path);
                    _logger.LogInformation("IMG URL: " + otherCategory.ImgSrc);
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Edit Category Exeception: {0}", ex.Message);
                throw new Exception();
            }

            _context.Categories.Update(otherCategory);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Edit/Update Category successfull \n\tCategoryID: " + otherCategory.Id);

        }
        catch (Exception ex)
        {
            _logger.LogInformation("Edit Category Exeception: {0}", ex.Message);
            return RedirectToAction(nameof(Categories));
        }

        return RedirectToAction(nameof(DetailCategory), new { id = otherCategory.Id });
    }

    public async Task<IActionResult> EditCategory(int? id)
    {
        if (!await IsLogin())
        {
            return RedirectToAction(nameof(Index));
        }
        if (id == null)
        {
            TempData["error"] = "Not Found this ID";
            return RedirectToAction(nameof(DetailCategory));
        }

        var selectedCategory = await _context.Categories.Where(c => c.Id == id).FirstAsync();


        ViewBag.selectedCategory = selectedCategory;
        return View();
    }

    public async Task<IActionResult> DetailCategory(int? id)
    {
        if (!await IsLogin())
        {
            return RedirectToAction(nameof(Index));
        }
        if (id == null)
        {
            TempData["error"] = "Not Found this ID";
            return RedirectToAction(nameof(Categories));
        }

        var cateById = await _context.Categories.Where(c => c.Id == id).FirstAsync();
        var productByCateId = await _context.ProductCategories.Where(c => c.CategoryId == id).ToListAsync();

        ViewBag.cateById = cateById;
        ViewBag.productByCateId = productByCateId;

        return View();
    }

    // Products page start
    public async Task<IActionResult> Categories(int page)
    {
        if (!await IsLogin())
        {
            return RedirectToAction(nameof(Index));
        }
        // var categories = await _context.Categories.OrderBy(p => p.Id).ToListAsync();
        if (page == 0)
        {
            page = 1;
        }

        int totalPage = 0;
        int totalRecord = 0;
        int pageSize = 9;

        var query = from cate in _context.Categories
                    where cate.Status == 1
                    select cate;

        totalRecord = await query.CountAsync();
        totalPage = (totalRecord / pageSize) + ((totalRecord % pageSize) > 0 ? 1 : 0);

        List<Category> listCateQuery = await query.OrderBy(acc => acc.Id)
                                                    .Skip((page - 1) * pageSize)
                                                    .Take(pageSize).ToListAsync();

        ViewBag.listCateQuery = listCateQuery;
        ViewBag.totalPage = totalPage;
        ViewBag.currentPage = page;
        return View();
    }

    // Products page end

    public async Task<IActionResult> LoginSolve(string UserName, string Password)
    {
        if (string.IsNullOrEmpty(UserName) || string.IsNullOrWhiteSpace(UserName))
        {
            TempData["error"] = "Username must be input";
            return RedirectToAction(nameof(Login)); ;
        }
        if (string.IsNullOrEmpty(Password) || string.IsNullOrWhiteSpace(Password))
        {
            TempData["error"] = "Password must be input";
            return RedirectToAction(nameof(Login)); ;
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

        User inputUser = new User();
        Password = MD5.CreateMD5(Password);

        // Get role id
        var roleid = await _context.Roles.Where(a => a.RoleNameEn == "Admin").FirstAsync();

        if (!await _context.Users.AnyAsync(u => u.UserName == UserName && u.Password == Password && u.RoleId == roleid.RoleId))
        {
            TempData["error"] = "Username or Password is incorrect";
            return RedirectToAction(nameof(Login)); ;
        }

        var loginUser = await _context.Users.Where(u => u.UserName == UserName && u.Password == Password && u.RoleId == roleid.RoleId).FirstAsync();

        if (loginUser == null)
        {
            TempData["error"] = "Has error, try again";
            return RedirectToAction(nameof(Login)); ;
        }

        HttpContext.Session.SetInt32(SessionKeyAdminId, loginUser.Id);
        HttpContext.Session.SetInt32(SessionKeyAdminRole, loginUser.RoleId);
        HttpContext.Session.SetString(SessionKeyAdminUserName, loginUser.UserName);

        var id = HttpContext.Session.GetInt32(SessionKeyAdminId);
        var role = HttpContext.Session.GetInt32(SessionKeyAdminRole);
        var uname = HttpContext.Session.GetString(SessionKeyAdminUserName);

        _logger.LogInformation($"Session key {roleid.RoleNameEn} ID: {id}");
        _logger.LogInformation($"Session key {roleid.RoleNameEn} Role Id: {role}");
        _logger.LogInformation($"Session key {roleid.RoleNameEn} UName: {uname}");

        loginUser.LastLogin = DateTime.Now;
        _context.Update(loginUser);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index), "Admin");
    }

    public async Task RenewAdminInformation()
    {
        ViewBag.admin = await _context.Users.Where(u => u.Id == HttpContext.Session.GetInt32(SessionKeyAdminId)).FirstAsync();
        // ViewBag.admin = await _context.Users.Where(u => u.Id == 1).FirstAsync();
    }

    public async Task<bool> IsLogin()
    {
        if (HttpContext.Session.GetString(SessionKeyAdminUserName) == null)
        {
            return false;
        }
        await RenewAdminInformation();
        return true;
    }

    public async Task<IActionResult> Login()
    {
        if (await IsLogin())
        {
            _logger.LogInformation("Is login, returning to INDEX view ...");
            return RedirectToAction(nameof(Index));
        }
        TempData["siteKey"] = (await _context.Googlerecaptchas.Where(a => a.HostName == HostName).FirstAsync()).SiteKey;
        System.Console.WriteLine(TempData["siteKey"]);
        return View();
    }

    public async Task<IActionResult> Index()
    {
        if (!await IsLogin())
        {
            _logger.LogInformation("Not Login, returning to login action ...");
            return RedirectToAction(nameof(Login));
        }

        return View();
    }

    public IActionResult LogOut()
    {
        HttpContext.Session.Clear();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Privacy()
    {
        return View();
    }

    // public async Task<IActionResult> Error404()
    // {
    //     // var varActionName = HttpContext.GetRouteValue("Controller");
    //     // System.Console.WriteLine(varActionName.ToString());

    //     var urlReferer = HttpContext.Request.Headers["Referer"].ToString();
    //     System.Console.WriteLine("b: " + urlReferer);

    //     if (urlReferer == null)
    //     {
    //         return View();
    //     }

    //     string a = (string)urlReferer;

    //     string[] splitStr = a.Split("/");
    //     System.Console.WriteLine(splitStr.Length);

    //     foreach (string f in splitStr)
    //     {
    //         System.Console.WriteLine("Index: "+ Array.IndexOf(splitStr,f));
    //         System.Console.WriteLine("String: " + f);
    //         // if (f.Contains("Admin"))
    //     }


    //     string controllerName = "";
    //     if (splitStr.Length > 4 && string.Compare(splitStr[4], "Admin") == 0)
    //     {
    //         controllerName = splitStr[4];
    //     }
    //     else if (splitStr.Length <= 4)
    //     {
    //         controllerName = "Home";
    //     }

    //     System.Console.WriteLine("Controller Name: " + controllerName);
    //     // if (!await IsLogin())
    //     // {
    //     //     _logger.LogInformation("Not Login, returning to login view ...");
    //     //     return RedirectToAction(nameof(Login));;
    //     // }

    //     return View();
    // }

    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View();
        // return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
