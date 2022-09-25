using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project_Sem2_WD07_NickVn.Models;
using Microsoft.EntityFrameworkCore;

namespace Project_Sem2_WD07_NickVn.Controllers;

public class ShopaccController : Controller
{
    private readonly ILogger<ShopaccController> _logger;
    private readonly NickVn_ProjectContext _context;

    public ShopaccController(ILogger<ShopaccController> logger, NickVn_ProjectContext context)
    {
        _logger = logger;
        _context = context;
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
            return RedirectToAction(nameof(Detail_LienMinh), new {id = productId});
        }

        var varProduct = await _context.Lienminhs.Where(i => i.Id == productId && i.Sold == Lienminh.NOT_SOLD).FirstAsync();
        var userBuy = await _context.Users.Where(i => i.Id == HttpContext.Session.GetInt32(UserController.SessionKeyId)).FirstAsync();

        if (varProduct == null && userBuy == null)
        {
            TempData["error"] = "Có lỗi xảy ra!";
            return RedirectToAction(nameof(Detail_LienMinh), new {id = productId});
        }

        Lienminh lienMinhProduct = new Lienminh();
        if (varProduct != null)
        {
            lienMinhProduct = (Lienminh)varProduct;
        }

        if (userBuy.Money < lienMinhProduct.PriceAtm)
        {
            TempData["error"] = "Bạn không có đủ tiền";
            return RedirectToAction(nameof(Detail_LienMinh), new {id = productId});
        }

        var maxOderId = await _context.Oders.MaxAsync(a => a.OderId);

        // Tao hoa don/ don hang
        Oder oderUser = new Oder();
        oderUser.OderId = maxOderId + 1;
        oderUser.UserId = userBuy.Id;
        oderUser.ProductId = lienMinhProduct.Id;
        oderUser.CreateAt = DateTime.Now;
        oderUser.UpdateAt = DateTime.Now;

        // Tru tien user
        userBuy.Money  = userBuy.Money - lienMinhProduct.PriceAtm;
        // Update stats da ban
        lienMinhProduct.Sold = Lienminh.SOLD;

        _context.Oders.Update(oderUser);
        _context.Users.Update(userBuy);
        _context.Lienminhs.Update(lienMinhProduct);

        await _context.SaveChangesAsync();


        TempData["buy-success"] = "Mua thành công";
        return RedirectToAction(nameof(Index));
    }

    public async Task<bool> RenewUserMoney()
    {
        var id = HttpContext.Session.GetInt32(UserController.SessionKeyId);
        if (id != null)
        {
            var money = HttpContext.Session.GetInt32(UserController.SessionKeyMoney);
            var user = await _context.Users.Where(i => i.Id == id).FirstAsync();
            if (money  != user.Money)
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
                        where product.Id == id && product.Sold == Lienminh.NOT_SOLD
                        select product;
                        

        // bool isFind = await _context.Lienminhs.AnyAsync(a => a.Id == id);
        bool isFind = await queryProductById.AnyAsync();

        if (isFind == false){
            ViewBag.Error = "Không tìm thấy tài khoản!";
            return View();
        }

        // Take a result by ID
        // var acc = await _context.Lienminhs.Where(a => a.Id == id && a.Sold == Lienminh.NOT_SOLD).FirstAsync();
        var acc = await queryProductById.FirstAsync();
        ViewBag.accLienMinh = acc;

        // var imgAcc = await _context.Images.Where(i => i.LienminhId == acc.Id).OrderBy(i => i.ImgId).ToListAsync();
        var imgAcc = await _context.Images.Where(i => i.LienminhId == acc.Id).OrderBy(i => i.ImgId).ToListAsync();
        ViewBag.imgAcc = imgAcc;

        // Take a random
        int total = await _context.Lienminhs.Where(a => a.Sold == Lienminh.NOT_SOLD).CountAsync();
        Random r = new Random();
        int offset = r.Next(0, total);
        var randomAcc = await _context.Lienminhs.OrderBy(a => a.Id).Skip(offset).FirstOrDefaultAsync();
        ViewBag.randomAcc = randomAcc;

        return View();
    }

    public async Task<IActionResult> LienMinh()
    {
        var shopacc = await _context.Lienminhs.Where(a => a.Sold == Lienminh.NOT_SOLD).OrderBy(a => a.Id).ToListAsync();

        ViewBag.Shopacc = shopacc;
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
