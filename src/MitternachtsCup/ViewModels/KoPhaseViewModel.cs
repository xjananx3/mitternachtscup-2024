namespace MitternachtsCup.ViewModels;

public class KoPhaseViewModel
{
    public IEnumerable<KoSpielVm> Achtelfinals { get; set; }
    public IEnumerable<KoSpielVm> Viertelfinals { get; set; }
    public IEnumerable<KoSpielVm> Halbfinals { get; set; }
    public KoSpielVm SpielUmPlatz3 { get; set; }
    public KoSpielVm Finale { get; set; }
    public IEnumerable<KoSpielVm> VergangeneKoSpiele { get; set; }
}