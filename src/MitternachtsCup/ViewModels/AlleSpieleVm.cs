using MitternachtsCup.Models;

namespace MitternachtsCup.ViewModels;

public class AlleSpieleVm
{
    public IEnumerable<GruppenSpielVm> GruppenSpieleAusJson { get; set; }
    public IEnumerable<Spiel> AngelegteSpiele { get; set; }
}