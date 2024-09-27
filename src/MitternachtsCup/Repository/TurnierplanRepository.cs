using Microsoft.EntityFrameworkCore;
using MitternachtsCup.Data;
using MitternachtsCup.Interfaces;
using MitternachtsCup.ViewModels;

namespace MitternachtsCup.Repository;

public class TurnierplanRepository : ITurnierplanRepository
{
    private readonly ApplicationDbContext _context;

    public TurnierplanRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<GruppenSpielVm>> GetGruppenSpiele()
    {
        var spiele = await _context.Spiele.Include(s => s.TeamA).Include(t => t.TeamB).ToListAsync();
        
        return spiele.Where(s => s.Name.Contains("gruppe", StringComparison.CurrentCultureIgnoreCase))
            .Select(s => new GruppenSpielVm()
            {
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

    public async Task<IEnumerable<KoSpielVm>> GetKoSpiele()
    {
        var koSpiele = await _context.Spiele.Include(s => s.TeamA).Include(s => s.TeamB)
            .ToListAsync();

        return koSpiele.Where(s => s.Name.Contains("finale", StringComparison.CurrentCultureIgnoreCase))
            .Select(s => new KoSpielVm()
            {
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

    public async Task<GruppeVm> GetGruppeByName(string gruppeName)
    {
        var spiele = await _context.Spiele.Include(s => s.TeamA).Include(t => t.TeamB).ToListAsync();

        var gruppenSpiele = spiele.Where(s => s.Name.Contains(gruppeName, StringComparison.CurrentCultureIgnoreCase))
            .Select(s => new GruppenSpielVm()
            {
                Name = s.Name,
                Platte = s.Platte,
                StartZeit = s.StartZeit,
                SpielDauer = s.SpielDauer,
                TeamAId = s.TeamAId,
                TeamAName = s.TeamA.Name,
                TeamBId = s.TeamBId,
                TeamBName = s.TeamB.Name,
            });

        var gruppeVm = new GruppeVm()
        {
            GruppenName = gruppeName,
            GruppenSpiele = gruppenSpiele
        };
        
        return gruppeVm;
    }
}