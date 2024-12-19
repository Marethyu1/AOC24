namespace Solutions.Day13;

public class XyRobotMath(XyCoord gridSize)
{
    public XyCoord Add(XyCoord initialPosition, XyCoord velocity)
    {
        var nextR = initialPosition.TilesFromTopWall + velocity.TilesFromTopWall;
        var nextC = initialPosition.TilesFromLeftWall + velocity.TilesFromLeftWall;

        switch (nextR)
        {
            case > 0:
                nextR %= gridSize.TilesFromTopWall;
                break;
            case < 0:
                nextR = gridSize.TilesFromTopWall + nextR;
                break;
        }

        switch (nextC)
        {
            case > 0:
                nextC %= gridSize.TilesFromLeftWall;
                break;
            case < 0:
                nextC = gridSize.TilesFromLeftWall + nextC;
                break;
        }
        
        
        return new XyCoord(nextC, nextR);
    }
}