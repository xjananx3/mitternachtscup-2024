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
                    new Team { Id = 3, Name = "Larios", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Id = 6, Name = "Team Dobey", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Id = 8, Name = "Kampfschnecken", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Id = 16, Name = "Piraten 1", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Id = 4, Name = "Team Havanna 1", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Id = 7, Name = "Moorknechte", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Id = 10, Name = "SoulEater", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Id = 17, Name = "Piraten 2", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Id = 5, Name = "Team Havanna 2", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Id = 11, Name = "Team Schiggy", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Id = 19, Name = "Malongsom", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Id = 18, Name = "Old Schmetterhand", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Id = 9, Name = "Rieberger", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Id = 12, Name = "Team Planlos", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Id = 20, Name = "RS Ingenieure", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Id = 22, Name = "Musikverein Sasbachried", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Id = 13, Name = "Die Plattenputzer", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Id = 14, Name = "Die Kräuterseitlinge", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Id = 21, Name = "SVS", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Id = 23, Name = "Musikverein Sasbachried 2", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Id = 15, Name = "Autoservice Lang", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Id = 24, Name = "Feuerwehr 1", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Id = 27, Name = "TTC Schnelle Kelle", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Id = 31, Name = "Bohnenklopfer 1", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Id = 25, Name = "Feuerwehr 2", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Id = 28, Name = "Schmetterlinge", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Id = 29, Name = "Die letzten Heuler", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Id = 32, Name = "Bohnenklopfer 2", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Id = 26, Name = "Feuerwehr 3", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Id = 30, Name = "Die letzten Heuler 2", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Id = 33, Name = "Sasbacher Trunkenbolde", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 },
                    new Team { Id = 34, Name = "TC Schmettern & Scheppern", Punkte = 0, GewonneneSpiele = 0, GegenSpiele = 0 }
                    });
                context.SaveChanges();
            }
        }
    }
}