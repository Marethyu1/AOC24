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

public record SpaceBlock(long Size) : Block(Size, BlockType.Space)
{
    public string Debug => new('.', (int)Size);
};

public class OneOfBlock : OneOfBase<FileBlock, SpaceBlock>
{
    protected OneOfBlock(OneOf<FileBlock, SpaceBlock> input) : base(input)
    {
        
    }

    public bool IsFile => IsT0;
    public bool IsSpace => IsT1;

    public string Debug()
    {
        return IsFile ? AsT0.Debug : AsT1.Debug;
    }
    
    public static implicit operator OneOfBlock(FileBlock input) => new(input);
    public static implicit operator OneOfBlock(SpaceBlock spaceBlock) => new(spaceBlock);
}


public class Day9Solution(LinkedList<OneOfBlock> blocks) : ISolution
{
    
    public long SolvePart1()
    {
        
        return 1;
    }

    private LinkedListNode<OneOfBlock> NextEmptySpace(LinkedListNode<OneOfBlock> node)
    {
        while (node.Value.IsFile && node.Next != null)
        {
            node = node.Next;
        }

        return node;
    }
    
    private LinkedListNode<OneOfBlock> LastFullSpace(LinkedListNode<OneOfBlock> node)
    {
        while (node!.Value.IsSpace && node.Next != null)
        {
            node = node.Previous!;
        }

        return node;
    }

    public long SolvePart2()
    {
        throw new NotImplementedException();
    }

    public static Day9Solution LoadSolution(string basicInput)
    {
        var numbers = File.ReadAllLines(basicInput)
            .First().Trim();
        
        var input = new LinkedList<OneOfBlock>();
        var blockType = BlockType.File;
        long fileId = 0;
        foreach (var number in numbers)
        {
            var size = long.Parse(number.ToString());
            if (blockType == BlockType.File)
            {
                OneOfBlock block = new FileBlock(fileId, size);
                
                input.AddLast(block);
                fileId++;
            }
            else
            {
                OneOfBlock block = new SpaceBlock(size);
                input.AddLast(block);
            }
            blockType = Toggle(blockType);
        }
        return new Day9Solution(input);
    }

    private static BlockType Toggle(BlockType blockType)
    {
        return blockType == BlockType.File ? BlockType.Space : BlockType.File;
    }
}