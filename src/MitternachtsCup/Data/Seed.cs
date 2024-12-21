using MitternachtsCup.Data;
using MitternachtsCup.Models;

namespace MitternachtsCup.Data;

public class Seed
{
    public static void SeedData(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            
            context.Database.EnsureCreated();

            if (!context.Teams.Any())
            {
                context.Teams.AddRange(new List<Team>() 
                    {
                    new Team { Name = "Larios", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Name = "Team Havanna 1", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Name = "Team Havanna 2", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Name = "Team Dobey", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Name = "Moorknechte", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Name = "Kampfschnecken", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Name = "Rieberger", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Name = "Souleater", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Name = "Team Shiggy", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Name = "Team Planlos", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Name = "Die Plattenputzer", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Name = "Ping-Pong-Poeten", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Name = "Autoservice Lang", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Name = "Piraten 1", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Name = "Piraten 2", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Name = "Old Schmetterhand", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Name = "Malongsom", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Name = "Topspinners", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Name = "SVS", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Name = "Musikverein Sasbachried", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Name = "Musikverein Sasbachried 2", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Name = "Schluchhalder", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Name = "Blaulichtbuben", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Name = "112-Butterfly", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Name = "TTC Schnelle Kelle", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Name = "Schmetterlinge", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Name = "Die letzten Heuler", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Name = "Die letzten Heuler 2", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Name = "Bohnenklopfer 1", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Name = "Bohnenklopfer 2", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Name = "Sasbacher Trunkenbo lde", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Name = "TC Schmettern & Scheppern", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 }
                    });
                context.SaveChanges();
            }
        }
    }
}