using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project_Sem2_WD07_NickVn.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.ComponentModel;

namespace Project_Sem2_WD07_NickVn.Controllers;

public class AdminController : Controller
{
    private readonly ILogger<AdminController> _logger;
    private readonly NickVn_ProjectContext _context;
    private const string SessionKeyAdminId = "_AdminId";
    private const string SessionKeyAdminRole = "_AdminRole";
    private const string SessionKeyAdminUserName = "_AdminUserName";
    private const string HostName = "GoogleReCaptcha";
    private readonly IWebHostEnvironment _hostEnvironment;

    public AdminController(ILogger<AdminController> logger, NickVn_ProjectContext context, IWebHostEnvironment hostEnvironment)
    {
        this._logger = logger;
        this._context = context;
        this._hostEnvironment = hostEnvironment;
    }

    public async Task<IActionResult> Orders(int? page)
    {
        if (!await IsLogin())
        {
            return RedirectToAction(nameof(Index));
        }

        var query = from od in _context.Orders
                    join us in _context.Users on od.UserId equals us.UserId
                    join pd in _context.Lienminhs on od.ProductId equals pd.ProductId
                    orderby od.CreateAt descending
                    select Tuple.Create<Order, User, Lienminh>(od, us, pd);

        int totalPage = 0;
        int totalRecord = 0;
        int pageSize = 9;

        totalRecord = await query.CountAsync();
        totalPage = (totalRecord / pageSize) + ((totalRecord % pageSize) > 0 ? 1 : 0);

        if (page == null || page == 0)
        {
            page = 1;
        }
        else if (page > totalPage)
        {
            page = totalPage;
        }

        var ordersUsersProducts = await query.Skip((Convert.ToInt32(page) - 1) * pageSize)
                                                    .Take(pageSize).ToListAsync();

        ViewBag.totalPage = totalPage;
        ViewBag.currentPage = page;

        // var ordersUsersProducts = await query.ToListAsync();
        // var orders = await _context.Orders.OrderByDescending(a => a.CreateAt).ToListAsync();

        ViewBag.ordersUsersProducts = ordersUsersProducts;
        return View();
    }

    public async Task<IActionResult> GenerateProductData(int? s)
    {
        if (!await IsLogin())
        {
            return RedirectToAction(nameof(Index));
        }

        string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghiklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        decimal RandomDecimal(int min, int max)
        {
            Random rd = new Random();
            return rd.NextInt64(min, max);
        }

        var watch = System.Diagnostics.Stopwatch.StartNew();
        int? dataCount = s;
        // Define number of data
        if (dataCount == null || dataCount.Equals(null))
        {
            dataCount = 3;
        }
        Random random = new Random();
        string[] rankRandomArray = { "Ch??a Rank", "S???t", "?????ng", "B???c", "V??ng", "B???ch Kim", "Kim C????ng", "Cao Th???", "?????i Cao Th???", "Th??ch ?????u" };

        string[] urlImage ={
            @"/storage/images/0qBPw7AiOQ_1632531413.jpg",
            @"/storage/images/CM9q56zAnM_1632531413.jpg",
            @"/storage/images/pTWDgoJQuz_1632531413.jpg",
            @"/storage/images/zwRCsqMtyo_1632531413.jpg",
            @"/storage/images/HLpEr7ojZm_1632531414.jpg",
            @"/storage/images/xxuC88f0h9_1632531414.jpg"
        };
        int urlImageLength = urlImage.Length;

        string[] urlImgThumb = {
            @"/storage/images/default-thumb1.webp",
            @"/storage/images/default-thumb2.webp",
            @"/storage/images/default-thumb3.webp",
            @"/storage/images/default-thumb4.webp",
            @"/storage/images/default-thumb5.webp",
            @"/storage/images/default-thumb6.webp",
            @"/storage/images/0qBPw7AiOQ_1632531413.jpg"
        };
        int urlImgThumbLength = urlImgThumb.Length;

        const int NOT_SOLD = 1003;
        // const int SOLD = 1004;
        // int[] status_id = { NOT_SOLD, SOLD };

        int lienMinhID;
        var lastLienMinhID = await _context.Lienminhs.OrderByDescending(a => a.ProductId).FirstOrDefaultAsync();
        if (lastLienMinhID == null)
        {
            lienMinhID = 1;
        }
        else
        {
            lienMinhID = lastLienMinhID.ProductId;
        }

        int lastImgID;
        var varimg = await _context.Images.OrderByDescending(b => b.ImgId).FirstOrDefaultAsync();
        if (varimg == null)
        {
            lastImgID = 1;
        }
        else
        {
            lastImgID = varimg.ImgId;
        }

        // Generate `lienminh` clone data
        lienMinhID = 3455;
        for (int i = 0; i <= dataCount; i++)
        {
            lienMinhID++;
            Lienminh newProduct = new Lienminh();

            newProduct.ProductId = lienMinhID;
            newProduct.ProductCategoryId = 1;
            newProduct.Name = "Li??n Minh";
            newProduct.StatusAccount = "shop" + RandomString(6);
            newProduct.ProductUserPassword = "passw" + RandomString(8);
            newProduct.Publisher = "Garena";
            newProduct.PriceAtm = RandomDecimal(10000, int.MaxValue / 100);
            newProduct.Champ = (int)RandomDecimal(1, 150);
            newProduct.Skin = (int)RandomDecimal(1, 555);
            newProduct.Rank = rankRandomArray[random.Next(0, rankRandomArray.Length)];
            newProduct.StatusAccount = "Tr???ng Th??ng Tin";
            newProduct.Note = "none";
            newProduct.ImgThumb = urlImgThumb[(int)RandomDecimal(0, urlImgThumbLength)];
            newProduct.StatusId = NOT_SOLD;

            // Generate clone image path
            for (int j = 0; j < urlImageLength; j++)
            {
                lastImgID++;
                Image productImage = new Image();

                productImage.ImgId = lastImgID;
                productImage.ProductId = lienMinhID;
                productImage.ImgLink = urlImage[j];

                // Add image path clone
                await _context.Images.AddAsync(productImage);
            }

            // Add data lien minh
            await _context.Lienminhs.AddAsync(newProduct);

            // Save data to database
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

    // public async Task<IActionResult> EditUserSolve(
    //     int? id, string? FirstName, string? LastName,
    //     string? Password, string? Phone, string? Email,
    //     int? role_id, int? status_id, string? Note,
    //     IFormFile? newCover, IFormFile? newAvatar
    //     )
    // {
    //     if(!await IsLogin())
    //     {
    //         return RedirectToAction(nameof(Index));
    //     }

    //     if (id == null)
    //     {
    //         TempData["err"] = "Can not find user";
    //         return RedirectToAction(nameof(Users));
    //     }

    //     var currentUserId = HttpContext.Session.GetInt32(SessionKeyAdminId);

    //     var user = await _context.Users.Where(a => a.UserId == id).FirstOrDefaultAsync();
    //     if (user == null)
    //     {
    //         TempData["err"] = "Can not find user";
    //         return RedirectToAction(nameof(Users));
    //     }
    //     User oldUser = user;
    //     user.FirstName = FirstName == null ? user.FirstName : (string)FirstName;
    //     user.LastName = LastName == null ? user.LastName : (string)LastName;
    //     user.Password = Password == null ? user.Password : MD5.CreateMD5((string)Password);
    //     user.Phone = Phone == null ? user.Phone : (string)Phone;
    //     user.Email = Email == null ? user.Email : (string)Email;
    //     user.Note = Note == null ? user.Note : (string)Note;

    //     if(role_id != null || status_id != null)
    //     {
    //         if(id == 1)
    //         {
    //             TempData["error"] = "Can not change status or role of super admin";
    //             return RedirectToAction(nameof(EditUser), new {id = id});
    //         }

    //         if(currentUserId == id)
    //         {
    //             TempData["error"] = "Can not change status or role of myself";
    //             return RedirectToAction(nameof(EditUser), new {id = id});
    //         }
    //     }

    //     user.RoleId = role_id == null ? user.RoleId : (int)role_id;
    //     user.StatusId = status_id == null ? user.StatusId : (int)status_id;

    //     if (newAvatar != null)
    //     {
    //         try
    //         {
    //             int lastImgID = (await _context.Images.OrderBy(a => a.ImgId).LastAsync()).ImgId;
    //             string wwwRootPath = _hostEnvironment.WebRootPath;
    //             string fileName;
    //             string extension;
    //             string path;
    //             string slashImg = @"\storage\images";
    //             Image image;
    //             IFormFile posted = newAvatar;

    //             fileName = Path.GetFileNameWithoutExtension(posted.FileName);
    //             extension = Path.GetExtension(posted.FileName);
    //             fileName = fileName + "_" + DateTime.Now.ToString("yymmssfff") + extension;
    //             // path = Path.Combine("/img", fileName);
    //             path = Path.Combine(wwwRootPath + slashImg, fileName);
    //             using (var fileStream = new FileStream(path, FileMode.Create))
    //             {
    //                 await posted.CopyToAsync(fileStream);
    //             }

    //             // Set new image
    //             image = new Image();
    //             image.ImgId = lastImgID + 1;
    //             image.ProductId = 0;
    //             image.ImgLink = Path.Combine(slashImg, fileName);

    //             user.ImgSrc = Path.Combine(slashImg, fileName);

    //             // Set path image
    //             _context.Images.Add(image);
    //         }
    //         catch (Exception ex)
    //         {
    //             _logger.LogWarning(ex.Message);
    //             TempData["err"] = "  Error occur when update image";
    //             return RedirectToAction(nameof(EditUser));
    //         }
    //     }

    //     if (newCover != null)
    //     {
    //         try
    //         {
    //             int lastImgID = (await _context.Images.OrderBy(a => a.ImgId).LastAsync()).ImgId;
    //             string wwwRootPath = _hostEnvironment.WebRootPath;
    //             string fileName;
    //             string extension;
    //             string path;
    //             string slashImg = @"\storage\images";
    //             Image image;
    //             IFormFile posted = newCover;

    //             fileName = Path.GetFileNameWithoutExtension(posted.FileName);
    //             extension = Path.GetExtension(posted.FileName);
    //             fileName = fileName + "_" + DateTime.Now.ToString("yymmssfff") + extension;
    //             // path = Path.Combine("/img", fileName);
    //             path = Path.Combine(wwwRootPath + slashImg, fileName);
    //             using (var fileStream = new FileStream(path, FileMode.Create))
    //             {
    //                 await posted.CopyToAsync(fileStream);
    //             }

    //             // Set new image
    //             image = new Image();
    //             image.ImgId = lastImgID + 1;
    //             image.ProductId = 0;
    //             image.ImgLink = Path.Combine(slashImg, fileName);

    //             // Set path image
    //             user.CoverImgSrc = Path.Combine(slashImg, fileName);
    //             _context.Images.Add(image);
    //         }
    //         catch (Exception ex)
    //         {
    //             _logger.LogWarning(ex.Message);
    //             TempData["err"] = "  Error occur when update image";
    //             return RedirectToAction(nameof(EditUser));
    //         }
    //     }
    //     // If user have change, update time
    //     if(!oldUser.Equals(user))
    //     {
    //         user.UpdateAt = DateTime.Now;
    //     }

    //     _context.Users.Update(user);
    //     await _context.SaveChangesAsync();
    //     TempData["success"] = "Edit user success!";

    //     return RedirectToAction(nameof(EditUser), new {id = id});
    // }

    // public async Task<IActionResult> EditUser(int? id)
    // {
    //     if (!await IsLogin())
    //     {
    //         return RedirectToAction(nameof(Index));
    //     }
    //     if (id == null)
    //     {
    //         TempData["err"] = "Can not found user";
    //         return RedirectToAction(nameof(Users));
    //     }

    //     var user = await _context.Users.Where(a => a.UserId == id).FirstOrDefaultAsync();
    //     if (user == null)
    //     {
    //         TempData["err"] = "Can not found user";
    //         return RedirectToAction(nameof(Users));
    //     }

    //     var listRole = await _context.Roles.OrderBy(b => b.RoleId).ToListAsync();
    //     var listStatus = await _context.Statuses.OrderBy(b => b.StatusId).ToListAsync();

    //     ViewBag.listStatus = listStatus;
    //     ViewBag.listRole = listRole;
    //     ViewBag.user = user;

    //     return View();
    // }

    public async Task<IActionResult> DetailUser(int? id)
    {
        if (!await IsLogin())
        {
            return RedirectToAction(nameof(Index));
        }
        if (id == null)
        {
            TempData["err"] = "Can not found user";
            return RedirectToAction(nameof(Users));
        }

        var user = await _context.Users.Where(a => a.UserId == id).FirstOrDefaultAsync();
        if (user == null)
        {
            TempData["err"] = "Can not found user";
            return RedirectToAction(nameof(Users));
        }

        var listRole = await _context.Roles.OrderBy(b => b.RoleId).ToListAsync();
        var listStatus = await _context.Statuses.OrderBy(b => b.StatusId).ToListAsync();

        ViewBag.listStatus = listStatus;
        ViewBag.listRole = listRole;
        ViewBag.user = user;
        return View();
    }

    public async Task<IActionResult> ChangeUserStatus(int? userId, int? statusId)
    {
        string? statusName = string.Empty;
        if (!await IsLogin())
        {
            return RedirectToAction(nameof(Index));
        }
        if (statusId == null)
        {
            TempData["ban-active-msg"] = "Can not find status";
            return RedirectToAction(nameof(Users));
        }
        if (userId == null)
        {
            TempData["ban-active-msg"] = "Can not find user";
            return RedirectToAction(nameof(Users));
        }

        int? currentUserId = HttpContext.Session.GetInt32(SessionKeyAdminId);
        if (currentUserId == null)
        {
            TempData["ban-active-msg"] = "Can not change status";
            return RedirectToAction(nameof(Users));
        }

        if (currentUserId == userId && statusId == 1006)
        {
            TempData["ban-active-msg"] = "You can not ban yourself";
            return RedirectToAction(nameof(Users));
        }

        var user = await _context.Users.Where(a => a.UserId == userId).FirstOrDefaultAsync();
        if (user == null)
        {
            TempData["ban-active-msg"] = "Can not find user";
            return RedirectToAction(nameof(Users));
        }

        user.StatusId = (int)statusId;
        statusName = (await _context.Statuses.Where(a => a.StatusId == user.StatusId).FirstAsync()).StatusNameEn;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        TempData["ban-active-msg"] = $"{statusName} user {userId} successfull!";
        return RedirectToAction(nameof(Users));
    }

    public async Task<IActionResult> Users(int? page)
    {
        if (!await IsLogin())
        {
            return RedirectToAction(nameof(Index));
        }
        var query = from us in _context.Users
                    orderby us.UserId
                    select us;

        // var listUser = await _context.Users.OrderBy(a => a.Id).ToListAsync();

        int totalPage = 0;
        int totalRecord = 0;
        int pageSize = 9;

        totalRecord = await query.CountAsync();
        totalPage = (totalRecord / pageSize) + ((totalRecord % pageSize) > 0 ? 1 : 0);

        if (page == null || page == 0)
        {
            page = 1;
        }
        else if (page > totalPage)
        {
            page = totalPage;
        }

        var listUser = await query.Skip((Convert.ToInt32(page) - 1) * pageSize)
                                                    .Take(pageSize).ToListAsync();

        var listRole = await _context.Roles.OrderBy(b => b.RoleId).ToListAsync();
        var listStatus = await _context.Statuses.OrderBy(b => b.StatusId).ToListAsync();

        ViewBag.listStatus = listStatus;
        ViewBag.listRole = listRole;
        ViewBag.listUser = listUser;
        ViewBag.totalPage = totalPage;
        ViewBag.currentPage = page;

        return View();
    }

    public async Task<IActionResult> AddProductSolve(
        string? name, string? ProductUserName,
        string? ProductPassword, string? Publisher, decimal? PriceAtm,
        int? Skin, int? Champ, string? Rank,
        string? StatusAccount, int? Status, string? note,
        IFormFileCollection? ImageCollection
        )
    {
        if (!await IsLogin())
        {
            return RedirectToAction(nameof(Index));
        }

        bool isNullEmptyWhitespace(string? input)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input) || input == null)
            {
                return true;
            }
            return false;
        }

        // Return err msg if required field is null, otherwise add ...
        if (isNullEmptyWhitespace(name) || isNullEmptyWhitespace(ProductUserName)
            || isNullEmptyWhitespace(ProductPassword) || isNullEmptyWhitespace(Publisher)
            || isNullEmptyWhitespace(Rank) || isNullEmptyWhitespace(StatusAccount)
        )
        {
            TempData["error-add"] = "Field can not be null or white space";
            return RedirectToAction(nameof(AddProduct));
        }

        // Set default if not input btw
        if (Skin == null)
            Skin = 1;
        if (Champ == null)
            Champ = 1;
        if (Status == null)
            Status = 1001;
        if (PriceAtm == null)
            PriceAtm = 0;
        if (isNullEmptyWhitespace(note)) // None required field
            note = "none";

        if (ImageCollection == null || ImageCollection.Count() <= 0)
        {
            TempData["err-add"] = "Image is blank!";
            return RedirectToAction(nameof(AddProduct));
        }

        foreach (var a in ImageCollection)
        {
            System.Console.WriteLine($"ANH ne:  {a.FileName}");
        }

        // Check ProductUserName is exists
        bool isExistsProdUserName = await _context.Lienminhs.AnyAsync(a => a.ProductUserName == ProductUserName);
        if (isExistsProdUserName)
        {
            TempData["err-add"] = "This account is already exists";
            return RedirectToAction(nameof(AddProduct));
        }

        List<Image> listImage = new List<Image>();
        Lienminh product = new Lienminh();

        // This section is check possible null above, then temporary disable warning
#pragma warning disable CS8601
        product.Name = name;
        product.ProductUserName = ProductUserName;
        product.ProductUserPassword = ProductPassword;
        product.Publisher = Publisher;
        product.ProductCategoryId = 1;
        product.Rank = Rank;
        product.StatusAccount = StatusAccount;
#pragma warning restore CS8601
        product.Skin = (int)Skin;
        product.Champ = (int)Champ;
        product.StatusId = (int)Status;
        product.PriceAtm = (decimal)PriceAtm;
        product.Note = note;

        int lastProductId = (await _context.Lienminhs.OrderBy(a => a.ProductId).LastAsync()).ProductId;
        int lastImgID = (await _context.Images.OrderBy(a => a.ImgId).LastAsync()).ImgId;

        product.ProductId = lastProductId + 1;
        try
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName;
            string extension;
            string path;
            string slashImg = @"\storage\images";
            bool firstIsThumbnail = true; // if true it will set a first image to thumbnail
            Image image;
            foreach (var posted in ImageCollection)
            {
                fileName = Path.GetFileNameWithoutExtension(posted.FileName);
                extension = Path.GetExtension(posted.FileName);
                fileName = fileName + "_" + DateTime.Now.ToString("yymmssfff") + extension;
                // path = Path.Combine("/img", fileName);
                path = Path.Combine(wwwRootPath + slashImg, fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await posted.CopyToAsync(fileStream);
                }

                if (firstIsThumbnail)
                {
                    firstIsThumbnail = false;
                    product.ImgThumb = Path.Combine(slashImg, fileName);
                }

                lastImgID++;
                // Set new image
                image = new Image();
                image.ImgId = lastImgID;
                image.ProductId = product.ProductId;
                image.ImgLink = Path.Combine(slashImg, fileName);

                // Push image to list image
                listImage.Add(image);

                // _logger.LogInformation("IMG URL local: " + image.ImgLink);
            }

            // _logger.LogInformation("IMG URL: " + product.ImagePath);

            await _context.Images.AddRangeAsync(listImage);
            await _context.Lienminhs.AddAsync(product);
            await _context.SaveChangesAsync();

            TempData["success-add"] = "Add product success, new product id: " + product.ProductId;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex.Message);
            TempData["err-add"] = "Error occur, please try again later!";
            return RedirectToAction(nameof(AddProduct));
        }

        return RedirectToAction(nameof(AddProduct));
    }

    public async Task<IActionResult> AddProduct()
    {
        if (!await IsLogin())
        {
            return RedirectToAction(nameof(Index));
        }
        var statusList = await _context.Statuses.OrderBy(a => a.StatusId).ToListAsync();
        ViewBag.statusList = statusList;
        return View();
    }

    public async Task<IActionResult> EditProductSolve(
        int id,
        string? name, string? ProductUserName,
        string? ProductPassword, string? Publisher, decimal? PriceAtm,
        int? Skin, int? Champ, string? Rank,
        string? StatusAccount, int? Status, string? note,
        IFormFile? ImgThumb, IFormFileCollection? ListImg
        )
    {
        if (!await IsLogin())
        {
            return RedirectToAction(nameof(Index));
        }

        if (id == 0)
        {
            TempData["err"] = "Error occur, please try again";
            return Redirect(Request.Headers["Referer"].ToString());
        }

        var product = await _context.Lienminhs.Where(a => a.ProductId == id).FirstOrDefaultAsync();
        if (product == null)
        {
            TempData["err"] = "Can not find product with id: " + id;
            return Redirect(Request.Headers["Referer"].ToString());
        }

        product.Name = name == null ? product.Name : name;
        product.ProductUserName = ProductUserName == null ? product.ProductUserName : ProductUserName;
        product.ProductUserPassword = ProductPassword == null ? product.ProductUserPassword : ProductPassword;
        product.Publisher = Publisher == null ? product.Publisher : Publisher;
        product.PriceAtm = PriceAtm == null ? product.PriceAtm : (decimal)PriceAtm;
        product.Skin = Skin == null ? product.Skin : (int)Skin;
        product.Champ = Champ == null ? product.Champ : (int)Champ;
        product.Rank = Rank == null ? product.Rank : Rank;
        product.StatusAccount = StatusAccount == null ? product.StatusAccount : StatusAccount;
        product.StatusId = Status == null ? product.StatusId : (int)Status;
        product.Note = note == null ? product.Note : note;

        // if (ImgThumb != null || ListImg != null)
        // {
        //     string wwwRootPath = _hostEnvironment.WebRootPath;
        //     string fileName;
        //     string extension;
        //     string path;
        //     string slashImg = @"\storage\images";
        //     bool firstIsThumbnail = true; // if true it will set a first image to thumbnail
        //     Image image;

        //     foreach (var posted in ImageCollection)
        //     {
        //         fileName = Path.GetFileNameWithoutExtension(posted.FileName);
        //         extension = Path.GetExtension(posted.FileName);
        //         fileName = fileName + "_" + DateTime.Now.ToString("yymmssfff") + extension;
        //         // path = Path.Combine("/img", fileName);
        //         path = Path.Combine(wwwRootPath + slashImg, fileName);
        //         using (var fileStream = new FileStream(path, FileMode.Create))
        //         {
        //             await posted.CopyToAsync(fileStream);
        //         }

        //         if (firstIsThumbnail)
        //         {
        //             firstIsThumbnail = false;
        //             product.ImgThumb = Path.Combine(slashImg, fileName);
        //         }

        //         lastImgID++;
        //         // Set new image
        //         image = new Image();
        //         image.ImgId = lastImgID;
        //         image.LienminhId = product.Id;
        //         image.ImgLink = Path.Combine(slashImg, fileName);

        //         // Push image to list image
        //         listImage.Add(image);

        //         // _logger.LogInformation("IMG URL local: " + image.ImgLink);
        //     }
        // }

        _context.Update(product);
        await _context.SaveChangesAsync();
        TempData["success"] = "Edit products success";

        return Redirect(Request.Headers["Referer"].ToString());
    }

    public async Task<IActionResult> EditProduct(int id)
    {
        if (!await IsLogin())
        {
            return RedirectToAction(nameof(Index));
        }

        var product = await _context.Lienminhs.Where(a => a.ProductId == id).FirstOrDefaultAsync();
        if (product == null)
        {
            TempData["error"] = $"Can not find product id: {id}";
            return RedirectToAction(nameof(Products));
        }

        List<string> listRank = new List<string> { "Ch??a Rank", "S???t", "?????ng", "B???c", "V??ng", "B???ch Kim", "Kim C????ng", "Cao Th???", "?????i Cao Th???", "Th??ch ?????u" };
        var listStatus = await _context.Statuses.OrderBy(a => a.StatusId).ToListAsync();

        var listImage = await _context.Images.Where(a => a.ProductId == product.ProductId).OrderBy(b => b.ImgId).ToListAsync();

        ViewBag.listRank = listRank;
        ViewBag.product = product;
        ViewBag.listStatus = listStatus;
        ViewBag.listImage = listImage;

        return View(nameof(EditProduct));
    }

    public async Task<IActionResult> DeleteProduct(int id)
    {
        if (!await IsLogin())
        {
            return RedirectToAction(nameof(Index));
        }

        var searchId = await _context.Lienminhs.AnyAsync(c => c.ProductId == id);
        if (!searchId)
        {
            _logger.LogInformation("Can not delete unknown ID: " + id);
            TempData["error"] = "Can not delete unknown ID: " + id;
            return Redirect(Request.Headers["Referer"].ToString());
        }

        var find = await _context.Lienminhs.Where(c => c.ProductId == id).FirstOrDefaultAsync();
        if (find == null)
        {
            TempData["error"] = "Can not delete unknown ID: " + id;
            return RedirectToAction(nameof(Products));
        }
        var status_DEL = await _context.Statuses.Where(a => a.StatusNameEn == "Deleted").FirstOrDefaultAsync();
        if (status_DEL == null)
        {
            TempData["error"] = "Can not get list of status";
            return RedirectToAction(nameof(Products));
        }
        int DELETED = status_DEL.StatusId;
        if (find.StatusId == DELETED)
        {
            TempData["error"] = "This product already deleted";
            return Redirect(Request.Headers["Referer"].ToString());
        }

        find.StatusId = DELETED;

        _context.Lienminhs.Update(find);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Products));
    }

    public async Task<IActionResult> DetailProduct(int? id)
    {
        if (!await IsLogin())
        {
            return RedirectToAction(nameof(Index));
        }
        if (id == null)
        {
            TempData["error"] = "Not Found this ID";
            return RedirectToAction(nameof(Products));
        }

        var DeltailProductByID = await _context.Lienminhs.Where(c => c.ProductId == id).FirstOrDefaultAsync();
        if (DeltailProductByID == null)
        {
            TempData["error"] = "Not Found this ID";
            return RedirectToAction(nameof(Products));
        }
        var imgList = await _context.Images.Where(a => a.ProductId == DeltailProductByID.ProductId).ToListAsync();
        var status_en = await _context.Statuses.Where(c => c.StatusId == DeltailProductByID.StatusId).FirstAsync();
        if (status_en == null)
        {
            TempData["error"] = "Not Found this ID";
            return RedirectToAction(nameof(Products));
        }

        ViewBag.DeltailProductByID = DeltailProductByID;
        ViewBag.imgList = imgList;
        ViewBag.statusName = status_en.StatusNameEn;

        return View();
    }

    public async Task<IActionResult> Products(int? page, string? SearchKey, int? id, decimal? price, int? status)
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
                    join sts in _context.Statuses on lienMinh.StatusId equals sts.StatusId
                    where lienMinh.Name.Contains(SearchKey == null ? string.Empty : SearchKey)
                    && (lienMinh.PriceAtm >= priceSearchMin && lienMinh.PriceAtm < priceSearchMax)
                    orderby lienMinh.ProductId descending
                    select Tuple.Create<Lienminh, Status>(lienMinh, sts);

        // var query = from lienMinh in _context.Lienminhs
        //             where lienMinh.Name.Contains(SearchKey == null ? string.Empty : SearchKey)
        //             && (lienMinh.PriceAtm >= priceSearchMin && lienMinh.PriceAtm < priceSearchMax)
        //             select lienMinh;

        int totalPage = 0;
        int totalRecord = 0;
        int pageSize = 9;

        totalRecord = await query.CountAsync();
        totalPage = (totalRecord / pageSize) + ((totalRecord % pageSize) > 0 ? 1 : 0);

        if (page == null || page == 0)
        {
            page = 1;
        }
        else if (page > totalPage)
        {
            page = totalPage;
        }

        List<Tuple<Lienminh, Status>> listLienMinhAcc = await query.Skip((Convert.ToInt32(page) - 1) * pageSize)
                                                    .Take(pageSize).ToListAsync();

        if (status != null)
        {
            listLienMinhAcc = listLienMinhAcc.Where(a => a.Item1.StatusId == status).ToList();
        }

        ViewBag.listLienMinhAcc = listLienMinhAcc;
        ViewBag.totalPage = totalPage;
        ViewBag.currentPage = page;
        return View();
    }

    //Delete Category
    public async Task<IActionResult> DeleteCategory(int? id)
    {
        if (!await IsLogin())
        {
            return RedirectToAction(nameof(Index));
        }
        if (id == null)
        {
            TempData["err"] = "Can not found category";
            return RedirectToAction(nameof(Categories));
        }
        var searchId = await _context.Categories.AnyAsync(c => c.CategoryId == id);
        if (!searchId)
        {
            _logger.LogInformation("Can not delete unknown ID: " + id);
            return Redirect(Request.Headers["Referer"].ToString());
        }

        var find = await _context.Categories.Where(c => c.CategoryId == id).FirstOrDefaultAsync();
        if (find == null)
        {
            TempData["err"] = "Can not found category";
            return RedirectToAction(nameof(Categories));
        }
        var status_DEL = await _context.Statuses.Where(a => a.StatusNameEn == "Deleted").FirstOrDefaultAsync();
        if (status_DEL == null)
        {
            TempData["err"] = "Can not found category";
            return RedirectToAction(nameof(Categories));
        }
        find.Status = status_DEL.StatusId;

        _context.Categories.Update(find);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Categories));
    }

    public async Task<IActionResult> EditCategorySolve(int? id, string? Name, string? Title, string? ActionString, int? Total, string? Note, IFormFile? ImgSaleOff, IFormFile? ImgSrc)
    {
        if (!await IsLogin())
        {
            return RedirectToAction(nameof(Index));
        }
        if (id == null)
        {
            TempData["err"] = "Can not found category";
            return RedirectToAction(nameof(Categories));
        }
        Category? otherCategory = new Category();
        try
        {
            otherCategory = await _context.Categories.Where(c => c.CategoryId == id).FirstOrDefaultAsync();
            if (otherCategory == null)
            {
                TempData["err"] = "Can not found category";
                return RedirectToAction(nameof(Categories));
            }
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
            _logger.LogInformation("Edit/Update Category successfull \n\tCategoryID: " + otherCategory.CategoryId);

        }
        catch (Exception ex)
        {
            _logger.LogInformation("Edit Category Exeception: {0}", ex.Message);
            return RedirectToAction(nameof(Categories));
        }

        return RedirectToAction(nameof(DetailCategory), new { id = otherCategory.CategoryId });
    }

    public async Task<IActionResult> EditCategory(int? id)
    {
        if (!await IsLogin())
        {
            return RedirectToAction(nameof(Index));
        }
        if (id == null)
        {
            TempData["error"] = "Not found this category";
            return RedirectToAction(nameof(DetailCategory));
        }

        var selectedCategory = await _context.Categories.Where(c => c.CategoryId == id).FirstOrDefaultAsync();
        if (selectedCategory == null)
        {
            TempData["error"] = "Not found this category";
            return RedirectToAction(nameof(DetailCategory));
        }

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
            TempData["error"] = "Not Found this category";
            return RedirectToAction(nameof(Categories));
        }

        var cateById = await _context.Categories.Where(c => c.CategoryId == id).FirstOrDefaultAsync();
        if (cateById == null)
        {
            TempData["error"] = "Not Found this category";
            return RedirectToAction(nameof(Categories));
        }
        var productByCateId = await _context.ProductCategories.Where(c => c.CategoryId == id).ToListAsync();

        ViewBag.cateById = cateById;
        ViewBag.productByCateId = productByCateId;

        return View();
    }

    // Categories page start
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

        List<Category> listCateQuery = await query.OrderBy(acc => acc.CategoryId)
                                                    .Skip((page - 1) * pageSize)
                                                    .Take(pageSize).ToListAsync();

        ViewBag.listCateQuery = listCateQuery;
        ViewBag.totalPage = totalPage;
        ViewBag.currentPage = page;
        return View();
    }

    // Categories page end

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
            ModelState.AddModelError(string.Empty, "X??c minh b???n kh??ng ph???i l?? robot");
            // ViewBag.Error = "X??c minh b???n kh??ng ph???i l?? robot";
            TempData["error"] = "X??c minh b???n kh??ng ph???i l?? robot";
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
        var roleid = await _context.Roles.Where(a => a.RoleNameEn == "Admin").FirstOrDefaultAsync();
        if (roleid == null)
        {
            TempData["error"] = "Can not get list of role";
            return RedirectToAction(nameof(Login));
        }

        if (!await _context.Users.AnyAsync(u => u.UserName == UserName && u.Password == Password && u.RoleId == roleid.RoleId))
        {
            TempData["error"] = "Username or Password is incorrect";
            return RedirectToAction(nameof(Login));
        }

        var loginUser = await _context.Users.Where(u => u.UserName == UserName && u.Password == Password && u.RoleId == roleid.RoleId).FirstOrDefaultAsync();

        if (loginUser == null)
        {
            TempData["error"] = "Has error, try again";
            return RedirectToAction(nameof(Login)); ;
        }

        // Get status list: Ban
        int ban_status_id = (await _context.Statuses.Where(a => a.StatusNameEn == "Ban").FirstAsync()).StatusId;
        if (loginUser.StatusId == ban_status_id)
        {
            TempData["error"] = "Your account is banned";
            return RedirectToAction(nameof(Login)); ;
        }

        HttpContext.Session.SetInt32(SessionKeyAdminId, loginUser.UserId);
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

    public async Task<bool> IsLogin()
    {
        //  Check login and renew information of admin
        if (HttpContext.Session.GetString(SessionKeyAdminUserName) == null)
        {
            return false;
        }

        var RenewAdminInformation = await _context.Users.Where(u => u.UserId == HttpContext.Session.GetInt32(SessionKeyAdminId)).FirstOrDefaultAsync();
        if (RenewAdminInformation == null)
        {
            HttpContext.Session.Remove(SessionKeyAdminId);
            HttpContext.Session.Remove(SessionKeyAdminRole);
            HttpContext.Session.Remove(SessionKeyAdminUserName);

            TempData["err"] = "Error occur when login";
            return false;
        }

        ViewBag.admin = RenewAdminInformation;
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

        // var dataOrders = _context.Orders.OrderBy(a => a.CreateAt).ToListAsync();
        // Note REVENUE > SALES

        // Get data for chart
        // L???y d??? li???u cho bi???u ?????
        // var query = from od in _context.Orders
        //             join pd in _context.Lienminhs on od.ProductId equals pd.ProductId
        //             select Tuple.Create<Order, Lienminh>(od, pd);

        var queryOrders = from order in _context.Orders
                          join product in _context.Lienminhs on order.ProductId equals product.ProductId
                          select new
                          {
                            Order = order,
                            Lienminh = product
                          };

        var queryChargeHistory = from ChargeHistory in _context.ChargeHistories
                                 select ChargeHistory;

        // int count = await queryOrders.CountAsync();
        var listOrders = await queryOrders.ToListAsync();
        var listCharge = await queryChargeHistory.ToListAsync();

        // Sort by Descending date
        listOrders.Sort((a, b) => b.Order.CreateAt.CompareTo(a.Order.CreateAt));
        listCharge.Sort((a, b) => b.CreateAt.CompareTo(a.CreateAt));

        DateTime today = DateTime.Now.Date;
        decimal todayRevenue = listOrders.Where(a => a.Order.CreateAt.Date == today).Sum(s => s.Lienminh.PriceAtm);
        decimal totalRevenue = listOrders.Sum(s => s.Lienminh.PriceAtm);
        decimal todaySale = Math.Round(todayRevenue / 100 * 35);
        decimal totalSale = Math.Round(totalRevenue / 100 * 35);

        decimal todayCharge = listCharge.Where(a => a.CreateAt.Date == today).Sum(a => a.MoneyReceived);
        decimal totalCharge = listCharge.Sum(a => a.MoneyReceived);

        int thisMonth = DateTime.Now.Month;
        int thisYear = DateTime.Now.Year;

        int startMonth = 0;
        int startYear = 0;
        int THANG12 = 12;

        if (thisMonth <= 8)
        {
            int temp = 8 - thisMonth;
            startMonth = THANG12 - temp;
            startYear = thisYear - 1;
        }
        else
        {
            startMonth = thisMonth - 8;
            startYear = thisYear;
        }
        // Lay 8 thang gan nhat
        string labels = "[";
        // Data bieu do cho doanh thu
        string dataRevenues = "[";
        // Data bieu do cho sale
        string dataSales = "[";
        // Data bieu do cho tong so nap the
        string ChargeHistoryDataChart = "[";
        for (int i = 0; i <= 8; i++)
        {
            if (startMonth > 12)
            {
                startMonth = 1;
                startYear++;
            }
            // Tong doanh thu trong thang
            decimal thisMonthTotalRevenue = listOrders.Where(a => a.Order.CreateAt.Month == startMonth).Sum(b => b.Lienminh.PriceAtm);
            // Tong so tien nap trong thang
            decimal thisMonthTotalCharge = listCharge.Where(b => b.CreateAt.Month == startMonth).Sum(c => c.MoneyReceived);
            // Add data to char data
            dataRevenues += string.Format($"\"{thisMonthTotalRevenue}\",");
            dataSales += string.Format($"\"{Math.Round(thisMonthTotalRevenue / 100 * 35)}\",");
            ChargeHistoryDataChart += string.Format($"\"{thisMonthTotalCharge}\",");
            labels += string.Format($"\"{startMonth}/{startYear}\",");
            startMonth++;
        }
        labels += "]";
        dataRevenues += "]";
        dataSales += "]";
        ChargeHistoryDataChart += "]";

        System.Console.WriteLine(ChargeHistoryDataChart);

        List<Order> recentSales = new List<Order>();
        foreach(var i in listOrders)
        {
            if(recentSales.Count < 5)
            {
                recentSales.Add(i.Order);
            }
        }


        // System.Console.WriteLine($"LIST ORDER COUNT: {recentSales.Count}");
        // foreach (var a in recentSales)
        // {
        //     System.Console.WriteLine($"Fild: {a.ProductId}");
        // }

        // All order
        ViewData["listOderProd"] = listOrders;
        // Da ban gan day
        ViewData["recentSales"] = recentSales;

        // Data of sale
        ViewData["todaySale"] = todaySale;
        ViewData["totalSale"] = totalSale;

        // Data of revenue
        ViewData["todayRevenue"] = todayRevenue;
        ViewData["totalRevenue"] = totalRevenue;

        // Data of charge
        ViewData["todayCharge"] = todayCharge;
        ViewData["totalCharge"] = totalCharge;

        // 8 thang gan nhat
        ViewData["labels"] = labels;

        // Data bieu do
        ViewData["dataSales"] = dataSales;
        ViewData["dataRevenues"] = dataRevenues;
        ViewData["ChargeHistoryDataChart"] = ChargeHistoryDataChart;

        return View();
    }

    public async Task<IActionResult> LogOut()
    {
        if (!await IsLogin())
        {
            return RedirectToAction(nameof(Index));
        }
        HttpContext.Session.Remove(SessionKeyAdminId);
        HttpContext.Session.Remove(SessionKeyAdminRole);
        HttpContext.Session.Remove(SessionKeyAdminUserName);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Privacy()
    {
        return View();
    }

    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View();
        // return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
