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
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Spiel>()
            .HasOne(s => s.TeamB)
            .WithMany()
            .HasForeignKey(s => s.TeamBId)
            .OnDelete(DeleteBehavior.NoAction); 

        modelBuilder.Entity<Spiel>()
            .HasOne(s => s.TeamB)
            .WithMany()
            .HasForeignKey(s => s.TeamBId)
            .OnDelete(DeleteBehavior.NoAction); // oder DeleteBehavior.NoAction
    }
    
    
}