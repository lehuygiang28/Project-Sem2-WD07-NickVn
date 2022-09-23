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
        return View();
    }
    public async Task<IActionResult> Garena(int id)
    {
        var query = from cate in _context.ProductCategories
                    where cate.Status == ENABLE_ACTIVE
                    select cate;

        var cateProduct = await query.ToListAsync();

        ViewBag.cateProduct  = cateProduct;
        _logger.LogInformation("ID selected: " + id);
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
