﻿using UnityEngine;

public class ManipulationInputModule : InputModule
{
    public override void OnLaserHit(Laser laser, Direction direction, Vector3 hitPosition)
    {
        base.OnLaserHit(laser, direction, hitPosition);

        if (UpdateLaser(laser, direction))
        {
            laserObject.UpdateManipulationInput();
        }
    }
}