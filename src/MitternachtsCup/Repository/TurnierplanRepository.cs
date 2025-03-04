using Microsoft.EntityFrameworkCore;
using MitternachtsCup.Data;
using MitternachtsCup.Data.Enum;
using MitternachtsCup.Interfaces;
using MitternachtsCup.Models;
using MitternachtsCup.ViewModels;

namespace MitternachtsCup.Repository;

public class TurnierplanRepository : ITurnierplanRepository
{
    private readonly ApplicationDbContext _context;

    public TurnierplanRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<GruppenSpielVm>> GetKommendeGruppenSpiele()
    {
        var spiele = await _context.Spiele
            .Include(s => s.TeamA)
            .Include(t => t.TeamB)
            .Where(s => s.ErgebnisId == null || (s.Ergebnis.PunkteTeamA == 0 && s.Ergebnis.PunkteTeamB == 0) )
            .ToListAsync();
        
        return spiele.Where(s => s.Name.Contains("gruppe", StringComparison.CurrentCultureIgnoreCase))
            .Select(s => new GruppenSpielVm()
            {
                Id = s.Id,
                Name = s.Name,
                Platte = s.Platte,
                StartZeit = s.StartZeit,
                SpielDauer = s.SpielDauer,
                TeamAId = s.TeamAId,
                TeamAName = s.TeamA.Name,
                TeamBId = s.TeamBId,
                TeamBName = s.TeamB.Name,
            });
    }

    public async Task<IEnumerable<GruppenSpielVm>> GetVergangeneGruppenSpiele()
    {
        var spiele = await _context.Spiele
            .Include(s => s.TeamA)
            .Include(t => t.TeamB)
            .Include(i => i.Ergebnis)
            .Where(s => s.ErgebnisId != null)
            .ToListAsync();
        
        return spiele.Where(s => s.Name.Contains("gruppe", StringComparison.CurrentCultureIgnoreCase))
            .Select(s => new GruppenSpielVm()
            {
                Id = s.Id,
                Name = s.Name,
                Platte = s.Platte,
                StartZeit = s.StartZeit,
                SpielDauer = s.SpielDauer,
                TeamAId = s.TeamAId,
                TeamAName = s.TeamA.Name,
                TeamBId = s.TeamBId,
                TeamBName = s.TeamB.Name,
                ErgebnisId = s.ErgebnisId,
                Ergebnis = s.Ergebnis
            });
    }

    public async Task<IEnumerable<KoSpielVm>> GetKommendeKoSpiele()
    {
        var koSpiele = await _context.Spiele
            .Include(s => s.TeamA)
            .Include(s => s.TeamB)
            .Where(s => s.ErgebnisId == null || (s.Ergebnis.PunkteTeamA == 0 && s.Ergebnis.PunkteTeamB == 0) )
            .ToListAsync();

        return koSpiele.Where(s => s.Name.Contains("finale", StringComparison.CurrentCultureIgnoreCase) || s.Name.Contains("Spiel um Platz 3"))
            .Select(s => new KoSpielVm()
            {
                Id = s.Id,
                Name = s.Name,
                Platte = s.Platte,
                StartZeit = s.StartZeit,
                SpielDauer = s.SpielDauer,
                TeamAId = s.TeamAId,
                TeamAName = s.TeamA.Name,
                TeamBId = s.TeamBId,
                TeamBName = s.TeamB.Name,
            });
    }

    public async Task<IEnumerable<KoSpielVm>> GetVergangeneKoSpiele()
    {
        var koSpiele = await _context.Spiele
            .Include(s => s.TeamA)
            .Include(t => t.TeamB)
            .Include(i => i.Ergebnis)
            .Where(s => s.ErgebnisId != null)
            .ToListAsync();

        return koSpiele.Where(s => s.Name.Contains("finale", StringComparison.CurrentCultureIgnoreCase) || s.Name.Contains("Spiel um Platz 3", StringComparison.CurrentCultureIgnoreCase))
            .Select(s => new KoSpielVm()
            {
                Id = s.Id,
                Name = s.Name,
                Platte = s.Platte,
                StartZeit = s.StartZeit,
                SpielDauer = s.SpielDauer,
                TeamAId = s.TeamAId,
                TeamAName = s.TeamA.Name,
                TeamBId = s.TeamBId,
                TeamBName = s.TeamB.Name,
                ErgebnisId = s.ErgebnisId,
                Ergebnis = s.Ergebnis
            });
    }

    public async Task<GruppeVm> GetGruppeByName(string gruppeName)
    {
        var spiele = await _context.Spiele
            .Include(s => s.TeamA)
            .Include(t => t.TeamB)
            .Include(s => s.Ergebnis)
            .ToListAsync();

        var kommendeGruppenSpiele = spiele
            .Where(s => s.Name.Contains(gruppeName, StringComparison.CurrentCultureIgnoreCase) && s.ErgebnisId == null)
            .Select(s => new GruppenSpielVm()
            {
                Id = s.Id,
                Name = s.Name,
                Platte = s.Platte,
                StartZeit = s.StartZeit,
                SpielDauer = s.SpielDauer,
                TeamAId = s.TeamAId,
                TeamAName = s.TeamA.Name,
                TeamBId = s.TeamBId,
                TeamBName = s.TeamB.Name,
            });

        var vergangeneGruppenSpiele = spiele
            .Where(s => s.Name.Contains(gruppeName, StringComparison.CurrentCultureIgnoreCase) && s.ErgebnisId != null)
            .Select(s => new GruppenSpielVm()
            {
                Id = s.Id,
                Name = s.Name,
                Platte = s.Platte,
                StartZeit = s.StartZeit,
                SpielDauer = s.SpielDauer,
                TeamAId = s.TeamAId,
                TeamAName = s.TeamA.Name,
                TeamBId = s.TeamBId,
                TeamBName = s.TeamB.Name,
                ErgebnisId = s.ErgebnisId,
                Ergebnis = s.Ergebnis
            });

        var teamListe = await GetGruppeTeamsPlatzierung(spiele, gruppeName);

        var gruppeVm = new GruppeVm()
        {
            GruppenName = gruppeName,
            PlatzierungTeams = teamListe,
            GruppenSpiele = kommendeGruppenSpiele,
            VergangeneGruppenSpiele = vergangeneGruppenSpiele
        };
        
        return gruppeVm;
    }

    public async Task<IEnumerable<KoSpielVm>> GetKoSpieleByName(string name)
    {
        var koSpiele = await _context.Spiele
            .Include(s => s.TeamA)
            .Include(t => t.TeamB)
            .Include(i => i.Ergebnis)
            .Where(s => s.ErgebnisId == null || (s.Ergebnis.PunkteTeamA == 0 && s.Ergebnis.PunkteTeamB == 0) )
            .ToListAsync();
        
        return koSpiele.Where(s => s.Name.Contains(name) )
            .Select(s => new KoSpielVm()
            {
                Id = s.Id,
                Name = s.Name,
                Platte = s.Platte,
                StartZeit = s.StartZeit,
                SpielDauer = s.SpielDauer,
                TeamAId = s.TeamAId,
                TeamAName = s.TeamA.Name,
                TeamBId = s.TeamBId,
                TeamBName = s.TeamB.Name,
            });
    }

    public async Task<KoSpielVm> GetFinalSpiel(string name)
    {
        var finalSpiel = await _context.Spiele
            .Include(s => s.TeamA)
            .Include(s => s.TeamB)
            .FirstOrDefaultAsync(s => s.Name == name);
        
        if(finalSpiel == null)
            return new KoSpielVm()
            {
                Name = name,
                Platte = Platten.Tisch1,
                TeamAName = "Sieger/Verlierer HF1",
                TeamBName = "Sieger/Verlierer HF2",
                StartZeit = new DateTime(2024, 11, 30, 23,30, 0)
            };

        return new KoSpielVm()
        {
            Id = finalSpiel.Id,
            Name = finalSpiel.Name,
            Platte = finalSpiel.Platte,
            StartZeit = finalSpiel.StartZeit,
            SpielDauer = finalSpiel.SpielDauer,
            TeamAId = finalSpiel.TeamAId,
            TeamAName = finalSpiel.TeamA.Name,
            TeamBId = finalSpiel.TeamBId,
            TeamBName = finalSpiel.TeamB.Name,
        };
    }

    private async Task<IEnumerable<TeamVm>> GetGruppeTeamsPlatzierung(IEnumerable<Spiel> gruppenSpiele, string gruppeName)
    {
        var spiele = gruppenSpiele.Where(s => s.Name.Contains(gruppeName, StringComparison.CurrentCultureIgnoreCase));

        var teams = GetDistinctTeams(spiele);

        var teamsMitPlatzierung = ErstellePlatzierungen(teams);
        
        return teamsMitPlatzierung;
    }

    private List<Team> GetDistinctTeams(IEnumerable<Spiel> gruppenSpiele)
    {
        var teams = new HashSet<Team>();

        foreach (var spiel in gruppenSpiele)
        {
            if (spiel.TeamA != null)
            {
                teams.Add(spiel.TeamA); // Team A hinzufügen, wenn vorhanden
            }
            if (spiel.TeamB != null)
            {
                teams.Add(spiel.TeamB); // Team B hinzufügen, wenn vorhanden
            }
        }

        return teams.ToList();
    }
    
    private IEnumerable<TeamVm> ErstellePlatzierungen(List<Team> teams)
    {
        var teamVms = teams.Select(team => new TeamVm
        {
            Id = team.Id,
            Name = team.Name,
            Punkte = team.Punkte,
            Spiele = team.GewonneneSpiele ?? 0,
            Gegenspiele = team.GegenSpiele ?? 0
        }).ToList();

        teamVms = teamVms
            .OrderByDescending(vm => vm.Punkte)
            .ThenByDescending(vm => (double)(vm.Spiele ?? 0) / (vm.Gegenspiele ?? 0))
            .ToList();
        
        for (int i = 0; i < teamVms.Count; i++)
        {
            teamVms[i].Platzierung = i + 1;
        }

        return teamVms;
    }
    
}