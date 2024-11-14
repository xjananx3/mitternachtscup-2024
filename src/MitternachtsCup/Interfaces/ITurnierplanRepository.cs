using MitternachtsCup.ViewModels;

namespace MitternachtsCup.Interfaces;

public interface ITurnierplanRepository
{
    Task<IEnumerable<GruppenSpielVm>> GetKommendeGruppenSpiele();
    Task<IEnumerable<GruppenSpielVm>> GetVergangeneGruppenSpiele();
    Task<IEnumerable<KoSpielVm>> GetKommendeKoSpiele();
    Task<IEnumerable<KoSpielVm>> GetVergangeneKoSpiele();
    Task<GruppeVm> GetGruppeByName(string gruppeName);
    Task<IEnumerable<KoSpielVm>> GetKoSpieleByName(string name);
    Task<KoSpielVm> GetFinalSpiel(string name);
}