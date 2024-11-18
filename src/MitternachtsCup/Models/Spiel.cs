using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MitternachtsCup.Data.Enum;

namespace MitternachtsCup.Models;

public class Spiel
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public Platten Platte { get; set; }
    public DateTime StartZeit { get; set; }
    public TimeSpan SpielDauer { get; set; }
    
    [ForeignKey("TeamA")]
    public int? TeamAId { get; set; }
    public Team? TeamA { get; set; }
    
    [ForeignKey("TeamB")]
    public int? TeamBId { get; set; }
    public Team? TeamB { get; set; }
    
    [ForeignKey("Ergebnis")] 
    public int? ErgebnisId { get; set; }
    public Ergebnis? Ergebnis { get; set; }
}