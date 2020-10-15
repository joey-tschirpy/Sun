using UnityEngine;

public class ManipulationInputModule : InputModule
{
    public override void OnLaserHit(DirectionalLaser dirLaser, Vector3 hitPosition)
    {
        base.OnLaserHit(dirLaser, hitPosition);

        if (UpdateLaser(dirLaser))
        {
            laserObject.UpdateManipulationInput();
        }
    }
}