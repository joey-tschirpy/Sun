using UnityEngine;

public class MirrorModule : Module
{
    public override void OnLaserHit(Laser laser, Direction direction, Vector3 hitPosition)
    {
        base.OnLaserHit(laser, direction, hitPosition);

        ReflectLaser(laser, direction, hitPosition);
    }

    public void ReflectLaser(Laser laser, Direction direction, Vector3 hitPosition)
    {
        var mirroredDirection = LaserUtil.GetMirroredDirection(FaceDirection, direction);

        if (mirroredDirection != null)
        {
            LaserUtil.SendLaser(this, laser, (Direction)mirroredDirection, hitPosition);
        }
    }
}