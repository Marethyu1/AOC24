namespace Helpers.Grid;

public record Spot(char Value)
{
     
     public bool IsStart => Value == '^';
     public bool IsOccupied => Value == '#';
};