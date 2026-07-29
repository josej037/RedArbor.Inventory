using Inventory.Web.Models.Categories;
using Inventory.Web.Services.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Web.Controllers;

[Authorize]
public class CategoriesController : Controller
{
    private readonly ICategoryApiService _service;
    public CategoriesController(ICategoryApiService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var response = await _service.GetAll();
        if(response.Success == false)
        {
            return View(Enumerable.Empty<CategoryDto?>());
        }

        return View(response.Data);
    }
}
