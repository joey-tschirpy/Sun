using UnityEngine;

public class FilterModule : Module
{
    [SerializeField]
    LaserColor filterColor = LaserColor.White;

    public override void OnLaserHit(Laser laser, Direction direction, Vector3 hitPosition)
    {
        base.OnLaserHit(laser, direction, hitPosition);

        FilterLaser(laser, direction, hitPosition);
    }

    public void FilterLaser(Laser laser, Direction direction, Vector3 hitPosition)
    {
        LaserUtil.SendLaser(this, Laser.Filter(laser, filterColor), direction, hitPosition);
    }
}