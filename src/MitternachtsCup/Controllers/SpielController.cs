using Microsoft.AspNetCore.Mvc;

namespace MitternachtsCup.Controllers;

public class SpielController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}