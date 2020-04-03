using UnityEngine;

public interface IGrid
{
    HitObject GetNextLaserObject(Vector3 position, Face face, Direction direction, LaserColor laserColor);
}
