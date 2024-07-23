using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using MitternachtsCup.Interfaces;
using MitternachtsCup.Models;
using MitternachtsCup.ViewModels;

namespace MitternachtsCup.Controllers;

public class GruppenController : Controller
{
    private readonly Random _random = new();
    private readonly ITeamRepository _teamRepository;

    public GruppenController(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }   
    public IActionResult Index()
    {
        return View();
    }


    public async Task<IActionResult> GruppenAnzeige(int anzahlGruppen)
    {
        var teams = await _teamRepository.GetAll();

        var gruppen = GruppenAusTeamsErrechnen(teams, anzahlGruppen);

        var gruppenVm = new GruppenAnzeigeViewModel();
        
        if (gruppen.ContainsKey(1)) gruppenVm.GruppeATeams = gruppen[1];
        if (gruppen.ContainsKey(2)) gruppenVm.GruppeBTeams = gruppen[2];
        if (gruppen.ContainsKey(3)) gruppenVm.GruppeCTeams = gruppen[3];
        if (gruppen.ContainsKey(4)) gruppenVm.GruppeDTeams = gruppen[4];
        if (gruppen.ContainsKey(5)) gruppenVm.GruppeETeams = gruppen[5];
        if (gruppen.ContainsKey(6)) gruppenVm.GruppeFTeams = gruppen[6];
        if (gruppen.ContainsKey(7)) gruppenVm.GruppeGTeams = gruppen[7];
        if (gruppen.ContainsKey(8)) gruppenVm.GruppeHTeams = gruppen[8];
        
        return View(gruppenVm);
    }


    public Dictionary<int, List<Team>> GruppenAusTeamsErrechnen(IEnumerable<Team> teams, int gruppenAnzahl)
    {
        if (gruppenAnzahl != 4 && gruppenAnzahl != 6 && gruppenAnzahl != 8)
            throw new InvalidEnumArgumentException("Eingegebene Zahl muss 4, 6 oder 8 sein");

        List<Team> teamList = teams.ToList();

        if (teamList.Count() < 16 || teamList.Count() > 32)
            throw new ArgumentException("Die Anzahl deiner Teams untersteigt die Zahl 24 und übersteigt die Zahl 32");

        var gemischteTeams = teamList.OrderBy(t => _random.Next()).ToList();
        
        // Dict um gruppen zu speichern
        var gruppen = new Dictionary<int, List<Team>>();
        for (int i = 1; i <= gruppenAnzahl; i++)
        {
            gruppen[i] = new List<Team>();
        }

        for (int i = 0; i < gemischteTeams.Count; i++)
        {
            int gruppenNummer = (i % gruppenAnzahl) + 1;
            gruppen[gruppenNummer].Add(gemischteTeams[i]);
        }

        return gruppen;
    }
}