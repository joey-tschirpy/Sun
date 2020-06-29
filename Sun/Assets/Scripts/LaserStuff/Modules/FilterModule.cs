using UnityEngine;

public class FilterModule : Module
{
    [SerializeField]
    LaserColor filterColor = LaserColor.White;

    public override void OnLaserHit(Laser laser, Direction direction)
    {
        base.OnLaserHit(laser, direction);

        FilterLaser(laser, direction);
    }

    public void FilterLaser(Laser laser, Direction direction)
    {
        LaserUtil.SendLaser(this, Laser.Filter(laser, filterColor), direction);
    }
}