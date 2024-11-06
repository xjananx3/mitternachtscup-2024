namespace MitternachtsCup.ViewModels;

public class TeamVm
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? Punkte { get; set; }

    public int? Spiele { get; set; }
    public int? Gegenspiele { get; set; }
    public int Platzierung { get; set; }
}