using UnityEngine;

public abstract class InputModule : Module
{
    private Laser[] inputLasers;

    public Laser CombinedLaser => Laser.Combine(inputLasers);

    protected override void Awake()
    {
        base.Awake();

        inputLasers = new Laser[LaserUtil.DirectionCount];

        for (int i = 0; i < LaserUtil.DirectionCount; i++)
        {
            inputLasers[i] = Laser.NullLaser;
        }
    }

    private void Update()
    {
        for (int i = 0; i < LaserUtil.DirectionCount; i++)
        {
            if (!inputLasers[i].IsNullLaser && !directionsHit[i])
            {
                OnLaserHit(new DirectionalLaser((Direction)i), transform.position);
            }
        }
    }

    /// <summary>
    /// Assigns input lasers with the lasers hitting the module
    /// </summary>
    /// <param name="laser"> Laser that hits the module </param>
    /// <param name="direction"> Direction the laser is going</param>
    /// <returns> Whether the input lasers were updated with a new laser </returns>
    protected bool UpdateLaser(DirectionalLaser dirLaser)
    {
        var laser = dirLaser.Laser;
        var direction = dirLaser.Direction;

        // Only accept laser hits on front of face
        if (!IsFrontHitFrom(direction)) return false;

        int index = (int)direction;

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