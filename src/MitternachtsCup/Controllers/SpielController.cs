using Microsoft.AspNetCore.Mvc;
using MitternachtsCup.Data;
using MitternachtsCup.Data.Enum;
using MitternachtsCup.Interfaces;
using MitternachtsCup.Models;
using MitternachtsCup.ViewModels;

namespace MitternachtsCup.Controllers;

public class SpielController : Controller
{
    private readonly ISpielRepository _spielRepository;
    private readonly ITeamRepository _teamRepository;

    public SpielController(ISpielRepository spielRepository, ITeamRepository teamRepository)
    {
        _spielRepository = spielRepository;
        _teamRepository = teamRepository;
    }
    
    public async Task<IActionResult> Index()
    {
        var spiele = await _spielRepository.GetAll();

        var spieleVm = spiele.Select(s => new SpielVm()
        {
            Id = s.Id,
            Name = s.Name,
            Platte = s.Platte,
            StartZeit = s.StartZeit,
            SpielDauer = s.SpielDauer,
            TeamAId = s.TeamAId,
            TeamBId = s.TeamBId,
            TeamA = s.TeamA,
            TeamB = s.TeamB,
            Ergebnis = $"{s.Ergebnis?.PunkteTeamA} : {s.Ergebnis?.PunkteTeamB}" ?? string.Empty
        });
        return View(spieleVm);
    }
    
    public IActionResult Create(int teamAId, int teamBId)
    {
        var createSpielViewModel = new CreateSpielVm
        {
            Name = "1. Achtelfinale",
            TeamAId = teamAId,
            TeamBId = teamBId,
            StartZeit = new DateTime(2024, 11, 30, 20, 15, 0),
            SpielDauer = TimeSpan.FromMinutes(30)
        };
        return View(createSpielViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateSpielVm spielVm)
    {
        var spiel = new Spiel()
        {
            Name = spielVm.Name,
            Platte = spielVm.Platte,
            StartZeit = spielVm.StartZeit,
            SpielDauer = spielVm.SpielDauer,
            TeamAId = spielVm.TeamAId,
            TeamA = spielVm.TeamA,
            TeamBId = spielVm.TeamBId,
            TeamB = spielVm.TeamB
        };
        _spielRepository.Add(spiel);
        return RedirectToAction("Index");
        
        return View(spielVm);
            
    }
    
    public async Task<IActionResult> Edit(int id)
    {
        var spiel = await _spielRepository.GetByIdAsync(id);
        if (spiel == null) return View("Error");
        var spielVm = new EditSpielVm()
        {
            Name = spiel.Name,
            Platte = spiel.Platte,
            StartZeit = spiel.StartZeit,
            SpielDauer = spiel.SpielDauer,
            TeamAId = spiel.TeamAId,
            TeamA = spiel.TeamA,
            TeamBId = spiel.TeamBId,
            TeamB = spiel.TeamB,
            ErgebnisId = spiel.ErgebnisId,
            Ergebnis = spiel.Ergebnis
        };
        return View(spielVm);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(int id, EditSpielVm spielVm)
    {
        var spiel = new Spiel
        {
            Id = id,
            Name = spielVm.Name,
            Platte = spielVm.Platte,
            StartZeit = spielVm.StartZeit,
            SpielDauer = spielVm.SpielDauer,
            TeamAId = spielVm.TeamAId,
            TeamA = spielVm.TeamA,
            TeamBId = spielVm.TeamBId,
            TeamB = spielVm.TeamB,
            ErgebnisId = spielVm.ErgebnisId,
            Ergebnis = spielVm.Ergebnis
        };
        _spielRepository.Update(spiel);

        return RedirectToAction("Index");
    }
    
    public async Task<IActionResult> EditDetails(int id)
    {
        var spiel = await _spielRepository.GetByIdAsync(id);
        if (spiel == null) return View("Error");
        var spielVm = new EditSpielVm()
        {
            Name = spiel.Name,
            Platte = spiel.Platte,
            StartZeit = spiel.StartZeit,
            SpielDauer = spiel.SpielDauer,
            TeamAId = spiel.TeamAId,
            TeamA = spiel.TeamA,
            TeamBId = spiel.TeamBId,
            TeamB = spiel.TeamB,
            
        };
        return View(spielVm);
    }


    [HttpPost]
    public async Task<IActionResult> EditDetails(int id, EditSpielVm spielVm)
    {
        var spiel = new Spiel
        {
            Id = id,
            Name = spielVm.Name,
            Platte = spielVm.Platte,
            StartZeit = spielVm.StartZeit,
            SpielDauer = spielVm.SpielDauer,
            TeamAId = spielVm.TeamAId,
            TeamA = spielVm.TeamA,
            TeamBId = spielVm.TeamBId,
            TeamB = spielVm.TeamB,
        };
        _spielRepository.Update(spiel);

        return RedirectToAction("Tus", "Turnierplan");
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateErgebnis(int spielId, int punkteTeamA, int punkteTeamB)
    {
        var userSpiel = await _spielRepository.GetByIdAsyncNoTracking(spielId);

        if (userSpiel != null)
        {
            var spielMitErgebnis = new Spiel()
            {
                Id = spielId,
                Name = userSpiel.Name,
                Platte = userSpiel.Platte,
                StartZeit = userSpiel.StartZeit,
                SpielDauer = userSpiel.SpielDauer,
                TeamAId = userSpiel.TeamAId,
                TeamA = userSpiel.TeamA,
                TeamBId = userSpiel.TeamBId,
                Ergebnis = new Ergebnis()
                {
                    PunkteTeamA = punkteTeamA,
                    PunkteTeamB = punkteTeamB
                }
            };
            _spielRepository.Update(spielMitErgebnis);

            // Process points and games for teams if it is a group game
            if (userSpiel.Name.Contains("gruppe", StringComparison.CurrentCultureIgnoreCase))
            {
                await UpdateTeamStats(userSpiel.TeamAId, userSpiel.TeamBId, punkteTeamA, punkteTeamB);
            }

            return RedirectToAction("Tus", "Turnierplan");
        }

        return NotFound();
        
    }

    private async Task UpdateTeamStats(int teamAId, int teamBId, int punkteTeamA, int punkteTeamB)
    {
        var teamA = await _teamRepository.GetByIdAsync(teamAId);
        var teamB = await _teamRepository.GetByIdAsync(teamBId);
        
        if (teamA == null || teamB == null) return;

        // Calculate points and games for team A
        if (punkteTeamA > punkteTeamB)
        {
            teamA.Punkte = (teamA.Punkte ?? 0) + 2;
        }
        teamA.GewonneneSpiele = (teamA.GewonneneSpiele ?? 0) + punkteTeamA;
        teamA.GegenSpiele = (teamA.GegenSpiele ?? 0) + punkteTeamB;

        // Calculate points and games for team B
        if (punkteTeamB > punkteTeamA)
        {
            teamB.Punkte = (teamB.Punkte ?? 0) + 2;
        }
        teamB.GewonneneSpiele = (teamB.GewonneneSpiele ?? 0) + punkteTeamB;
        teamB.GegenSpiele = (teamB.GegenSpiele ?? 0) + punkteTeamA;

        _teamRepository.Update(teamA);
        _teamRepository.Update(teamB);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var spielDetails = await _spielRepository.GetByIdAsync(id);
        if (spielDetails == null) return View("Error");
        return View(spielDetails);
    }
    
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteSpiel(int id)
    {
        var spielDetails = await _spielRepository.GetByIdAsync(id);
        if (spielDetails == null) return View("Error");

        _spielRepository.Delete(spielDetails);
        return RedirectToAction("Index");
    }
}