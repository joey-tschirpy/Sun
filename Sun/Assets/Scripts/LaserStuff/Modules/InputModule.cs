using UnityEngine;

public abstract class InputModule : Module
{
    private Laser[] inputLasers;

    private float[] laserRemoveDelayTimers;

    public Laser CombinedLaser
    { 
        get
        {
            Laser combined = Laser.NullLaser;
            foreach(var laser in inputLasers)
            {
                combined.Combine(laser);
            }
            return combined;
        }
    }

    protected override void Awake()
    {
        base.Awake();

        // TODO: Use dictionary for laser input in unique directions
        inputLasers = new Laser[LaserUtil.DirectionCount];
        laserRemoveDelayTimers = new float[LaserUtil.DirectionCount];

        for (int i = 0; i < LaserUtil.DirectionCount; i++)
        {
            inputLasers[i] = Laser.NullLaser;
            laserRemoveDelayTimers[i] = 0.0f;
        }
    }

    private void Update()
    {
        // Removes laser from input after delay if no laser detected
        for (int i = 0; i < LaserUtil.DirectionCount; i++)
        {
            if (inputLasers[i].IsNullLaser) continue;

            laserRemoveDelayTimers[i] += Time.deltaTime;

            if (laserRemoveDelayTimers[i] > settings.inputLaserStopDelay)
            {
                OnLaserHit(Laser.NullLaser, (Direction)i);
            }
        }
    }

    /// <summary>
    /// Assigns input lasers with the lasers hitting the module
    /// </summary>
    /// <param name="laser"> Laser that hits the module </param>
    /// <param name="direction"> Direction the laser is going</param>
    /// <returns> Whether the input lasers were updated with a new laser </returns>
    protected bool UpdateLaser(Laser laser, Direction direction)
    {
        int index = (int)direction;

        laserRemoveDelayTimers[index] = 0;

        if (inputLasers[index] != laser)
        {
            Debug.Log($"<b>{typeof(InputModule)}:</b> <i>{(laser.IsNullLaser ? "REMOVING" : "ADDING")}</i>" +
                $" <b>{(laser.IsNullLaser ? inputLasers[index] : laser)}</b>" +
                $" in <b>{direction}</b> direction hitting <b>{gameObject.name}</b> face of <b>{transform.parent.name}</b>");

            inputLasers[index] = laser;

            // Input laser updated
            return true;
        }

        // No changes to input lasers
        return false;
    }
}