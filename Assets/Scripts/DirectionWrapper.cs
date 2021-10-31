using UnityEngine;

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public class DirectionWrapper
{
    private const string HorizontalAxisName = "Horizontal";
    private const string VerticalAxisName = "Vertical";

    public Direction Direction { get; }
    public Vector3 Vector3Value { get; }
    public string AxisName { get; }
    public float AxisValue { get; }

    public DirectionWrapper(Direction direction)
    {
        Direction = direction;
        switch (direction)
        {
            case Direction.Up:
                Vector3Value = Vector3.up;
                AxisName = VerticalAxisName;
                AxisValue = 1f;
                break;
            case Direction.Down:
                Vector3Value = Vector3.down;
                AxisName = VerticalAxisName;
                AxisValue = -1f;
                break;
            case Direction.Left:
                Vector3Value = Vector3.left;
                AxisName = HorizontalAxisName;
                AxisValue = -1f;
                break;
            case Direction.Right:
                Vector3Value = Vector3.right;
                AxisName = HorizontalAxisName;
                AxisValue = 1f;
                break;
        }
    }

    public DirectionWrapper(string axisName, float axisValue)
    {
        AxisName = axisName;
        AxisValue = axisValue;
        switch (axisName)
        {
            case VerticalAxisName:
                switch (axisValue)
                {
                    case 1f:
                        Direction = Direction.Up;
                        Vector3Value = Vector3.up;
                        break;
                    case -1f:
                        Direction = Direction.Down;
                        Vector3Value = Vector3.down;
                        break;
                }
                break;
            case HorizontalAxisName:
                switch (axisValue)
                {
                    case 1f:
                        Direction = Direction.Right;
                        Vector3Value = Vector3.right;
                        break;
                    case -1f:
                        Direction = Direction.Left;
                        Vector3Value = Vector3.left;
                        break;
                }
                break;
        }
    }
    
    public DirectionWrapper(string axisName)
    {
        AxisName = axisName;
        AxisValue = Input.GetAxisRaw(AxisName);
        switch (axisName)
        {
            case VerticalAxisName:
                switch (AxisValue)
                {
                    case 1f:
                        Direction = Direction.Up;
                        Vector3Value = Vector3.up;
                        break;
                    case -1f:
                        Direction = Direction.Down;
                        Vector3Value = Vector3.down;
                        break;
                }
                break;
            case HorizontalAxisName:
                switch (AxisValue)
                {
                    case 1f:
                        Direction = Direction.Right;
                        Vector3Value = Vector3.right;
                        break;
                    case -1f:
                        Direction = Direction.Left;
                        Vector3Value = Vector3.left;
                        break;
                }
                break;
        }

    }
}