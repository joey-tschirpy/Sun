using System;
using UnityEngine;

public enum Direction
{
    N,
    NE,
    E,
    SE,
    S,
    SW,
    W,
    NW
}

public static class LaserUtil
{
    // TODO: DirectionalLaser class instead??

    public static readonly int DirectionCount = Enum.GetValues(typeof(Direction)).Length;

    // Will need to be float if direction count is not a power of 2
    private static readonly int RightAngleValueDifference = DirectionCount / 4;

    /// <summary>
    /// Return next value in Direction enum array. Before/After (anticlockwise/clockwise) 'current' Direction given
    /// </summary>
    /// <param name="current"></param>
    /// <param name="clockwise"></param>
    /// <returns></returns>
    public static Direction GetNextDirection(Direction current, bool clockwise)
    {
        int delta = clockwise ? 1 : -1;
        return GetNextDirection(current, delta);
    }

    /// <summary>
    /// Returns next direction 'delta' positions from 'current'
    /// </summary>
    /// <param name="current"></param>
    /// <param name="delta"></param>
    /// <returns></returns>
    public static Direction GetNextDirection(Direction current, int delta)
    {
        int index = ((int)current + delta) % DirectionCount;

        while (index < 0) index += DirectionCount;

        return (Direction)index;
    }

    /// <summary>
    /// Converts Direction enum to direction vector on XZ plane.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public static Vector3 GetDirectionVector(Direction direction)
    {
        int x = 0;
        int z = 0;

        if (direction > Direction.N && direction < Direction.S) x = 1;
        else if (direction > Direction.S) x = -1;

        if (direction > Direction.W || direction < Direction.E) z = 1;
        else if (direction < Direction.W && direction > Direction.E) z = -1;

        // TODO: possibly need to normalize?? Faster without it though.
        return new Vector3(x, 0, z);
    }

    /// <summary>
    /// Converts direction vector on XZ plane to Direction enum.
    /// </summary>
    /// <param name="directionVector"></param>
    /// <returns></returns>
    public static Direction GetDirection(Vector3 directionVector)
    {
        if (directionVector.x == 0 && directionVector.z == 0) throw new Exception("Can't use zero vector on XZ plane");

        // 2^n Directions, requires n-1 variables, for 16 (2^4) Directions needs 1 more variable (x.CompareTo(z))
        var x = Math.Sign(directionVector.x);
        var z = Math.Sign(directionVector.z);

        var mid = DirectionCount / 2;
        var index = mid - x * (mid / 2 + z);

        if (index == mid && z > 0) index = 0; // Special case for first Direction

        return (Direction)index;
    }

    public static Direction? GetMirroredDirection(Direction mirrorFaceDirection, Direction direction)
    {
        // return null if direction not hitting front of mirror face
        if (!IsObtuse(mirrorFaceDirection, direction)) return null;

        var oppMirrorDirection = GetNextDirection(mirrorFaceDirection, DirectionCount / 2);
        int diff = (int)oppMirrorDirection - (int)direction;

        return GetNextDirection(mirrorFaceDirection, diff);
    }

    /// <summary>
    /// Returns a bool value determined by the angle between the given directions.
    /// </summary>
    /// <param name="direction1"></param>
    /// <param name="direction2"></param>
    /// <returns>Whether the angle between the Directions is obtuse or not</returns>
    private static bool IsObtuse(Direction direction1, Direction direction2)
    {
        int diff = Mathf.Abs((int)direction1 - (int)direction2);
        return diff > RightAngleValueDifference && diff < DirectionCount - RightAngleValueDifference;
    }

    /// <summary>
    /// Raycasts from 'start' position in the given 'direction' and sends 'laser' information to
    /// the hit object if it's a module.
    /// </summary>
    /// <param name="laser"></param>
    /// <param name="start"></param>
    /// <param name="direction"></param>
    public static void SendLaser(Module from, Laser laser, Direction direction)
    {
        SendLaser(from, laser, GetDirectionVector(direction));
    }

    /// <summary>
    /// Raycasts from module position in the given 'direction' and sends 'laser' information to
    /// the hit object if it's a module.
    /// </summary>
    /// <param name="module"> The module that sent the laser </param>
    /// <param name="laser"></param>
    /// <param name="start"></param>
    /// <param name="direction"></param>
    public static void SendLaser(Module from, Laser laser, Vector3 direction)
    {
        from.SetColliderEnabled(false);

        var start = from.transform.position;

        RaycastHit hit;
        if (Physics.Raycast(start, direction, out hit))
        {
            Debug.Log($"Sending from {from.name}({start}) to {hit.collider.name}({hit.collider.transform.position}), hit position: {hit.point}");

            Debug.DrawLine(start, hit.point, Laser.VisualColor(laser.Color));

            var module = hit.collider.GetComponent<Module>();
            if (module != null)
            {
                module.OnLaserHit(laser, GetDirection(direction));
            }

            // TODO: on hit effect
        }
        else
        {
            Debug.Log($"Sending from {from.name}({start}) int direction {direction}, hit position: null");
            Debug.DrawLine(start, start + direction.normalized * 50, Laser.VisualColor(laser.Color));
        }

        from.SetColliderEnabled();
    }
}
