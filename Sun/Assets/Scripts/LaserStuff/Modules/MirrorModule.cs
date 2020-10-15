using UnityEngine;

public class MirrorModule : Module
{
    public override void OnLaserHit(DirectionalLaser dirLaser, Vector3 hitPosition)
    {
        // TODO: Possibly return here if not hitting front face (Check is done in LaserUtil.GetMirroredDirection)

        base.OnLaserHit(dirLaser, hitPosition);

        ReflectLaser(dirLaser, hitPosition);
    }

    /// <summary>
    /// Reflects the given directional laser in a reflected direction at the hit position
    /// </summary>
    /// <param name="dirLaser"> Directional laser to be reflected </param>
    /// <param name="hitPosition"> Position where the given directional laser hits </param>
    public void ReflectLaser(DirectionalLaser dirLaser, Vector3 hitPosition)
    {
        var mirroredDirection = LaserUtil.GetMirroredDirection(FaceDirection, dirLaser.Direction);

        if (mirroredDirection != null)
        {
            LaserUtil.SendLaser(this, dirLaser.Laser, mirroredDirection.Value, hitPosition);
        }
    }
}