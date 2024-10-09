using MitternachtsCup.Models;
using MitternachtsCup.ViewModels;

namespace MitternachtsCup.Interfaces;

public interface IGruppenRepository
{
    Task<Dictionary<int, List<Team>>> GetRandomGruppenTeams(int anzahlGruppen);
    IEnumerable<GruppeVm> GetSavedGruppenMitPaarungen(int anzahlGruppen, Dictionary<int, List<Team>> gruppenTeams);
}