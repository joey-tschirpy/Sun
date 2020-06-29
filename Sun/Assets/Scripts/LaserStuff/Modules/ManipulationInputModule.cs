using UnityEngine;

public class ManipulationInputModule : InputModule
{
    public override void OnLaserHit(Laser laser, Direction direction, Vector3 hitPosition)
    {
        if (UpdateLaser(laser, direction))
        {
            laserObject.UpdateManipulationInput();
        }
    }
}