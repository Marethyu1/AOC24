namespace Solutions.Day15;

public record Tile(TileValue Value)
{
    public override string ToString() => ((char) Value).ToString();
    
    public static Tile ToTile(char c) => new((TileValue) c);
    
    public static Tile[] ToWideTile(Tile t)
    {
        return t.Value switch
        {
            TileValue.Box => [new Tile(TileValue.LhBox), new Tile(TileValue.RhBox)],
            TileValue.Space => [new Tile(TileValue.Space), new Tile(TileValue.Space)],
            TileValue.Wall => [new Tile(TileValue.Wall), new Tile(TileValue.Wall)],
            TileValue.Robot => [new Tile(TileValue.Robot), new Tile(TileValue.Space)],
            _ => throw new ArgumentOutOfRangeException()
        };
    }
};