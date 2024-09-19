using Microsoft.AspNetCore.Mvc;
using MitternachtsCup.Data;
using MitternachtsCup.Interfaces;
using MitternachtsCup.Models;
using MitternachtsCup.ViewModels;

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
    
    public IActionResult Create(int teamAId, int teamBId)
    {
        
        var createSpielViewModel = new CreateSpielVm
        {
            Name = "Gruppe X 1. Spiel",
            TeamAId = teamAId,
            TeamBId = teamBId,
            StartZeit = new DateTime(2023, 11, 25, 17, 0, 0),
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
            TeamB = spiel.TeamB
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
            TeamB = spielVm.TeamB
        };
        _spielRepository.Update(spiel);

        return RedirectToAction("Index");
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