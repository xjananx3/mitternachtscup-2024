using MitternachtsCup.Data.Enum;
using MitternachtsCup.Models;

namespace MitternachtsCup.ViewModels;

public class CreateSpielVm
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Platten Platte { get; set; }
    public DateTime StartZeit { get; set; }
    public TimeSpan SpielDauer { get; set; }
    public int? TeamAId { get; set; }
    public Team? TeamA { get; set; }
    public int? TeamBId { get; set; }
    public Team? TeamB { get; set; }
}