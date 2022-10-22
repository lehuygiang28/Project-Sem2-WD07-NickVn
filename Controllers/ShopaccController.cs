using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project_Sem2_WD07_NickVn.Models;
using Microsoft.EntityFrameworkCore;

namespace Project_Sem2_WD07_NickVn.Controllers;

public class ShopaccController : Controller
{
    private readonly ILogger<ShopaccController> _logger;
    private readonly NickVn_ProjectContext _context;

    public const int SOLD = 1004;
    public const int NOT_SOLD = 1003;

    public ShopaccController(ILogger<ShopaccController> logger, NickVn_ProjectContext context)
    {
        _logger = logger;
        _context = context;
    }

    private async Task<List<Status>> GetStatusList()
    {
        var list = await _context.Statuses.OrderBy(a => a.StatusId).ToListAsync();
        return list;
    }

    public async Task<IActionResult> BuyConfirmSolve(int productId)
    {
        TempData["error"] = null;
        TempData["success"] = null;
        // True if is logged in otherwise false
        bool isRenewMoney = await RenewUserMoney();
        if (!isRenewMoney)
        {
            TempData["error"] = "Có lỗi xảy ra!";
            return RedirectToAction(nameof(Detail_LienMinh), new { id = productId });
        }

        var query = from lol in _context.Lienminhs
                    join sts in _context.Statuses on lol.StatusId equals sts.StatusId
                    where lol.ProductId == productId
                    && lol.StatusId == 1003
                    select lol;

        var varProduct = await query.FirstOrDefaultAsync();

        // var varProduct = await _context.Lienminhs.Where(i => i.Id == productId && i.Sold == Lienminh.NOT_SOLD).FirstAsync();
        var userBuy = await _context.Users.Where(i => i.UserId == HttpContext.Session.GetInt32(UserController.SessionKeyId)).FirstAsync();

        if (varProduct == null || userBuy == null)
        {
            TempData["error"] = "Có lỗi xảy ra!";
            return RedirectToAction(nameof(Detail_LienMinh), new { id = productId });
        }

        // Tuple<Lienminh, Status> lienMinhProduct;
        // if (varProduct != null)
        // {
        //     lienMinhProduct = (Tuple<Lienminh, Status>)varProduct;
        // }
        // if(varProduct == null)
        // {
        //     return;
        // }

        if (userBuy.Money < varProduct.PriceAtm)
        {
            TempData["error"] = "Bạn không có đủ tiền";
            return RedirectToAction(nameof(Detail_LienMinh), new { id = productId });
        }

        var maxOderId = await _context.Orders.MaxAsync(a => a.OrderId);

        // Tao hoa don/ don hang
        Order oderUser = new Order();
        oderUser.OrderId = maxOderId + 1;
        oderUser.UserId = userBuy.UserId;
        oderUser.ProductId = varProduct.ProductId;
        oderUser.Status = "Paid";
        oderUser.CreateAt = DateTime.Now;
        oderUser.UpdateAt = DateTime.Now;

        // Tru tien user
        userBuy.Money = userBuy.Money - varProduct.PriceAtm;
        // Update stats da ban
        varProduct.StatusId = SOLD;

        _context.Orders.Update(oderUser);
        _context.Users.Update(userBuy);
        _context.Lienminhs.Update(varProduct);

        await _context.SaveChangesAsync();

        HttpContext.Session.SetInt32(UserController.SessionKeyMoney, (int)userBuy.Money);

        TempData["buy-success"] = "Mua thành công";
        return RedirectToAction(nameof(Index));
    }

    public async Task<bool> RenewUserMoney()
    {
        var id = HttpContext.Session.GetInt32(UserController.SessionKeyId);
        if (id != null)
        {
            var money = HttpContext.Session.GetInt32(UserController.SessionKeyMoney);
            var user = await _context.Users.Where(i => i.UserId == id).FirstAsync();
            if (money != user.Money)
            {
                HttpContext.Session.SetInt32(UserController.SessionKeyMoney, Convert.ToInt32(user.Money));
            }
            return true;
        }
        return false;
    }

    public async Task<IActionResult> Detail_LienMinh(int id)
    {
        await RenewUserMoney();

        var queryProductById = from product in _context.Lienminhs
                               join sts in _context.Statuses on product.StatusId equals sts.StatusId
                               where product.ProductId == id && product.StatusId == NOT_SOLD
                               select product;


        // bool isFind = await _context.Lienminhs.AnyAsync(a => a.Id == id);
        bool isFind = await queryProductById.AnyAsync();

        if (isFind == false)
        {
            ViewBag.Error = "Không tìm thấy tài khoản!";
            return View();
        }

        // Take a result by ID
        // var acc = await _context.Lienminhs.Where(a => a.Id == id && a.Sold == Lienminh.NOT_SOLD).FirstAsync();
        var acc = await queryProductById.FirstAsync();
        ViewBag.accLienMinh = acc;

        // var imgAcc = await _context.Images.Where(i => i.LienminhId == acc.Id).OrderBy(i => i.ImgId).ToListAsync();
        var imgAcc = await _context.Images.Where(i => i.ProductId == acc.ProductId).OrderBy(i => i.ImgId).ToListAsync();
        ViewBag.imgAcc = imgAcc;

        // Take a random
        int total = await _context.Lienminhs.Where(a => a.StatusId == NOT_SOLD).CountAsync();
        Random r = new Random();
        int offset = r.Next(0, total);
        var randomAcc = await _context.Lienminhs.OrderBy(a => a.ProductId).Skip(offset).Take(4).ToListAsync();
        ViewBag.randomAcc = randomAcc;

        return View();
    }

    public async Task<IActionResult> LienMinh(int page, string? SearchKey, int? id, int? price, int? status, int? sort_price)
    {
        const int SORT_DESCENDING = 0;
        const int SORT_ASCENDING = 1;

        // ViewData["CurrentFilter"] = string.IsNullOrEmpty(SearchKey) ? "" : SearchKey;
        ViewData["SearchKey"] = string.IsNullOrEmpty(SearchKey) ? "" : SearchKey;
        ViewData["price"] = string.IsNullOrEmpty(price.ToString()) ? string.Empty : price;
        ViewData["sort_price"] = sort_price == null ? SORT_ASCENDING : sort_price;
        ViewData["search_by_id"] = id == null ? "" : id;

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
                    where lienMinh.StatusId == (status == null ? NOT_SOLD : status)
                    && ((
                            lienMinh.Name.Contains(SearchKey == null ? string.Empty : SearchKey)
                            || lienMinh.Name.StartsWith(SearchKey == null ? string.Empty : SearchKey)
                            || lienMinh.Name.EndsWith(SearchKey == null ? string.Empty : SearchKey)
                        ) || (
                            lienMinh.Rank.Contains(SearchKey == null ? string.Empty : SearchKey)
                            || lienMinh.Rank.StartsWith(SearchKey == null ? string.Empty : SearchKey)
                            || lienMinh.Rank.EndsWith(SearchKey == null ? string.Empty : SearchKey)
                        ))
                    && (lienMinh.PriceAtm >= priceSearchMin && lienMinh.PriceAtm < priceSearchMax)
                    select lienMinh;

        if (id != null)
        {
            query = from lienMinh in _context.Lienminhs
                    where lienMinh.ProductId == id
                    select lienMinh;
        }

        // Pagination
        if (page == 0 || page.Equals(null))
        {
            page = 1;
        }

        // Max size of page is 12
        int totalPage = 0;
        int totalRecord = 0;
        int pageSize = 12;

        totalRecord = await query.CountAsync();
        totalPage = (totalRecord / pageSize) + ((totalRecord % pageSize) > 0 ? 1 : 0);

        // var shopacc = await query.OrderBy(h => h.Id).Skip((page - 1) * pageSize)
        //                                             .Take(pageSize).ToListAsync();

        if (sort_price == SORT_ASCENDING || sort_price == null)
        {
            var shopacc = await query.OrderBy(a => a.PriceAtm)
                                        .Skip((page - 1) * pageSize).Take(pageSize)
                                        .ToListAsync();
            ViewBag.Shopacc = shopacc;
        }
        else if (sort_price == SORT_DESCENDING)
        {
            var shopacc = await query.OrderByDescending(a => a.PriceAtm)
                                        .Skip((page - 1) * pageSize).Take(pageSize)
                                        .ToListAsync();
            ViewBag.Shopacc = shopacc;
        }

        ViewBag.totalPage = totalPage;
        ViewBag.currentPage = page;

        return View();
    }

    public IActionResult Index()
    {
        return RedirectToAction(nameof(LienMinh));
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
