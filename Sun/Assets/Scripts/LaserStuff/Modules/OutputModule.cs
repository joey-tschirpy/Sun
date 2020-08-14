using UnityEngine;

public class OutputModule : Module
{
    private Laser[] outputLasers;

    protected override void Awake()
    {
        base.Awake();

        // TODO: Refactor to only require the 3 forward directions (currently including unnecessary backward and sideways directions)
        // possibly could use dictionary??
        outputLasers = new Laser[LaserUtil.DirectionCount];

        for (int i = 0; i < LaserUtil.DirectionCount; i++)
        {
            outputLasers[i] = Laser.NullLaser;
        }
    }

    private void Update()
    {
        if (laserObject.IsPowered)
        {
            for (int i = 0; i < outputLasers.Length; i++)
            {
                if (!outputLasers[i].IsNullLaser)
                {
                    LaserUtil.SendLaser(this, outputLasers[i], (Direction)i);
                }
            }
        }
    }

    public virtual void UpdateLaser(Laser laser, Direction direction)
    {
        Debug.Log($"<b>{typeof(OutputModule)}:</b> <i>UPDATING</i> <b>{direction}</b> direction with <b>{laser}</b> on <b>{gameObject.name}</b> face of <b>{transform.parent.name}</b>");

        outputLasers[(int)direction] = laser;
    }
}