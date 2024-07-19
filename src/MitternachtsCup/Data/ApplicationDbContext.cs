using Microsoft.EntityFrameworkCore;
using MitternachtsCup.Models;

namespace MitternachtsCup.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<Team> Teams { get; set; }
    public DbSet<Spiel> Spiele { get; set; }
    
}