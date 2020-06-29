using UnityEngine;

public class MirrorModule : Module
{
    public override void OnLaserHit(Laser laser, Direction direction)
    {
        base.OnLaserHit(laser, direction);

        ReflectLaser(laser, direction);
    }

    public void ReflectLaser(Laser laser, Direction direction)
    {
        var mirroredDirection = LaserUtil.GetMirroredDirection(FaceDirection, direction);

        if (mirroredDirection != null)
        {
            LaserUtil.SendLaser(this, laser, (Direction)mirroredDirection);
        }
    }
}