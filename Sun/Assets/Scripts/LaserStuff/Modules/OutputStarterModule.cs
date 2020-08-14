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

        base.UpdateLaser(starterLaser, LaserUtil.GetDirection(transform.forward));
    }

    public override void UpdateLaser(Laser laser, Direction direction)
    {
        throw new System.Exception("Cannot update lasers in Output Starter Module");
    }
}
