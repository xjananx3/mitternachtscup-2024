using Microsoft.AspNetCore.Mvc;
using MitternachtsCup.Interfaces;
using MitternachtsCup.ViewModels;

namespace MitternachtsCup.Controllers;

public class TurnierplanController : Controller
{
    private readonly ITurnierplanRepository _turnierplanRepository;
    private readonly IKoRepository _koRepository;

    public TurnierplanController(ITurnierplanRepository turnierplanRepository, IKoRepository koRepository)
    {
        _turnierplanRepository = turnierplanRepository;
        _koRepository = koRepository;
    }
    
    public async Task<IActionResult> Index()
    {
        var kommendeGruppenSpiele = await _turnierplanRepository.GetKommendeGruppenSpiele();
        var kommendeKoSpiele = await _turnierplanRepository.GetKommendeKoSpiele();
        if (!kommendeKoSpiele.Any())
        {
            // Wenn `koSpiele` leer ist, lade die Daten von `_koRepository`
            kommendeKoSpiele = _koRepository.GetAllDummyKoSpiele(8);
        }
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
        if (!koSpiele.Any())
        {
            // Wenn `koSpiele` leer ist, lade die Daten von `_koRepository`
            koSpiele = _koRepository.GetAllDummyKoSpiele(8);
        }
        
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

    public async Task<IActionResult> KoPhase()
    {
        var achtelfinals = await _turnierplanRepository.GetKoSpieleByName("Achtelfinale");
        if (!achtelfinals.Any())
        {
            // Wenn `koSpiele` leer ist, lade die Daten von `_koRepository`
            achtelfinals = _koRepository.GetDummyAchtelfinals(8);
        }
        var viertelfinals = await _turnierplanRepository.GetKoSpieleByName("Viertelfinale");
        if (!viertelfinals.Any())
        {
            // Wenn `koSpiele` leer ist, lade die Daten von `_koRepository`
            viertelfinals = _koRepository.GetDummyViertelfinals();
        }
        
        var halbfinals = await _turnierplanRepository.GetKoSpieleByName("Halbfinale");
        if (!halbfinals.Any())
        {
            // Wenn `koSpiele` leer ist, lade die Daten von `_koRepository`
            halbfinals = _koRepository.GetDummyHalbfinals();
        }
        var finale = await _turnierplanRepository.GetFinalSpiel("Finale");
        
        var spielUmPlatz3 = await _turnierplanRepository.GetFinalSpiel("Spiel um Platz 3");
        
        var vergangeneKoSpiele = await _turnierplanRepository.GetVergangeneKoSpiele();
        
        var koPhase = new KoPhaseViewModel()
        {
            Achtelfinals = achtelfinals,
            Viertelfinals = viertelfinals,
            Halbfinals = halbfinals,
            Finale = finale,
            SpielUmPlatz3 = spielUmPlatz3,
            VergangeneKoSpiele = vergangeneKoSpiele
        };
        
        return View(koPhase);
    }
}