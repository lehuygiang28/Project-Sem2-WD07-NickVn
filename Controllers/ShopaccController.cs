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

    // public async Task<IActionResult> BuyNowPopupModalLienMinh(int id)
    // {
    //     var acc = await _context.Lienminhs.Where(c => c.Id == id).FirstAsync();
        
    //     if (acc == null)
    //     {
    //         ViewBag.popupModalScriptBuy = null;
    //         return View(nameof(Detail_LienMinh), new {id = id});
    //     }

    //     Lienminh accSelected = (Lienminh)acc;
    //     System.Console.WriteLine($"ID ne: {acc.Id}, name: {acc.Name}");

    //     ViewBag.popupModalScriptBuy = @$"
    //             <form method='POST' asp-action='BuyAcc' asp-controller= 'ShopAcc' accept-charset='UTF-8'
    //                 class='form-horizontal' enctype='multipart/form-data'>
    //                 <input name='_token' type='hidden' value='HJ1Ev1SApzFP8sBSxWreqUtn2Hje5r3jhf8PosZO'>

    //                 <div class='modal-header'>
    //                     <button type='button' class='close' data-dismiss='modal' aria-label='Close'>
    //                         <span aria-hidden='true'>×</span>
    //                     </button>
    //                     <h4 class='modal-title'>Xác nhận mua tài khoản</h4>
    //                 </div>

    //                 <div class='modal-body'>
    //                     <div class='c-content-tab-4 c-opt-3' role='tabpanel'>
    //                         <ul class='nav nav-justified' role='tablist'>
    //                             <li role='presentation' class='active'>
    //                                 <a href='#p' role='tab' data-toggle='tab' class='c-font-16'>Thanh toán</a>
    //                             </li>
    //                             <li role='presentation'>
    //                                 <a href='#' role='tab' data-toggle='tab' class='c-font-16'>Tài khoản</a>
    //                             </li>
    //                         </ul>
    //                         <div class='tab-content'>
    //                             <div role='tabpanel' class='tab-pane fade in active' id='payment'>
    //                                 <ul class='c-tab-items p-t-0 p-b-0 p-l-5 p-r-5'>
    //                                     <li class='c-font-dark'>
    //                                         <table class='table table-striped'>
    //                                             <tbody>
    //                                                 <tr>
    //                                                     <th colspan='2'>Thông tin tài khoản #{acc.Id}</th>
    //                                                 </tr>
    //                                             </tbody>
    //                                             <tbody>
    //                                                 <tr>
    //                                                     <td>Nhà phát hành:</td>
    //                                                     <th>{acc.Publisher}</th>
    //                                                 </tr>
    //                                                 <tr>
    //                                                     <td>Tên game:</td>
    //                                                     <th>{acc.Name}</th>
    //                                                 </tr>
    //                                                 <tr>
    //                                                     <td>Giá tiền:</td>
    //                                                     <th class='text-info'>{acc.PriceAtm}đ</th>
    //                                                 </tr>
    //                                             </tbody>
    //                                         </table>
    //                                     </li>
    //                                 </ul>
    //                             </div>
    //                             <div role='tabpanel' class='tab-pane fade' id='info'>
    //                                 <ul class='c-tab-items p-t-0 p-b-0 p-l-5 p-r-5'>
    //                                     <li class='c-font-dark'>
    //                                         <table class='table table-striped'>
    //                                             <tbody>
    //                                                 <tr>
    //                                                     <th colspan='2'>Chi tiết tài khoản #{acc.Id}</th>
    //                                                 </tr>

    //                                                 <tr>
    //                                                     <td style='width: 50%'>Tướng:</td>
    //                                                     <td class='text-danger' style='font-weight: 700'>{acc.Champ}</td>
    //                                                 </tr>

    //                                                 <tr>
    //                                                     <td style='width: 50%'>Skin:</td>
    //                                                     <td class='text-danger' style='font-weight: 700'>{acc.Skin}</td>
    //                                                 </tr>

    //                                                 <tr>
    //                                                     <td style='width: 50%'>Bảng Ngọc:</td>
    //                                                     <td class='text-danger' style='font-weight: 700'></td>
    //                                                 </tr>

    //                                                 <tr>
    //                                                     <td style='width:50%'>Rank:</td>
    //                                                     <td class='text-danger' style='font-weight: 700'>{acc.Rank}</td>
    //                                                 </tr>

    //                                                 <tr>
    //                                                     <td style='width:50%'>Trạng Thái:</td>
    //                                                     <td class='text-danger' style='font-weight: 700'>{acc.Status}
    //                                                     </td>
    //                                                 </tr>

    //                                             </tbody>
    //                                         </table>
    //                                     </li>
    //                                 </ul>
    //                             </div>
    //                         </div>
    //                     </div>
    //                     <div class='form-group '>
    //                         <label class='col-md-3 form-control-label'>Mã giảm giá:</label>
    //                         <div class='col-md-7'>
    //                             <input type='text' class='form-control c-square c-theme ' name='coupon'
    //                                 placeholder='Mã giảm giá' value=''>
    //                             <span class='help-block'>Nhập mã giảm giá nếu có để nhận ưu đãi</span>
    //                         </div>
    //                     </div>

    //                     <div class='form-group '>
    //                         <label class='col-md-12 form-control-label text-danger'
    //                             style='text-align: center;margin: 10px 0; '>
    //                             Bạn phải đăng nhập mới có thể mua tài khoản tự động.
    //                         </label>


    //                     </div>

    //                     <div style='clear: both'></div>
    //                 </div>
    //                 <div class='modal-footer'>

    //                     <a class='btn c-theme-btn c-btn-square c-btn-uppercase c-btn-bold' href='/login'>Đăng nhập</a>

    //                     <button type='button'
    //                         class='btn c-theme-btn c-btn-border-2x c-btn-square c-btn-bold c-btn-uppercase'
    //                         data-dismiss='modal'>Đóng</button>

    //                 </div>
    //             </form>";
    
    //     return View(nameof(Detail_LienMinh), new { id = id});
    // }

    public async Task RenewUserMoney()
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
        }
    }

    public async Task<IActionResult> Detail_LienMinh(int id)
    {
        await RenewUserMoney();
        bool isFind = await _context.Lienminhs.AnyAsync(a => a.Id == id);

        if (isFind == false){
            ViewBag.Error = "Không tìm thấy tài khoản!";
            return View();
        }

        // Take a result by ID
        var acc = await _context.Lienminhs.Where(a => a.Id == id).FirstAsync();
        ViewBag.accLienMinh = acc;

        var imgAcc = await _context.Images.Where(i => i.LienminhId == acc.Id).OrderBy(i => i.ImgId).ToListAsync();
        ViewBag.imgAcc = imgAcc;

        // Take a random
        int total = await _context.Lienminhs.CountAsync();
        Random r = new Random();
        int offset = r.Next(0, total);
        var randomAcc = await _context.Lienminhs.OrderBy(a => a.Id).Skip(offset).FirstOrDefaultAsync();
        ViewBag.randomAcc = randomAcc;

        return View();
    }

    public async Task<IActionResult> LienMinh()
    {
        var shopacc = await _context.Lienminhs.OrderBy(a => a.Id).ToListAsync();

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
