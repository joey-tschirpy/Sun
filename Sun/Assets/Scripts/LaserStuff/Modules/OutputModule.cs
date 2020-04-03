using System.Collections.Generic;
using UnityEngine;

public class OutputModule : Module
{
    public List<Laser[]> outputLasers = new List<Laser[]>();

    public override ModuleType ModuleType => ModuleType.Output;

    public override void AddFace(Face face)
    {
        base.AddFace(face);

        outputLasers.Add(Laser.CreateArray(LasersPerFaceCount));
    }

    public void AssignLaser(Laser laser, Direction direction)
    {
        foreach (var lasers in outputLasers)
        {
            lasers[(int)direction] = laser;
        }
    }

    public void SendLasers(IGrid grid, Vector3 objectPosition)
    {
        for (int i = 0; i < outputLasers.Count; i++)
        {
            for(int j = 0; j < outputLasers[i].Length; j++)
            {
                if (outputLasers[i][j] == Laser.NullLaser) continue;

                var hitObject = grid.GetNextLaserObject(objectPosition, Faces[i], (Direction)j, outputLasers[i][j].Color);

                if (hitObject.LaserObject != null)
                {
                    hitObject.LaserObject.ReceiveLaser(outputLasers[i][j], hitObject.Face, (Direction)j);
                }
            }
        }
    }
}