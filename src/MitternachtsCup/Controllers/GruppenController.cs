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
    private readonly Random _random = new();
    private readonly ITeamRepository _teamRepository;
    private readonly ISpielRepository _spielRepository;
    private readonly IGruppenRepository _gruppenRepository;

    public GruppenController(ITeamRepository teamRepository, ISpielRepository spielRepository, IGruppenRepository gruppenRepository)
    {
        _teamRepository = teamRepository;
        _spielRepository = spielRepository;
        _gruppenRepository = gruppenRepository;
    }   
    public IActionResult Index()
    {
        return View();
    }


    public async Task<IActionResult> GruppenAnzeige(int anzahlGruppen)
    {

        var gruppen = await _gruppenRepository.GetRandomGruppenMitPaarungen(anzahlGruppen);

        
        HttpContext.Session.SetObjectAsJson("gruppen", gruppen);
        
        return View(gruppen);
    }

    public IActionResult AlleGruppenSpiele()
    {
        var gruppenSpiele = new List<SpielVm>();
        return View(gruppenSpiele);
    }
}