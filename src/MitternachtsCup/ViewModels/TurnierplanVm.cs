namespace MitternachtsCup.ViewModels;

public class TurnierplanVm
{
    public IEnumerable<GruppenSpielVm> GruppenSpieleOhneErgebnis { get; set; }
    public IEnumerable<GruppenSpielVm> GruppenSpieleMitErgebnis { get; set; }
    public IEnumerable<KoSpielVm> KoSpieleOhneErgebnis { get; set; }
    public IEnumerable<KoSpielVm> KoSpieleMitErgebnis { get; set; }
}