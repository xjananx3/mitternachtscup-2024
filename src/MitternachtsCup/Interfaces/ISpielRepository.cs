using MitternachtsCup.Models;

namespace MitternachtsCup.Interfaces;

public interface ISpielRepository
{
    Task<IEnumerable<Spiel>> GetAll();
    Task<Spiel> GetByIdAsync(int id);
    bool Add(Spiel spiel);
    bool AddRange(IEnumerable<Spiel> spiele);
    bool Update(Spiel spiel);
    bool Delete(Spiel spiel);
    bool Save();
}