using MitternachtsCup.Models;

namespace MitternachtsCup.ViewModels;

public class Turnierplan
{
    public List<Spiel> GruppenSpiele { get; set; }
    public List<Spiel> KoSpiele { get; set; }
}