namespace Solutions.Day7;

public class PermutationGenerator<T>
{
    // X, Y
    // 1. x -> y 
    // 2. x , x -> x, y -> y, y -> y, y
    // 3. x, x, x -> x, x, y -> x, y, y -> y, y, y -> y, y, x -> y, x, x -> y, x, y -> x, y, x

    // 3 x, x, x
    //   x, x, y
    //   x, y, x, 
    //   y, x, x, 
    //   x, y, y, 
    //   y, y, x
    //   y, y, y
    //   y, x, y
    
    public static IEnumerable<T[]> GetPermutationsV2(T t1, T t2, int length)
    {
        
        var amountOfPermutations = Math.Pow(2, length);
        for (var i = 0; i < amountOfPermutations; i++)
        {
            var permutation = BinaryRepresentation(i, length);
            var permutationArray = new T[length];
            
            for (var j = 0; j < length; j++)
            {
                var input = permutation[j] == '0' ? t1 : t2;
                permutationArray[j] = input; 
            }
            yield return permutationArray;
        }
    }
    
    public static IEnumerable<T[]> GetPermutationsV3(T[] options, int length)
    {
        var counter = new BaseNCounterFast(options.Length, length);
        var amountOfPermutations = Math.Pow(options.Length, length);
        
        for (var i = 0; i < amountOfPermutations; i++)
        {
            var permutationArray = new T[length];
            var permutation = counter.Values;
            
            for (var j = 0; j < permutationArray.Length; j++)
            {
                var asIndex = permutation[permutationArray.Length - j];
                permutationArray[j] = options[asIndex];; 
            }
            yield return permutationArray;
            counter.Next();
        }
    }

    public static string BinaryRepresentation(int i, int length)
    {
        var stringRepresentation = Convert.ToString(i, 2);
        if (length > stringRepresentation.Length)
        {
            stringRepresentation = new string('0', length - stringRepresentation.Length) + stringRepresentation;
        }
        return stringRepresentation;
    }
}