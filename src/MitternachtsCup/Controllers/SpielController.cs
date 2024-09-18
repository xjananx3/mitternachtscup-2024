using Microsoft.AspNetCore.Mvc;
using MitternachtsCup.Data;
using MitternachtsCup.Interfaces;

namespace MitternachtsCup.Controllers;

public class SpielController : Controller
{
    private readonly ISpielRepository _spielRepository;

    public SpielController(ISpielRepository spielRepository)
    {
        _spielRepository = spielRepository;
    }
    public async Task<IActionResult> Index()
    {
        var spiele = await _spielRepository.GetAll();
        return View(spiele);
    }
}