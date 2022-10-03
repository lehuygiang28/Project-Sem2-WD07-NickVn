using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project_Sem2_WD07_NickVn.Models;
using Microsoft.EntityFrameworkCore;

namespace Project_Sem2_WD07_NickVn.Controllers;

public class CategoryController : Controller
{
    private readonly ILogger<CategoryController> _logger;
    private readonly NickVn_ProjectContext _context;
    private const int ENABLE_ACTIVE = 1;

    public CategoryController(ILogger<CategoryController> logger, NickVn_ProjectContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return RedirectToAction(nameof(Garena));
        // return View();
    }
    public async Task<IActionResult> Garena(int id)
    {
        var query = from cate in _context.ProductCategories
                    where cate.Status == ENABLE_ACTIVE
                    select cate;

        var cateProduct = await query.ToListAsync();
        int totalProduct = await _context.Lienminhs.CountAsync();

        var updateTotal = await query.Where(a => a.Action == "LienMinh").FirstAsync();

        foreach(var cat in cateProduct)
        {
            if(cat.Action == "LienMinh")
            {
                cat.Total = totalProduct;
                _context.ProductCategories.Update(cat);
                await _context.SaveChangesAsync();
            }
        }

        ViewBag.cateProduct  = cateProduct;

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
