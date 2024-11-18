using MitternachtsCup.Models;

namespace MitternachtsCup.ViewModels;

public class GruppenMitSpieleVm
{
    public IEnumerable<GruppeVm> Gruppen { get; set; }
    public IEnumerable<Spiel> AngelegteGruppenSpiele { get; set; }
}