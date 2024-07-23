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
                        new Team()
                        {
                            Name = "Bohnenkloper 1"
                        },
                        new Team()
                        {
                            Name = "Bohnenkloper 2"
                        },
                        new Team()
                        {
                            Name = "Moorknechte Sasbachried"
                        },
                        new Team()
                        {
                            Name = "Maflotho"
                        },
                        new Team()
                        {
                            Name = "Larios 1"
                        },
                        new Team()
                        {
                            Name = "Larios 2"
                        },
                        new Team()
                        {
                            Name = "Musikverein Sasbachried"
                        },
                        new Team()
                        {
                            Name = "Team Dobey"
                        },
                        new Team()
                        {
                            Name = "RSkaliert"
                        },
                        new Team()
                        {
                            Name = "Gruschtle"
                        },
                        new Team()
                        {
                            Name = "Kräuterhexen"
                        },
                        new Team()
                        {
                            Name = "OlympAllstars"
                        },
                        new Team()
                        {
                            Name = "Schluchhalder"
                        },
                        new Team()
                        {
                            Name = "Spritzer"
                        },
                        new Team()
                        {
                            Name = "Durschdlöscher"
                        },
                        new Team()
                        {
                            Name = "Team Havana"
                        },
                        new Team()
                        {
                            Name = "The Old Schmetterhänds"
                        },
                        new Team()
                        {
                            Name = "MaLongSom"
                        },
                        new Team()
                        {
                            Name = "Rieder Piraten 1"
                        },
                        new Team()
                        {
                            Name = "Rieder Piraten 2"
                        },
                        new Team()
                        {
                            Name = "SoulEater"
                        },
                        new Team()
                        {
                            Name = "Bohnenklopferinas"
                        },
                        new Team()
                        {
                            Name = "Schmetterball"
                        },
                        new Team()
                        {
                            Name = "Geschwister Bauer"
                        },
                        new Team()
                        {
                            Name = "Jungspritzer"
                        },
                        new Team()
                        {
                            Name = "Rheingoldstraße"
                        },
			            new Team()
                        {
                            Name = "Omami Flavour"
                        },
			            new Team()
                        {
                            Name = "Schmetterlinge"
                        },
                    });
                context.SaveChanges();
            }
        }
    }
}