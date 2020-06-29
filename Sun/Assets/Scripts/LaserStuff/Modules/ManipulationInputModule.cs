public class ManipulationInputModule : InputModule
{
    public override void OnLaserHit(Laser laser, Direction direction)
    {
        if (UpdateLaser(laser, direction))
        {
            laserObject.UpdateManipulationInput();
        }
    }
}