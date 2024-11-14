using MitternachtsCup.Models;

namespace MitternachtsCup.Interfaces;

public interface ITeamRepository
{
    Task<IEnumerable<Team>> GetAll();
    Task<Team> GetByIdAsync(int id);
    Task<Team> GetByIdAsyncNoTracking(int id);
    bool Add(Team team);
    bool Update(Team team);
    bool Delete(Team team);
    bool Save();
}