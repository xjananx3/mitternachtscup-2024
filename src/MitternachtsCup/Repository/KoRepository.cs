using System.Collections;
using MitternachtsCup.Data.Enum;
using MitternachtsCup.Interfaces;
using MitternachtsCup.ViewModels;

namespace MitternachtsCup.Repository;

public class KoRepository : IKoRepository
{
    public IEnumerable<KoSpielVm> GetAllDummyKoSpiele(int groupCount)
    {
        var alleKoSpiele  = new List<KoSpielVm>();
        
        var achtelfinale = GetDummyAchtelfinals(8);
        var viertelfinale = GetDummyViertelfinals();
        var halbfinals = GetDummyHalbfinals();
        var final = GetDummyFinal();
        var spielUmPlatz3 = GetDummySpielUmBronze();
        
        alleKoSpiele.AddRange(achtelfinale);
        alleKoSpiele.AddRange(viertelfinale);
        alleKoSpiele.AddRange(halbfinals);
        
        alleKoSpiele.Add(final);
        alleKoSpiele.Add(spielUmPlatz3);

        return alleKoSpiele;
    }

    public IEnumerable<KoSpielVm> GetDummyAchtelfinals(int groupCount)
    {
        return GenerateAchtelfinals(groupCount);
    }

    public IEnumerable<KoSpielVm> GetDummyViertelfinals()
    {
        return GenerateQuarterFinals("Viertelfinale",9, DateTime.Now.AddDays(4));
    }

    public IEnumerable<KoSpielVm> GetDummyHalbfinals()
    {
        return GenerateSemiFinals("Halbfinale",13, DateTime.Now.AddDays(9));
    }

    public KoSpielVm GetDummyFinal()
    {
        return new KoSpielVm { 
            Id = 15, 
            Name = "Finale", 
            TeamAName = "Sieger HF1", 
            TeamBName = "Sieger HF2", 
            Platte = (Platten)1,
            StartZeit = new DateTime(2024, 11, 30, 23,50, 0)
        };
    }

    public KoSpielVm GetDummySpielUmBronze()
    {
        return new KoSpielVm
        {
            Id = 16,
            Name = "Spiel um Platz 3",
            TeamAName = "Verlierer HF1",
            TeamBName = "Verlierer HF2",
            Platte = (Platten)1,
            StartZeit = new DateTime(2024, 11, 30, 23,30, 0)
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
        int pIndex = 1;
        for (int i = 0; i < schema.Length; i++)
        {
            if (pIndex > 6)
            {
                pIndex = 1;
            }
            matches.Add(new KoSpielVm
            {
                Id = i + 1,
                Name = $"{i + 1}. Achtelfinale",
                StartZeit = startTimes,
                Platte = (Platten)pIndex,
                TeamAName = schema[i].TeamA,
                TeamBName = schema[i].TeamB
            });
            pIndex++;
        }
        return matches;
    }

    private IEnumerable<KoSpielVm> GenerateQuarterFinals(string roundName, int startId, DateTime startTime)
    {
        var matches = new List<KoSpielVm>();
        int pIndex = 1;
        for (int i = 0; i < 4; i++)
        {
            matches.Add(new KoSpielVm
            {
                Id = startId + i,
                Name = $"{i + 1}. {roundName}",
                StartZeit = new DateTime(2024, 11, 30, 21,30, 0),
                Platte = (Platten)pIndex,
                TeamAName = $"Sieger AF{i * 2 + 1}",
                TeamBName = $"Sieger AF{i * 2 + 2}"
            });
            pIndex++;
        }

        return matches;
    }
    
    private IEnumerable<KoSpielVm> GenerateSemiFinals(string roundName, int startId, DateTime startTime)
    {
        var matches = new List<KoSpielVm>();
        int pIndex = 1;
        for (int i = 0; i < 2; i++)
        {
            matches.Add(new KoSpielVm
            {
                Id = startId + i,
                Name = $"{i + 1}. {roundName}",
                StartZeit = new DateTime(2024, 11, 30, 22,30, 0),
                Platte = (Platten)pIndex,
                TeamAName = $"Sieger VF{i * 2 + 1}",
                TeamBName = $"Sieger VF{i * 2 + 2}"
            });
            pIndex++;
        }

        return matches;
    }

    
}