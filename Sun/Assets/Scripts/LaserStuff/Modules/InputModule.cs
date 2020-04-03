using System.Collections.Generic;

public abstract class InputModule : Module
{
    public List<Laser[]> inputLasers = new List<Laser[]>();

    public override void AddFace(Face face)
    {
        base.AddFace(face);

        inputLasers.Add(Laser.CreateArray(LasersPerFaceCount));
    }

    public void ReceiveLaser(Laser laser, Face face, Direction direction)
    {
        var faceIndex = Faces.IndexOf(face);

        if (faceIndex >= 0)
        {
            inputLasers[faceIndex][(int)direction] = laser;
        }
    }

    public void Reset()
    {
        foreach (var lasers in inputLasers)
        {
            foreach(var laser in lasers)
            {
                laser.Reset();
            }
        }
    }

    public Laser GetCombinedLaser()
    {
        var combinedLaser = Laser.NullLaser;

        foreach (var lasers in inputLasers)
        {
            combinedLaser.Combine(Laser.Combine(new List<Laser>(lasers)));

            foreach(var laser in lasers)
            {
                combinedLaser.Combine(laser);
            }
        }

        return combinedLaser;
    }
}