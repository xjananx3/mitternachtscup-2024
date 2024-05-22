using System.ComponentModel.DataAnnotations;

namespace MitternachtsCup.Models;

public class Team
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
}