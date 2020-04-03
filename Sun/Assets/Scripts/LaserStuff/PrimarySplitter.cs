using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimarySplitter : LaserObject
{
    protected override void Awake()
    {
        base.Awake();

        var module = GetModule(ModuleType.ManipulationInput);
        if (module != null)
        {
            var input = (ManipulationInputModule)module;
            input.manipulateFunction += ManipulateLaser;
        }
    }

    private Laser[] ManipulateLaser(Laser combinedInput)
    {
        var lasers = new Laser[]
        {
            Laser.Filter(combinedInput, LaserColor.Red),
            Laser.Filter(combinedInput, LaserColor.Green),
            Laser.Filter(combinedInput, LaserColor.Blue)
        };

        foreach (var laser in lasers)
        {
            if (laser.Color == LaserColor.Black)
            {
                laser.Power = 0;
            }
        }

        return lasers;
    }
}
