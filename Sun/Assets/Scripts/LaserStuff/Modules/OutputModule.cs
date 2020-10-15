using UnityEngine;

public class OutputModule : Module
{
    private Laser[] outputLasers;

    protected override void Awake()
    {
        base.Awake();

        // TODO: Refactor to only require the 3 forward directions (currently including unnecessary backward and sideways directions)
        // possibly could use Dictionary or KeyValuePair??
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

    /// <summary>
    /// Processes the combined input lasers and updates output lasers (default: Updates front facing output laser to combined laser)
    /// </summary>
    /// <param name="combinedLaser"> Laser to be processed </param>
    public virtual void ProcessLaserInput(Laser combinedLaser)
    {
        UpdateLaser(new DirectionalLaser(combinedLaser, LaserUtil.GetDirection(transform.forward)));
    }

    /// <summary>
    /// Updates the laser in output with direction of 'dirLaser' as index
    /// </summary>
    /// <param name="dirLaser"> Directional laser to add to output </param>
    public virtual void UpdateLaser(DirectionalLaser dirLaser)
    {
        var laser = dirLaser.Laser;
        var direction = dirLaser.Direction;

        Debug.Log($"<b>{typeof(OutputModule)}:</b> <i>UPDATING</i> <b>{direction}</b> direction with <b>{laser}</b> on <b>{gameObject.name}</b> face of <b>{transform.parent.name}</b>");

        outputLasers[(int)direction] = laser;
    }
}