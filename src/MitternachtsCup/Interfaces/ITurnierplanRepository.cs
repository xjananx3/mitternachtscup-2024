using MitternachtsCup.ViewModels;

namespace MitternachtsCup.Interfaces;

public interface ITurnierplanRepository
{
    Task<IEnumerable<GruppenSpielVm>> GetGruppenSpiele();
    Task<IEnumerable<KoSpielVm>> GetKoSpiele();
    Task<GruppeVm> GetGruppeByName(string gruppeName);
}