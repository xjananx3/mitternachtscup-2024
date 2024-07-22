using Microsoft.AspNetCore.Mvc;
using MitternachtsCup.Interfaces;
using MitternachtsCup.Models;

namespace MitternachtsCup.Controllers;

public class TeamController : Controller
{
    private readonly ITeamRepository _teamRepository;

    public TeamController(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }
    
    public async Task<IActionResult> Index()
    {
        var teams = await _teamRepository.GetAll();
        return View(teams);
    }
    
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(Team team)
    {
        if (!ModelState.IsValid)
        {
            return View(team);
        }
        
        _teamRepository.Add(team);
        return RedirectToAction("Index");
    }


}