
namespace Solutions.Day16;

public record MazeTile
{
    private readonly string _asString;
    public override string ToString()
    {
        return _asString;
    }
    
    public static MazeTile ToTile(char c) => new((TileValue) c);
    public TileValue Value;

    public MazeTile(TileValue tile)
    {
        _asString = ((char) tile).ToString();
        Value = tile;
    }
}