using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputPrimarySplitterModule : OutputModule
{
    /// <summary>
    /// Splits combined laser into RGB components. Red to the left, Green forward, Blue to the right.
    /// If a component isn't part of the laser, just sends a null laser
    /// </summary>
    /// <param name="combinedLaser"> Laser to be processed </param>
    public override void ProcessLaserInput(Laser combinedLaser)
    {
        var red = new DirectionalLaser(Laser.Filter(combinedLaser, LaserColor.Red), LaserUtil.GetNextDirection(FaceDirection, false));
        var green = new DirectionalLaser(Laser.Filter(combinedLaser, LaserColor.Green), FaceDirection);
        var blue = new DirectionalLaser(Laser.Filter(combinedLaser, LaserColor.Blue), LaserUtil.GetNextDirection(FaceDirection, true));

        UpdateLaser(red);
        UpdateLaser(green);
        UpdateLaser(blue);
    }
}
