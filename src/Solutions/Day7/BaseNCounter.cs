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

public class BaseNCounter
{
    private readonly int _baseNumber;
    private readonly int _width;
    private readonly int maxValue;
    private new List<BaseNumber> _queue = new();
    private int _currentIndex = 0;
    
    public BaseNCounter(int baseNumber, int width)
    {
        _baseNumber = baseNumber;
        _width = width;
        for (var i = 0; i < _width + 1; i++)
        {
            _queue.Add(new BaseNumber(_baseNumber));
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