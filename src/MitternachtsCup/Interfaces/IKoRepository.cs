using MitternachtsCup.ViewModels;

namespace MitternachtsCup.Interfaces;

public interface IKoRepository
{
    IEnumerable<KoSpielVm> GetDummyAchtelfinals(int groupCount);
    IEnumerable<KoSpielVm> GetDummyViertelfinals();
    IEnumerable<KoSpielVm> GetDummyHalbfinals();
    KoSpielVm GetDummyFinal();
    KoSpielVm GetDummySpielUmPlatz3();
    
}