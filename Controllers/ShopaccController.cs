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

    public async Task<IActionResult> Detail_LienMinh(int id)
    {
        if (!await _context.Lienminhs.AnyAsync(a => a.Id == id)){
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
        var randomAcc = await _context.Lienminhs.Skip(offset).FirstOrDefaultAsync();
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
        return View();
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
