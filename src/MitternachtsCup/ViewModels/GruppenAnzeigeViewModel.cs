using MitternachtsCup.Models;

namespace MitternachtsCup.ViewModels;

public class GruppenAnzeigeViewModel
{
    public List<Team> GruppeATeams { get; set; } = new();
    public List<Team> GruppeBTeams { get; set; } = new();
    public List<Team> GruppeCTeams { get; set; } = new();
    public List<Team> GruppeDTeams { get; set; } = new();
    public List<Team> GruppeETeams { get; set; } = new();
    public List<Team> GruppeFTeams { get; set; } = new();
    public List<Team> GruppeGTeams { get; set; } = new();
    public List<Team> GruppeHTeams { get; set; } = new();

    public List<GruppenSpiel> GruppeASpiele { get; set; } = new List<GruppenSpiel>();
    public List<GruppenSpiel> GruppeBSpiele { get; set; } = new List<GruppenSpiel>();
    public List<GruppenSpiel> GruppeCSpiele { get; set; } = new List<GruppenSpiel>();
    public List<GruppenSpiel> GruppeDSpiele { get; set; } = new List<GruppenSpiel>();
    public List<GruppenSpiel> GruppeESpiele { get; set; } = new List<GruppenSpiel>();
    public List<GruppenSpiel> GruppeFSpiele { get; set; } = new List<GruppenSpiel>();
    public List<GruppenSpiel> GruppeGSpiele { get; set; } = new List<GruppenSpiel>();
    public List<GruppenSpiel> GruppeHSpiele { get; set; } = new List<GruppenSpiel>();

}