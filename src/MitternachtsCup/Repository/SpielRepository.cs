using Microsoft.EntityFrameworkCore;
using MitternachtsCup.Data;
using MitternachtsCup.Interfaces;
using MitternachtsCup.Models;

namespace MitternachtsCup.Repository;

public class SpielRepository : ISpielRepository
{
    private readonly ApplicationDbContext _context;

    public SpielRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Spiel>> GetAll()
    {
        return await _context.Spiele
            .Include(s => s.TeamA)
            .Include(s => s.TeamB)
            .Include(s => s.Ergebnis)
            .ToListAsync();
    }
    
    public async Task<Spiel> GetByIdAsync(int id)
    {
        return await _context.Spiele
            .Include(t => t.TeamA)
            .Include(t => t.TeamB)
            .Include(s => s.Ergebnis)
            .FirstOrDefaultAsync(s => s.Id == id);
    }
    
    public async Task<Spiel> GetByIdAsyncNoTracking(int id)
    {
        return await _context.Spiele
            .Include(t => t.TeamA)
            .Include(t => t.TeamB)
            .Include(s => s.Ergebnis)
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public bool Add(Spiel spiel)
    {
        _context.Add(spiel);
        return Save();
    }

    public bool AddRange(IEnumerable<Spiel> spiele)
    {
        _context.AddRange(spiele);
        return Save();
    }


    public bool Update(Spiel spiel)
    {
        _context.Update(spiel);
        return Save();
    }

    public bool Delete(Spiel spiel)
    {
        _context.Remove(spiel);
        return Save();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}