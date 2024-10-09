using Microsoft.AspNetCore.Mvc;
using MitternachtsCup.Interfaces;
using MitternachtsCup.ViewModels;

namespace MitternachtsCup.Controllers;

public class TurnierplanController : Controller
{
    private readonly ITurnierplanRepository _turnierplanRepository;

    public TurnierplanController(ITurnierplanRepository turnierplanRepository)
    {
        _turnierplanRepository = turnierplanRepository;
    }
    
    public async Task<IActionResult> Index()
    {
        var kommendeGruppenSpiele = await _turnierplanRepository.GetKommendeGruppenSpiele();
        var kommendeKoSpiele = await _turnierplanRepository.GetKommendeKoSpiele();
        var vergangeneGruppenSpiele = await _turnierplanRepository.GetVergangeneGruppenSpiele();
        var vergangeneKoSpiele = await _turnierplanRepository.GetVergangeneKoSpiele();

        var turnierplanVm = new TurnierplanVm()
        {
            GruppenSpieleOhneErgebnis = kommendeGruppenSpiele,
            GruppenSpieleMitErgebnis = vergangeneGruppenSpiele,
            KoSpieleOhneErgebnis = kommendeKoSpiele,
            KoSpieleMitErgebnis = vergangeneKoSpiele
        };
        
        return View(turnierplanVm);
    }

    public async Task<IActionResult> Tus()
    {
        var gruppenSpiele = await _turnierplanRepository.GetKommendeGruppenSpiele();
        var koSpiele = await _turnierplanRepository.GetKommendeKoSpiele();
        var vergangeneGruppenSpiele = await _turnierplanRepository.GetVergangeneGruppenSpiele();
        var vergangeneKoSpiele = await _turnierplanRepository.GetVergangeneKoSpiele();

        var turnierplanVm = new TurnierplanVm()
        {
            GruppenSpieleOhneErgebnis = gruppenSpiele,
            GruppenSpieleMitErgebnis = vergangeneGruppenSpiele,
            KoSpieleOhneErgebnis = koSpiele,
            KoSpieleMitErgebnis = vergangeneKoSpiele
        };
        
        return View(turnierplanVm);
    }

    public async Task<IActionResult> GruppeA()
    {
        var gruppe = await _turnierplanRepository.GetGruppeByName("Gruppe A");

        return View(gruppe);
    }
    
    public async Task<IActionResult> GruppeB()
    {
        var gruppe = await _turnierplanRepository.GetGruppeByName("Gruppe B");

        return View(gruppe);
    }
    
    public async Task<IActionResult> GruppeC()
    {
        var gruppe = await _turnierplanRepository.GetGruppeByName("Gruppe C");

        return View(gruppe);
    }
    
    public async Task<IActionResult> GruppeD()
    {
        var gruppe = await _turnierplanRepository.GetGruppeByName("Gruppe D");

        return View(gruppe);
    }
    
    public async Task<IActionResult> GruppeE()
    {
        var gruppe = await _turnierplanRepository.GetGruppeByName("Gruppe E");

        return View(gruppe);
    }
    
    public async Task<IActionResult> GruppeF()
    {
        var gruppe = await _turnierplanRepository.GetGruppeByName("Gruppe F");

        return View(gruppe);
    }
    
    public async Task<IActionResult> GruppeG()
    {
        var gruppe = await _turnierplanRepository.GetGruppeByName("Gruppe G");

        return View(gruppe);
    }
    
    public async Task<IActionResult> GruppeH()
    {
        var gruppe = await _turnierplanRepository.GetGruppeByName("Gruppe H");

        return View(gruppe);
    }
}