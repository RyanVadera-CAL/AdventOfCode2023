namespace AdventOfCode2023;

public class Day05
{
    private readonly HashSet<long> _seeds;
    private readonly Queue<string> _lines;

    public Day05(string[] lines)
    {
        _lines = new Queue<string>(lines);
        _seeds = _lines
            .Dequeue()
            .Split(':')
            [1].Split(' ')
            .Where(x => !string.IsNullOrEmpty(x))
            .Select(long.Parse)
            .ToHashSet();
    }
    
    public long Solve_A()
    {
        var maps = LoadMaps();

        var locations = new HashSet<long>();

        foreach (var seed in _seeds)
        {
            var x = maps.Aggregate(seed, (current, map) => map.MapNumber(current));

            locations.Add(x);
        }

        return locations.Min();
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
    }

    private class MapRange
    {
        private readonly long _start;
        private readonly long _end;
        private readonly long _modifier;
        
        public MapRange(long destStart, long sourceStart, long length)
        {
            _start = sourceStart;
            _end = sourceStart + length;
            _modifier = destStart - sourceStart;
        }
        
        public bool TryMapNumber(long source, out long? x)
        {
            x = null;
            if (source < _start || source > _end)
            {
                x = null;
                return false;
            }
            
            x = source + _modifier;
            return true;
        }
    }
}