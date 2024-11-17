namespace MitternachtsCup.ViewModels;

public class EditTeamVm
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? Punkte { get; set; }
    public int? Spiele { get; set; }
    public int? GegenSpiele { get; set; }
}