using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using MitternachtsCup.Data;
using MitternachtsCup.Data.Enum;
using MitternachtsCup.Interfaces;
using MitternachtsCup.Models;
using MitternachtsCup.ViewModels;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MitternachtsCup.Controllers;

public class GruppenController : Controller
{
    private readonly ISpielRepository _spielRepository;
    private readonly IGruppenRepository _gruppenRepository;
    private readonly IWebHostEnvironment _hostingEnvironment;
    private readonly string _jsonFileName = "gruppen.json";

    public GruppenController(ISpielRepository spielRepository, IGruppenRepository gruppenRepository, IWebHostEnvironment hostingEnvironment)
    {
        _spielRepository = spielRepository;
        _gruppenRepository = gruppenRepository;
        _hostingEnvironment = hostingEnvironment;
    }  
    
    private string GetJsonFilePath()
    {
        // Den Pfad zum wwwroot-Verzeichnis oder ein benutzerdefiniertes Verzeichnis festlegen
        return Path.Combine(_hostingEnvironment.WebRootPath, _jsonFileName);
    }
    
    public async Task<ActionResult> Index()
    {
        string jsonPath = GetJsonFilePath();
        if (System.IO.File.Exists(jsonPath))
        {
            var json = await System.IO.File.ReadAllTextAsync(jsonPath);
            var gruppenTeams = JsonConvert.DeserializeObject<Dictionary<int, List<Team>>>(json);
            return View(gruppenTeams);
        }
        
        
        Dictionary<int, List<Team>> gruppenTeamsr = await _gruppenRepository.GetRandomGruppenTeams(8);
      
        
        return View(gruppenTeamsr);
    }
    
    public async Task<IActionResult> AlleGruppen()
    {
        string jsonPath = GetJsonFilePath();
        if (System.IO.File.Exists(jsonPath))
        {
            var json = await System.IO.File.ReadAllTextAsync(jsonPath);
            var gruppenTeams = JsonConvert.DeserializeObject<Dictionary<int, List<Team>>>(json);
            var gruppen = _gruppenRepository.GetSavedGruppenMitPaarungen(8, gruppenTeams);
            return View(gruppen);
        }

        return NotFound();
    }

    public async Task<IActionResult> SpieleAnlegen()
    {
        string jsonPath = GetJsonFilePath();
        if (System.IO.File.Exists(jsonPath))
        {
            var json = await System.IO.File.ReadAllTextAsync(jsonPath);
            var gruppenTeams = JsonConvert.DeserializeObject<Dictionary<int, List<Team>>>(json);
            var gruppen = _gruppenRepository.GetSavedGruppenMitPaarungen(8, gruppenTeams);
            var angelegteGruppenSpiele = await _spielRepository.GetGruppenSpiele();

            var gruppenMitSpiele = new GruppenMitSpieleVm()
            {
                Gruppen = gruppen,
                AngelegteGruppenSpiele = angelegteGruppenSpiele
            };
            
            return View(gruppenMitSpiele);
        }

        return NotFound();
    }

    public IActionResult CreateSpiel(string name, int teamAId, int teamBId)
    {
        var createSpielVm = new CreateSpielVm()
        {
            Name = name,
            StartZeit = new DateTime(2024, 11, 30, 17, 0, 0),
            SpielDauer = TimeSpan.FromMinutes(20),
            Platte = Platten.Ausstehend,
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
        return RedirectToAction("SpieleAnlegen", "Gruppen");
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SaveGruppen(Dictionary<int, List<Team>> gruppenTeams)
    {
        var jsonPath = GetJsonFilePath();
        
        if (gruppenTeams == null)
        {
            return BadRequest("Gruppen Daten sind leer.");
        }
        
        var json = JsonConvert.SerializeObject(gruppenTeams);
        await System.IO.File.WriteAllTextAsync(jsonPath, json);
        
        TempData["Message"] = "Gruppen erfolgreich gespeichert!";
        
        return RedirectToAction("Index");
        
    }
}