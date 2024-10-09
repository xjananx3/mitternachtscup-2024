using System.Collections;
using MitternachtsCup.Interfaces;
using MitternachtsCup.ViewModels;

namespace MitternachtsCup.Repository;

public class KoRepository : IKoRepository
{
    public IEnumerable<KoSpielVm> GetDummyAchtelfinals(int groupCount)
    {
        return GenerateAchtelfinals(groupCount);
    }

    public IEnumerable<KoSpielVm> GetDummyViertelfinals()
    {
        return GenerateNextRounds("Viertelfinale",8, DateTime.Now.AddDays(4));
    }

    public IEnumerable<KoSpielVm> GetDummyHalbfinals()
    {
        return GenerateNextRounds("Halbfinale",12, DateTime.Now.AddDays(9));
    }

    public KoSpielVm GetDummyFinal()
    {
        return new KoSpielVm { 
            Id = 15, 
            Name = "Finale", 
            TeamAName = "Sieger HF1", 
            TeamBName = "Sieger HF2", 
            StartZeit = DateTime.Now.AddDays(14)  
        };
    }

    public KoSpielVm GetDummySpielUmPlatz3()
    {
        return new KoSpielVm
        {
            Id = 16,
            Name = "Spiel um Platz 3",
            TeamAName = "Verlierer HF1",
            TeamBName = "Verlierer HF2",
            StartZeit = new DateTime(2024, 11, 30, 23,50, 0)
        };
    }
    
    private IEnumerable<KoSpielVm> GenerateAchtelfinals(int groupCount)
    {
        var matches = new List<KoSpielVm>();
        var startTimes = new DateTime(2024, 11, 30, 20, 30, 0);
        
        // Achtelfinale Schema für 8 Gruppen
        var eightGroupSchema = new (string TeamA, string TeamB)[]
        {
            ("1. Gruppe C", "2. Gruppe D"), ("1. Gruppe A", "2. Gruppe B"),
            ("1. Gruppe B", "2. Gruppe A"), ("1. Gruppe D", "2. Gruppe C"),
            ("1. Gruppe E", "2. Gruppe F"), ("1. Gruppe G", "2. Gruppe H"),
            ("1. Gruppe F", "2. Gruppe E"), ("1. Gruppe H", "2. Gruppe G")
        };

        // Achtelfinale Schema für 6 Gruppen
        var sixGroupSchema = new (string TeamA, string TeamB)[]
        {
            ("1. Gruppe B", "3. Gruppe A/D/E/F"), ("1. Gruppe A", "2. Gruppe C"),
            ("1. Gruppe F", "3. Gruppe A/B/C"), ("2. Gruppe D", "2. Gruppe E"),
            ("1. Gruppe E", "3. Gruppe A/B/C/D"), ("1. Gruppe D", "2. Gruppe F"),
            ("1. Gruppe C", "3. Gruppe D/E/F"), ("2. Gruppe A", "2. Gruppe B")
        };

        // Achtelfinale Generierung
        var schema = groupCount == 8 ? eightGroupSchema : sixGroupSchema;
        for (int i = 0; i < schema.Length; i++)
        {
            matches.Add(new KoSpielVm
            {
                Id = i + 1,
                Name = $"{i + 1}. Achtelfinale",
                StartZeit = startTimes,
                TeamAName = schema[i].TeamA,
                TeamBName = schema[i].TeamB
            });
        }
        return matches;
    }

    private IEnumerable<KoSpielVm> GenerateNextRounds(string roundName, int startId, DateTime startTime)
    {
        var matches = new List<KoSpielVm>();
        for (int i = 0; i < 4; i++)
        {
            matches.Add(new KoSpielVm
            {
                Id = startId + i,
                Name = $"{i + 1}. {roundName}",
                StartZeit = startTime,
                TeamAName = $"Sieger VF{i * 2 + 1}",
                TeamBName = $"Sieger VF{i * 2 + 2}"
            });
        }

        return matches;
    }

    
}