namespace AdventOfCode2023;

public class Day05
{
    private HashSet<long> _seeds = new();
    private readonly Queue<string> _lines;

    public Day05(string[] lines)
    {
        _lines = new Queue<string>(lines);
    }
    
    public long Solve_A()
    {
        LoadSeeds_A();
        
        var locations = FollowMaps();

        return locations.Min();
    }

    public long Solve_B()
    {
        var pairs = _lines
            .Dequeue()
            .Split(':')
            [1].Split(' ')
            .Where(x => !string.IsNullOrEmpty(x))
            .Select(long.Parse)
            .ToList();

        var seedPairs = new List<(long Start, long End)>();

        for (var i = 0; i < pairs.Count / 2; i++)
        {
            var s = pairs.Skip(i * 2).Take(2);
            seedPairs.Add(new ValueTuple<long, long>(s.First(), s.First() + s.Last()));
        }

        var maps = LoadMaps().Reverse();

        long location = 0;

        while (true)
        {
            var seed = maps.Aggregate(location, (current, map) => map.ReverseMapNumber(current));

            if (location >= 24261546) throw new ApplicationException();

            if (seedPairs.Any(sp => seed >= sp.Item1 && seed <= sp.Item2))
            {
                return location;
            }

            location++;
        }
    }

    private void LoadSeeds_A()
    {
        _seeds = _lines
            .Dequeue()
            .Split(':')
            [1].Split(' ')
            .Where(x => !string.IsNullOrEmpty(x))
            .Select(long.Parse)
            .ToHashSet();
    }
    
    private HashSet<long> FollowMaps()
    {
        var maps = LoadMaps();

        var locations = new HashSet<long>();

        foreach (var seed in _seeds)
        {
            var x = maps.Aggregate(seed, (current, map) => map.MapNumber(current));

            locations.Add(x);
        }

        return locations;
    }

    private ICollection<Map> LoadMaps()
    {
        var maps = new List<Map>();
        
        foreach (var line in _lines)
        {
            if(string.IsNullOrEmpty(line)) continue;

            if (line.EndsWith("map:"))
            {
                maps.Add(new Map());
                continue;
            }

            var mapNumbers = line.Split(' ').Select(long.Parse).ToArray();
            maps.Last().AddRange(new MapRange(mapNumbers[0], mapNumbers[1], mapNumbers[2]));
        }

        return maps;
    }
    
    private class Map
    {
        private List<MapRange> MapRanges { get; } = new ();
        
        public void AddRange(MapRange range)
        {
            MapRanges.Add(range);
        }
        
        public long MapNumber(long source)
        {
            foreach (var mapRange in MapRanges)
            {
                if (mapRange.TryMapNumber(source, out var dest))
                {
                    return dest!.Value;
                }
            }

            return source;
        }
        
        public long ReverseMapNumber(long dest)
        {
            foreach (var mapRange in MapRanges)
            {
                if (mapRange.TryReverseMapNumber(dest, out var source))
                {
                    return source!.Value;
                }
            }
            
            return dest;
        }
    }

    private class MapRange
    {
        private readonly long _sourceStart;
        private readonly long _sourceEnd;
        private readonly long _destStart;
        private readonly long _destEnd;
        private readonly long _modifier;
        
        public MapRange(long destStart, long sourceStart, long length)
        {
            _sourceStart = sourceStart;
            _sourceEnd = sourceStart + length - 1;
            _destStart = destStart;
            _destEnd = destStart + length - 1;
            _modifier = destStart - sourceStart;
        }
        
        public bool TryMapNumber(long source, out long? dest)
        {
            if (source < _sourceStart || source > _sourceEnd)
            {
                dest = null;
                return false;
            }
            
            dest = source + _modifier;
            return true;
        }

        public bool TryReverseMapNumber(long dest, out long? source)
        {
            if (dest < _destStart || dest > _destEnd)
            {
                source = null;
                return false;
            }
            
            source = dest - _modifier;
            return true;
        }
    }
}