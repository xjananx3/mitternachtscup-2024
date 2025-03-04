using System.ComponentModel;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MitternachtsCup.Data;
using MitternachtsCup.Data.Enum;
using MitternachtsCup.Interfaces;
using MitternachtsCup.Models;
using MitternachtsCup.ViewModels;

namespace MitternachtsCup.Repository;

public class GruppenRepository : IGruppenRepository
{
    private readonly ApplicationDbContext _context;

    public GruppenRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Dictionary<int, List<Team>>> GetRandomGruppenTeams(int anzahlGruppen)
    {
        var teams = await _context.Teams.ToListAsync();
        
        var gruppenTeams = GruppenAusTeamsErrechnen(teams, anzahlGruppen);
        return gruppenTeams;
    }

    public IEnumerable<GruppenSpielVm> GetSavedPaarungen(Dictionary<int, List<Team>> gruppenTeams)
    {
        var alleGruppenSpieleVm = new List<GruppenSpielVm>();

        foreach (var kvp in gruppenTeams)
        {
            int groupIndex = kvp.Key;
            List<Team> teams = kvp.Value;
            
            var gruppenSpielePerGroup = ErrechneGruppenSpiele(teams, groupIndex);

            var sortierteGruppenSpiele = SortiereGruppenSpiele(gruppenSpielePerGroup);
            
            alleGruppenSpieleVm.AddRange(sortierteGruppenSpiele);
        }

        return alleGruppenSpieleVm;
    }
    
    public IEnumerable<GruppeVm> GetSavedGruppenMitPaarungen(int anzahlGruppen, Dictionary<int, List<Team>> gruppenTeams)
    {
        
        var gruppenVM = new List<GruppeVm>();
        
        for (int i = 1; i <= anzahlGruppen; i++)
        {
            var gruppeVm = new GruppeVm()
            {
                GruppenName = $"Gruppe {GetBuchstabe(i)}",
                Teams = gruppenTeams[i],
                GruppenSpiele = ErrechneGruppenSpiele(gruppenTeams[i], i)
            };
            gruppenVM.Add(gruppeVm);
        }

        return gruppenVM;
    }

    private IEnumerable<GruppenSpielVm> ErrechneGruppenSpiele(List<Team> gruppenTeams, int groupIndex)
    {
        var gruppenSpiele = new List<GruppenSpielVm>();
        var gruppenSpielPaarungen = GenerierePaarungen(gruppenTeams);
        var gruppenBuchstabe = GetBuchstabe(groupIndex);

        int index = 0;
        foreach (var gs in gruppenSpielPaarungen)
        {
            var gruppenSpielVm = new GruppenSpielVm()
            {
                GruppenSpielNr = index + 1,
                Name = $"Gruppe {gruppenBuchstabe} {index + 1}.Spiel",
                Platte = (Platten.Ausstehend),
                StartZeit = new DateTime(2024, 11, 30, 17,0, 0),
                SpielDauer = TimeSpan.FromMinutes(20),
                TeamAId = gs.TeamA.Id,
                TeamAName = gs.TeamA.Name,
                TeamBId = gs.TeamB.Id,
                TeamBName = gs.TeamB.Name
            };
            gruppenSpiele.Add(gruppenSpielVm);
            index++;
        }

        return gruppenSpiele;
    }

    public IEnumerable<GruppenSpielVm> SortiereGruppenSpiele(IEnumerable<GruppenSpielVm> gruppenSpiele)
    {
        var gruppenSpieleSortiert = gruppenSpiele
            .OrderBy(spiel =>
            {
                // Extrahiere die Spielnummer (z.B. "1" aus "Gruppe A 1.Spiel")
                var match = Regex.Match(spiel.Name, @"(\d+)\.Spiel");
                return match.Success ? int.Parse(match.Groups[1].Value) : int.MaxValue; // Standardwert falls kein Match
            })
            .ThenBy(spiel =>
            {
                // Extrahiere die Gruppenkennung (z.B. "A" aus "Gruppe A")
                var match = Regex.Match(spiel.Name, @"Gruppe\s([A-H])");
                return match.Success ? match.Groups[1].Value : "Z"; // Standardwert falls kein Match
            });

        return gruppenSpieleSortiert;
    }

    private char GetBuchstabe(int number)
    {
        if(number < 1 || number > 8)
        {
            throw new ArgumentOutOfRangeException(nameof(number), "Index muss zwischen 0 und 7 liegen.");
        }
        
        char gruppenBuchstabe = (char)(65 + (number - 1));
        
        return gruppenBuchstabe;
    }

    private Dictionary<int, List<Team>> GruppenAusTeamsErrechnen(List<Team> teams, int anzahlGruppen)
    {
        if (anzahlGruppen != 4 && anzahlGruppen != 6 && anzahlGruppen != 8)
            throw new InvalidEnumArgumentException("Eingegebene Zahl muss 4, 6 oder 8 sein");

        List<Team> teamList = teams.ToList();

        if (teamList.Count() < 16 || teamList.Count() > 32)
            throw new ArgumentException("Die Anzahl deiner Teams untersteigt die Zahl 24 und übersteigt die Zahl 32");

        var gemischteTeams = teamList.OrderBy(t => Guid.NewGuid()).ToList();
        
        // Dict um gruppen zu speichern
        var gruppen = new Dictionary<int, List<Team>>();
        for (int i = 1; i <= anzahlGruppen; i++)
        {
            gruppen[i] = new List<Team>();
        }

        for (int i = 0; i < gemischteTeams.Count; i++)
        {
            int gruppenNummer = (i % anzahlGruppen) + 1;
            gruppen[gruppenNummer].Add(gemischteTeams[i]);
        }

        return gruppen;
    }
    
    private List<GruppenSpiel> GenerierePaarungen(List<Team> teams)
    {
        var paarungen = new List<GruppenSpiel>();
        int teamCount = teams.Count();

        bool ungeradeTeamAnzahl = teamCount % 2 != 0;
        if (ungeradeTeamAnzahl)
        {
            teams.Add(null); // Das Null-Team repräsentiert das Freilos
            teamCount++;     // Erhöhe die Teamanzahl, um sie gerade zu machen
        }
        
        for (int round = 0; round < teamCount - 1; round++)
        {
            for (int i = 0; i < teamCount / 2; i++)
            {
                int teamA = (round + i) % (teamCount - 1);
                int teamB = (teamCount - 1 - i + round) % (teamCount - 1);

                // Das letzte Team bleibt an der gleichen Position, die anderen rotieren
                if (i == 0)
                {
                    teamB = teamCount - 1;
                }

                // Wenn kein Freilos vorhanden ist, füge die Paarung hinzu
                if (teams[teamA] != null && teams[teamB] != null)
                {
                    paarungen.Add(new GruppenSpiel()
                    {
                        TeamA = teams[teamA],
                        TeamB = teams[teamB]
                    });
                }
            }
        }
        return paarungen;
    }

    
}