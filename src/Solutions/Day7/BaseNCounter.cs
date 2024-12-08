namespace Solutions.Day7;


public class BaseNumber(long max)
{
    public long Value = 0;

    public bool Increment()
    {
        var next = Value + 1;
        if (next == max)
        {
            Value = 0;
            return true;
        }
        Value = next;
        return false;
    }

    public bool WillOverflow()
    {
        return Value + 1 == max;
    }
}

public class BaseNCounterFast
{
    public readonly int[] Values;
    private int _currentIndex;
    private readonly int _rightMost;
    private readonly int _max;
    
    public BaseNCounterFast(int baseNumber, int width)
    {
        _max = baseNumber;
        Values = Enumerable.Range(0, width + 1)
            .Select(_ => 0)
            .ToArray();
        
        _rightMost = Values.Length - 1;
        _currentIndex = _rightMost;
    }

    public int[] Next()
    {
        if (WillOverFlow())
        {
            while (WillOverFlow())
            {
                Increment();
                _currentIndex--;
            }
            Increment();
            _currentIndex = _rightMost;
        }
        else
        {
            Increment();
        }
        
        return Values;
    }

    private bool WillOverFlow()
    {
        return Values[_currentIndex] + 1 == _max;
    }

    private void Increment()
    {
        Values[_currentIndex]++;
        if (Values[_currentIndex] == _max)
        {
            Values[_currentIndex] = 0;
        }
    }
}

public class BaseNCounter
{
    private readonly int _width;
    private readonly List<BaseNumber> _queue = [];
    private string AsString;
    private int _currentIndex = 0;
    
    public BaseNCounter(int baseNumber, int width)
    {
        _width = width;
        for (var i = 0; i < _width + 1; i++)
        {
            _queue.Add(new BaseNumber(baseNumber));
        }
    }
    
    public string Value => string.Join("", _queue.Take(_width).Select(x => x.Value).Reverse()); 

    public string NextValue()
    {
        var currentNumber = _queue[_currentIndex];
        if (currentNumber.WillOverflow())
        {
            while (_queue[_currentIndex].WillOverflow())
            {
                _queue[_currentIndex].Increment();
                _currentIndex++;
            }
            _queue[_currentIndex].Increment();
            _currentIndex = 0;
            
        }
        else
        {
            currentNumber.Increment();
        }
        return Value;
    }
}