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

public record FileBlock(long Id, long Size, long InitialSpace) : Block(Size, BlockType.File)
{
    public Guid Guid = Guid.NewGuid();
    public string Debug => new(Id.ToString()[0], (int)Size);
};

public record SingleFileBlock(long Id, long Value) : Block(1, BlockType.File)
{
    public string Debug => Value.ToString();
};

public record SpaceBlock(long Size, long InitialSpace) : Block(Size, BlockType.Space)
{
    public Guid Guid = Guid.NewGuid();
    public string Debug => new('.', (int)Size);
};

public record SingleSpaceBlock() : SpaceBlock(1, 0)
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
    
    public FileBlock AsFile => AsT0;
    public bool IsSpace => IsT1;
    public SpaceBlock ASpaceBlock => AsT1;

    public string Debug()
    {
        return IsFile ? AsT0.Debug : AsT1.Debug;
    }

    public Guid Guid()
    {
        return IsFile ? AsFile.Guid : ASpaceBlock.Guid;
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
        var reverseChunks = oneOfBlockChunks.ToArray().Reverse().ToArray();
        foreach (FileBlock currentFile in reverseChunks.Where(file => file.IsFile).Select(file => file.AsFile))
        {
            // Console.WriteLine($"Processing {currentFile.Debug}");
            SpaceBlock nextMemoryBlock;
            try
            {
                nextMemoryBlock = NextEmptySpaceThatFits(oneOfBlockChunks.First, currentFile);
            }
            catch (Exception e)
            {
                // Console.WriteLine($"Cant find space for {currentFile.Debug}");
                continue;
            }

            var counter = 0;
            var memoryLocation = 0;
            var fileLocation = 0;
            foreach (var currentChunk in oneOfBlockChunks)
            {
                if (currentChunk.IsSpace && currentChunk.ASpaceBlock.Guid == nextMemoryBlock.Guid)
                {
                    memoryLocation = counter;
                }

                if (currentChunk.IsFile && currentChunk.AsFile.Guid == currentFile.Guid)
                {
                    fileLocation = counter;
                }
                counter++;
            }

            if (memoryLocation > fileLocation)
            {
                // Console.WriteLine($"skipping for space {currentFile.Debug}");
                continue;
            }


            if (nextMemoryBlock.InitialSpace > currentFile.InitialSpace)
            {
                // Console.WriteLine($"skipping {currentFile.Debug}");
                continue;
            }
            
            
            var freeMemoryBlock = Find(nextMemoryBlock.Guid);
            var currentFileSpace = Find(currentFile.Guid);
            var memoryRemaining = freeMemoryBlock.Value.ASpaceBlock.Size - currentFile.Size;
            if (memoryRemaining > 0)
            {
                freeMemoryBlock.Value = currentFile;
                var remainingMemory = new SpaceBlock(memoryRemaining, -1);
                oneOfBlockChunks.AddAfter(freeMemoryBlock, remainingMemory);
                currentFileSpace.Value = new SpaceBlock(currentFile.Size, -1);
            }
            else
            {
                freeMemoryBlock.Value = currentFile;
                currentFileSpace.Value = new SpaceBlock(currentFile.Size, -1);
            }
            
            // Print();
        }

        Print();
        long index = 0;
        long sum = 0;
        foreach (var currentChunk in oneOfBlockChunks)
        {
            if (currentChunk.IsSpace)
            {
                index += currentChunk.ASpaceBlock.Size;
                continue;
            }

            for (var i = 0; i < currentChunk.AsFile.Size; i++)
            {
                sum += currentChunk.AsFile.Id * index;
                index++;
            }
        }
        return sum;
    }

    private LinkedListNode<OneOfBlockChunk> Find(Guid guid)
    {
        var current = oneOfBlockChunks.First;
        while (current.Value.Guid() != guid)
        {
            current = current.Next;
        }

        return current;
    }

    private void Print()
    {
        foreach (var oneOfBlockChunk in oneOfBlockChunks)
        {
            Console.Write(oneOfBlockChunk.Debug());
        }
        Console.WriteLine();
    }

    private SpaceBlock NextEmptySpaceThatFits(LinkedListNode<OneOfBlockChunk>? first, FileBlock fileBlock)
    {
        var spaceBlock = NextEmptySpace(first);
        while (!FitsInChunk(fileBlock, spaceBlock.Value!.ASpaceBlock))
        {
            spaceBlock = NextEmptySpace(spaceBlock.Next);
        }
        return spaceBlock.Value.ASpaceBlock;
    }

    private static bool FitsInChunk(FileBlock chunk, SpaceBlock memory)
    {
        return memory.Size >= chunk.Size;
    }

    private static LinkedListNode<OneOfBlockChunk> NextEmptySpace(LinkedListNode<OneOfBlockChunk>? currentChunk)
    {
        while (currentChunk!.Next != null && currentChunk.Value.IsFile)
        {
            currentChunk = currentChunk.Next;
        }
        return currentChunk;
    }

    public static Day9Solution LoadSolution(string basicInput)
    {
        var numbers = File.ReadAllLines(basicInput)
            .First().Trim();
        
        var input = new LinkedList<OneOfBlock>();
        var part2Input = new LinkedList<OneOfBlockChunk>();
        var blockType = BlockType.File;
        long fileId = 0;
        long globalId = -1;
        foreach (var number in numbers)
        {
            globalId++;
            var size = long.Parse(number.ToString());
            if (blockType == BlockType.File)
            {
                OneOfBlockChunk chunk = new FileBlock(fileId, size, globalId);
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
                    OneOfBlockChunk chunk = new SpaceBlock(size, globalId);
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