using Microsoft.AspNetCore.Mvc;
using MitternachtsCup.Interfaces;
using MitternachtsCup.Repository;
using MitternachtsCup.ViewModels;

namespace MitternachtsCup.Controllers;

public class KoController : Controller
{
    private readonly IKoRepository _koRepository;

    public KoController(IKoRepository koRepository)
    {
        _koRepository = koRepository;
    }
    
    public IActionResult Index()
    {
        var alleKoSpiele = _koRepository.GetAllDummyKoSpiele(8);
        
        return View(alleKoSpiele);
    }
}