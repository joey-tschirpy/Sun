using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class OutputStarterModule : OutputModule
{
    [SerializeField]
    private Laser starterLaser;

    protected override void Awake()
    {
        base.Awake();

        base.UpdateLaser(new DirectionalLaser(starterLaser, LaserUtil.GetDirection(transform.forward)));
    }

    /// <summary>
    /// Does nothing.
    /// </summary>
    /// <param name="combinedLaser"></param>
    public override void ProcessLaserInput(Laser combinedLaser) { }

    public override void UpdateLaser(DirectionalLaser dirLaser)
    {
        // TODO: Maybe use this as a toggle between different lasers as opposed to processing input
        throw new System.Exception("Cannot update lasers in Output Starter Module");
    }
}
