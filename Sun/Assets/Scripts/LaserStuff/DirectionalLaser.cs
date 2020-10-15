using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalLaser
{
    public Laser Laser { get; private set; }
    public Direction Direction { get; private set; }

    // TODO: laser representation (particle / line renderer / etc)

    public DirectionalLaser(Laser laser, Direction direction)
    {
        Laser = laser;
        Direction = direction;
    }

    public DirectionalLaser(Direction direction) : this(Laser.NullLaser, direction) { }
}
