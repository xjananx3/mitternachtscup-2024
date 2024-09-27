using MitternachtsCup.Models;

namespace MitternachtsCup.ViewModels;

public class GruppeVm
{
    public string GruppenName { get; set; }
    public IEnumerable<Team> Teams { get; set; }
    public IEnumerable<GruppenSpielVm> GruppenSpiele { get; set; }
}