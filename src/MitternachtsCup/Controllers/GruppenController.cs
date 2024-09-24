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

    public GruppenController(ITeamRepository teamRepository, ISpielRepository spielRepository)
    {
        _teamRepository = teamRepository;
        _spielRepository = spielRepository;
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
        
        gruppenVm.GruppeASpiele = GenerierePaarungen(gruppen[1]);
        gruppenVm.GruppeBSpiele = gruppen.ContainsKey(2) ? GenerierePaarungen(gruppen[2]) : new List<GruppenSpiel>();
        gruppenVm.GruppeCSpiele = gruppen.ContainsKey(3) ? GenerierePaarungen(gruppen[3]) : new List<GruppenSpiel>();
        gruppenVm.GruppeDSpiele = gruppen.ContainsKey(4) ? GenerierePaarungen(gruppen[4]) : new List<GruppenSpiel>();
        gruppenVm.GruppeESpiele = gruppen.ContainsKey(5) ? GenerierePaarungen(gruppen[5]) : new List<GruppenSpiel>();
        gruppenVm.GruppeFSpiele = gruppen.ContainsKey(6) ? GenerierePaarungen(gruppen[6]) : new List<GruppenSpiel>();
        gruppenVm.GruppeGSpiele = gruppen.ContainsKey(7) ? GenerierePaarungen(gruppen[7]) : new List<GruppenSpiel>();
        gruppenVm.GruppeHSpiele = gruppen.ContainsKey(8) ? GenerierePaarungen(gruppen[8]) : new List<GruppenSpiel>(); 
        
        // Save groups and matchups to session
        HttpContext.Session.SetObjectAsJson("gruppen", gruppen);
        
        return View(gruppenVm);
    }

    public IActionResult AlleGruppenSpiele()
    {
        var gruppenSpiele = ErstelleGruppenSpiele();
        return View(gruppenSpiele);
    }

    
    public List<SpielVm> ErstelleGruppenSpiele()
    {
        var gruppen = HttpContext.Session.GetObjectFromJson<Dictionary<int, List<Team>>>("gruppen");

        if (gruppen == null)
        {
            return new List<SpielVm>();
        }
        
        var spiele = new List<SpielVm>();
        int startZeitStunde = 18;
        TimeSpan spielDauer = TimeSpan.FromMinutes(20);
        DateTime startZeit = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, startZeitStunde, 0, 0);

        int plattenIndex = 0;
        int spielIndex = 1;
        
        foreach (var gruppe in gruppen)
        {
            var gruppenSpiele = GenerierePaarungen(gruppe.Value);
            foreach (var gruppenSpiel in gruppenSpiele)
            {
                var spiel = new SpielVm
                {
                    Name = $"Gruppe {Convert.ToChar('A' + (gruppe.Key - 1))} {spielIndex}. Spiel",
                    Platte = plattenIndex < 6 ? (Platten)plattenIndex : Platten.Ausstehend,
                    StartZeit = plattenIndex < 6 ? startZeit.AddMinutes(spielIndex * spielDauer.TotalMinutes) : DateTime.MinValue,
                    SpielDauer = spielDauer,
                    TeamAId = gruppenSpiel.TeamA.Id,
                    TeamA = gruppenSpiel.TeamA,
                    TeamBId = gruppenSpiel.TeamB.Id,
                    TeamB = gruppenSpiel.TeamB
                };

                spiele.Add(spiel);
                plattenIndex++;
                if (plattenIndex > 6)
                {
                    plattenIndex = 0;
                    spielIndex++;
                }
            }
            spielIndex = 1;
        }
        
        return spiele;

    }

    private Dictionary<int, List<Team>> GruppenAusTeamsErrechnen(IEnumerable<Team> teams, int gruppenAnzahl)
    {
        if (gruppenAnzahl != 4 && gruppenAnzahl != 6 && gruppenAnzahl != 8)
            throw new InvalidEnumArgumentException("Eingegebene Zahl muss 4, 6 oder 8 sein");

        List<Team> teamList = teams.ToList();

        if (teamList.Count() < 16 || teamList.Count() > 32)
            throw new ArgumentException("Die Anzahl deiner Teams untersteigt die Zahl 24 und übersteigt die Zahl 32");

        var gemischteTeams = teamList.OrderBy(t => Guid.NewGuid()).ToList();
        
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

    private List<GruppenSpiel> GenerierePaarungen(List<Team> teams)
    {
        var paarungen = new List<GruppenSpiel>();
        int teamCount = teams.Count();
        
        for (int round = 0; round < teamCount - 1; round++)
        {
            for (int i = 0; i < teamCount / 2; i++)
            {
                int teamA = (round + i) % (teamCount - 1);
                int teamB = (teamCount - 1 - i + round) % (teamCount - 1);

                // Last team stays in the same place, teams rotate around it
                if (i == 0)
                {
                    teamB = teamCount - 1;
                }

                paarungen.Add(new GruppenSpiel()
                {
                    TeamA = teams[teamA],
                    TeamB = teams[teamB]
                });
            }
        }
        return paarungen;
    }
}