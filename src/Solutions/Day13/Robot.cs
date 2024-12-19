
namespace Solutions.Day13;

public record XYRobot(XyCoord InitialPosition, XyCoord Velocity)
{
    public Guid Id = Guid.NewGuid();
    public XyCoord CurrentPosition { get; private set; } = InitialPosition;

    public XyCoord NextPosition(XyRobotMath math)
    {
        var nextCord = math.Add(CurrentPosition, Velocity);
        CurrentPosition = nextCord;
        return CurrentPosition;
    }
}