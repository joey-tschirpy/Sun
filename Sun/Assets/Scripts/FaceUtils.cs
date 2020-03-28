﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceUtils
{
    public enum Face { front, back, left, right }
    public enum Direction { left, straight, right }

    public static Vector3Int GetDirectionFromFace(Face f, Direction dir)
    {
        Vector3Int outputDirection = Vector3Int.up;

        if (dir == Direction.left) outputDirection.x -= 1;
        if (dir == Direction.right) outputDirection.x += 1;

        if (f == Face.back) outputDirection *= -1;
        if (f == Face.left) outputDirection = new Vector3Int(-outputDirection.y, outputDirection.x, 0);
        if (f == Face.right) outputDirection = new Vector3Int(outputDirection.y, -outputDirection.x, 0);

        return outputDirection;
    }

    public static Vector3Int GetDirectionFromFace(Face f)
    {
        Vector3Int outputDirection = Vector3Int.up;

        if (f == Face.back) outputDirection *= -1;
        if (f == Face.left) outputDirection = new Vector3Int(-outputDirection.y, outputDirection.x, 0);
        if (f == Face.right) outputDirection = new Vector3Int(outputDirection.y, -outputDirection.x, 0);

        return outputDirection;
    }
}
