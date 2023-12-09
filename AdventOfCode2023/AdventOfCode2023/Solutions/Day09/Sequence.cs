namespace AdventOfCode2023.Solutions.Day09;

public class Sequence
{
    private readonly IList<int> _sequenceValues;
    
    public Sequence(string sequenceValues)
    {
        _sequenceValues = sequenceValues.Split(' ').Select(int.Parse).ToList();
    }

    private Sequence(IList<int> sequenceValuesValues)
    {
        _sequenceValues = sequenceValuesValues;
    }

    public int PredictNextValue()
    {
        if (_sequenceValues.All(n => n == 0)) return 0;

        var deltaSequence = GenerateDeltaSequence();

        return _sequenceValues.Last() + deltaSequence.PredictNextValue();
    }

    public int PredictPreviousValue()
    {
        if (_sequenceValues.All(n => n == 0)) return 0;
        
        var deltaSequence = GenerateDeltaSequence();

        return _sequenceValues.First() - deltaSequence.PredictPreviousValue();
    }

    private Sequence GenerateDeltaSequence()
    {
        var deltaSequenceValues = new List<int>();

        for (var i = 1; i < _sequenceValues.Count; i++)
        {
            deltaSequenceValues.Add(_sequenceValues[i] - _sequenceValues[i - 1]);
        }

        return new Sequence(deltaSequenceValues);
    }
}