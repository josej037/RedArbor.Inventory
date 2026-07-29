using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Web.Controllers
{
    [Authorize]
    public class InventoryEntriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
