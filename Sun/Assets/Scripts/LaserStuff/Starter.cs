using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : LaserObject
{
    [SerializeField]
    private Laser[] outputLasers;

    private void Start()
    {
        var module = GetModule(ModuleType.Output);
        if (module != null)
        {
            var output = (OutputModule)module;

            for (int i = 0; i < outputLasers.Length; i++)
            {
                output.AssignLaser(outputLasers[i], (Direction)i);
            }

            output.SendLasers(grid, transform.position);
        }
    }
}
