using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using MitternachtsCup.Data;
using MitternachtsCup.Data.Enum;
using MitternachtsCup.Interfaces;
using MitternachtsCup.Models;
using MitternachtsCup.ViewModels;

namespace MitternachtsCup.Controllers;

public class GruppenController : Controller
{
    private readonly ISpielRepository _spielRepository;
    private readonly IGruppenRepository _gruppenRepository;

    public GruppenController(ISpielRepository spielRepository, IGruppenRepository gruppenRepository)
    {
        _spielRepository = spielRepository;
        _gruppenRepository = gruppenRepository;
    }   
    public IActionResult Index()
    {
        return View();
    }


    public async Task<IActionResult> AlleGruppen(int anzahlGruppen)
    {
        var gruppen = await _gruppenRepository.GetRandomGruppenMitPaarungen(anzahlGruppen);
        
        return View(gruppen);
    }

    public IActionResult AlleGruppenSpiele()
    {
        var gruppenSpiele = new List<SpielVm>();
        return View(gruppenSpiele);
    }

    public IActionResult CreateSpiel(string name, int teamAId, int teamBId)
    {
        var createSpielVm = new CreateSpielVm()
        {
            Name = name,
            StartZeit = new DateTime(2024, 11, 30, 17, 0, 0),
            SpielDauer = TimeSpan.FromMinutes(20),
            TeamAId = teamAId,
            TeamBId = teamBId,
        };
        return View(createSpielVm);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateSpielVm spielVm)
    {
        if (spielVm.TeamAId == 0)
        {
            return BadRequest();
        }
        var spiel = new Spiel()
        {
            Name = spielVm.Name,
            Platte = spielVm.Platte,
            StartZeit = spielVm.StartZeit,
            SpielDauer = spielVm.SpielDauer,
            TeamAId = spielVm.TeamAId,
            TeamBId = spielVm.TeamBId,
        };
        _spielRepository.Add(spiel);
        return RedirectToAction("AlleGruppen", "Gruppen", 8);
    }
}