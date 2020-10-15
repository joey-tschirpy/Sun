using UnityEngine;

public class FilterModule : Module
{
    [SerializeField]
    LaserColor filterColor = LaserColor.White;

    public override void OnLaserHit(DirectionalLaser dirLaser, Vector3 hitPosition)
    {
        base.OnLaserHit(dirLaser, hitPosition);

        FilterLaser(dirLaser, hitPosition);
    }

    /// <summary>
    /// Filters the given laser and sends the filtered laser out in the same direction from the hit position
    /// </summary>
    /// <param name="dirLaser"> Directional laser to be filtered </param>
    /// <param name="hitPosition"> Position where the given directional laser hits </param>
    public void FilterLaser(DirectionalLaser dirLaser, Vector3 hitPosition)
    {
        LaserUtil.SendLaser(this, Laser.Filter(dirLaser.Laser, filterColor), dirLaser.Direction, hitPosition);
    }
}