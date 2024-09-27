namespace MitternachtsCup.ViewModels;

public class TurnierplanVm
{
    public IEnumerable<GruppenSpielVm> GruppenSpiele { get; set; }
    public IEnumerable<KoSpielVm> KoSpiele { get; set; }
}