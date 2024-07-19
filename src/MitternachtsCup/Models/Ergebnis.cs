using System.ComponentModel.DataAnnotations;

namespace MitternachtsCup.Models;

public class Ergebnis
{
    [Key] 
    public int Id { get; set; }

    public int PunkteTeamA { get; set; }
    public int PunkteTeamB { get; set; }
    
}