using System.ComponentModel.DataAnnotations;

namespace MitternachtsCup.Models;

public class Spiel
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public Platten Platte { get; set; }
    public DateTime StartZeit { get; set; }
    public TimeSpan SpielDauer { get; set; }
    public int TeamAId { get; set; }
    public Team TeamA { get; set; }
    public int TeamBId { get; set; }
    public Team TeamB { get; set; }
}