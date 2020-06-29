using UnityEngine;

public class PowerInputModule : InputModule
{
    public override void OnLaserHit(Laser laser, Direction direction, Vector3 hitPosition)
    {
        if (UpdateLaser(laser, direction))
        {
            laserObject.UpdatePowerInput();
        }
    }
}
