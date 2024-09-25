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
        var alleKoSpiele = new List<KoSpielVm>();
        
        var achtelfinale = _koRepository.GetDummyAchtelfinals(8);
        var viertelfinale = _koRepository.GetDummyViertelfinals();
        var halbfinals = _koRepository.GetDummyHalbfinals();
        var final = _koRepository.GetDummyFinal();
        var spielUmPlatz3 = _koRepository.GetDummySpielUmPlatz3();
        
        alleKoSpiele.AddRange(achtelfinale);
        alleKoSpiele.AddRange(viertelfinale);
        alleKoSpiele.AddRange(halbfinals);
        
        alleKoSpiele.Add(final);
        alleKoSpiele.Add(spielUmPlatz3);
        
        return View(alleKoSpiele);
    }
}