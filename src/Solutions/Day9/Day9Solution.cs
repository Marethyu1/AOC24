using Helpers.Solution;
using OneOf;

namespace Solutions.Day9;

public enum BlockType
{
    Space,
    File
}

public record Block(long Size, BlockType Type)
{
};

public record FileBlock(long Id, long Size) : Block(Size, BlockType.File)
{
    public string Debug => new(Id.ToString()[0], (int)Size);
};

public record SingleFileBlock(long Id, long Value) : Block(1, BlockType.File)
{
    public string Debug => Value.ToString();
};

public record SpaceBlock(long Size) : Block(Size, BlockType.Space)
{
    public string Debug => new('.', (int)Size);
};

public record SingleSpaceBlock() : SpaceBlock(1)
{
    
}

public class OneOfBlock : OneOfBase<SingleFileBlock, SingleSpaceBlock>
{
    protected OneOfBlock(OneOf<SingleFileBlock, SingleSpaceBlock> input) : base(input)
    {
        
    }

    public bool IsFile => IsT0;
    public bool IsSpace => IsT1;

    public string Debug()
    {
        return IsFile ? AsT0.Debug : AsT1.Debug;
    }
    
    public static implicit operator OneOfBlock(SingleFileBlock input) => new(input);
    public static implicit operator OneOfBlock(SingleSpaceBlock spaceBlock) => new(spaceBlock);
}

public class OneOfBlockChunk : OneOfBase<FileBlock, SpaceBlock>
{
    protected OneOfBlockChunk(OneOf<FileBlock, SpaceBlock> input) : base(input)
    {
        
    }

    public bool IsFile => IsT0;
    public bool IsSpace => IsT1;

    public string Debug()
    {
        return IsFile ? AsT0.Debug : AsT1.Debug;
    }
    
    public static implicit operator OneOfBlockChunk(FileBlock input) => new(input);
    public static implicit operator OneOfBlockChunk(SpaceBlock spaceBlock) => new(spaceBlock);
}


public class Day9Solution(LinkedList<OneOfBlock> blocks, LinkedList<OneOfBlockChunk> oneOfBlockChunks) : ISolution
{
    
    public long SolvePart1()
    {

        var blockArray = blocks.ToArray();
        
        foreach (var block in blockArray)
        {
            Console.Write(block.Debug());
        }
        Console.WriteLine();
        
        var rhsIndex = blockArray.Length - 1;
        for (var i = 0; i < blockArray.Length; i++)
        {
            if (rhsIndex < i)
            {
                continue;
            }
            var currentBlock = blockArray[i];
            if (currentBlock.IsFile)
            {
                continue;
            }
            blockArray[i] = blockArray[rhsIndex];
            blockArray[rhsIndex] = new SingleSpaceBlock();
            rhsIndex--;
            while (rhsIndex > i - 1 && blockArray[rhsIndex].IsSpace)
            {
                rhsIndex--;
            }
            
            
        }


        long total = 0;
        for (var i = 0; i < blockArray.Length; i++)
        {
            if (blockArray[i].IsFile)
            {
                total += blockArray[i].AsT0.Id * i;
            };
        }
        return total;
    }

    public long SolvePart2()
    {
        var rhsBlock = oneOfBlockChunks.Last;
        var lhsIndex = 0;
       
       
        var processed = new HashSet<long>();

        while (rhsBlock!.Previous != null)
        {
            if (rhsBlock.Value.IsSpace)
            {
                rhsBlock = rhsBlock.Previous;
                continue;
            }
            
            var file = rhsBlock.Value.AsT0;
            if (!processed.Add(file.Id))
            {
                rhsBlock = rhsBlock.Previous;
                continue;
            }
            

            var lhsBlock = oneOfBlockChunks.First;
            bool pastStart = false;
            while (lhsBlock!.Value.IsFile && lhsBlock.Next != null)
            {
                
                lhsBlock = lhsBlock.Next;
                if (lhsBlock.Value.IsFile && lhsBlock.Value.AsT0.Id == file.Id)
                {
                    pastStart = true;
                }
            }
            

            var space = lhsBlock.Value.AsT1;

            var delta = space.Size - file.Size;
            while (delta < 0)
            {
                if (lhsBlock!.Next == null)
                {
                    break;
                }
                lhsBlock = lhsBlock!.Next;
                if (lhsBlock!.Value.IsSpace)
                {
                    delta = lhsBlock.Value.AsT1.Size - file.Size;
                }
                if (lhsBlock.Value.IsFile && lhsBlock.Value.AsT0.Id == file.Id)
                {
                    pastStart = true;
                }
                
            }
            
            if (pastStart)
            {
                rhsBlock = rhsBlock.Previous;
                continue;
            }
            
            if (delta >= 0)
            {
                lhsBlock.Value = new FileBlock(file.Id, file.Size);
                rhsBlock.Value = new SpaceBlock(file.Size);
                if (delta > 0)
                {
                    OneOfBlockChunk newSpaceBlock = new SpaceBlock(delta);
                    oneOfBlockChunks.AddAfter(lhsBlock, newSpaceBlock);
                }
                
            }
            // else
            // {
            //     
            // }
            
            


            // while (rhsBlock.Previous != null && rhsBlock.Value.IsSpace)
            // {
                // rhsBlock = rhsBlock.Previous;
            // }
            rhsBlock = rhsBlock.Previous;
            
            
            // foreach (var block in oneOfBlockChunks)
            // {
            //     Console.Write(block.Debug());
            // }
            // Console.WriteLine();
        }

        long total = 0;
        var i = 0;
        var items = new List<long>();
        foreach (var block in oneOfBlockChunks)
        {
            var blockString = block.Debug();
            foreach (var c in blockString)
            {
                if (c == '.')
                {
                    items.Add(0);
                }
                else
                {
                    var l = long.Parse(c.ToString());
                    items.Add(l);
                }
               
            }
        }

        Console.WriteLine(total);
        foreach (var item in items)
        {
            Console.Write(item.ToString());
        }
        Console.WriteLine(total);
        for (int j = 0; j < items.Count; j++)
        {
            total += j * items[j];
        }
        
        return total;
    }

    public static Day9Solution LoadSolution(string basicInput)
    {
        var numbers = File.ReadAllLines(basicInput)
            .First().Trim();
        
        var input = new LinkedList<OneOfBlock>();
        var part2Input = new LinkedList<OneOfBlockChunk>();
        var blockType = BlockType.File;
        long fileId = 0;
        foreach (var number in numbers)
        {
            var size = long.Parse(number.ToString());
            if (blockType == BlockType.File)
            {
                OneOfBlockChunk chunk = new FileBlock(fileId, size);
                part2Input.AddLast(chunk);
                for (long i = 0; i < size; i++)
                {
                    OneOfBlock block = new SingleFileBlock(fileId, fileId);
                    input.AddLast(block);
                }
                
                fileId++;
            }
            else
            {
                for (long i = 0; i < size; i++)
                {
                    OneOfBlock block = new SingleSpaceBlock();
                    input.AddLast(block);
                }

                if (size > 0)
                {
                    OneOfBlockChunk chunk = new SpaceBlock(size);
                    part2Input.AddLast(chunk);
                }
                
                
                
            }
            blockType = Toggle(blockType);
        }
        return new Day9Solution(input, part2Input);
    }

    private static BlockType Toggle(BlockType blockType)
    {
        return blockType == BlockType.File ? BlockType.Space : BlockType.File;
    }
}