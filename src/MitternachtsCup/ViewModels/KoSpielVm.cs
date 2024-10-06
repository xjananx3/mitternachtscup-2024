using MitternachtsCup.Data.Enum;

namespace MitternachtsCup.ViewModels;

public class KoSpielVm
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Platten Platte { get; set; }
    public TimeSpan SpielDauer { get; set; }
    public DateTime StartZeit { get; set; }
    public int? TeamAId { get; set; }
    public string TeamAName { get; set; }
    public int? TeamBId { get; set; }
    public string TeamBName { get; set; }
    public string? GewinnerName { get; set; }
    
    public int? ErgebnisId { get; set; }
    public string? Ergebnis { get; set; }
}