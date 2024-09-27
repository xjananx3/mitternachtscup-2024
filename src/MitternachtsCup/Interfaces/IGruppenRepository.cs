using MitternachtsCup.Models;
using MitternachtsCup.ViewModels;

namespace MitternachtsCup.Interfaces;

public interface IGruppenRepository
{
    Task<Dictionary<int, List<Team>>> GetRandomGruppenTeams(int anzahlGruppen);
    Task<IEnumerable<GruppeVm>> GetRandomGruppenMitPaarungen(int anzahlGruppen);
}