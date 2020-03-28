using System.Collections.Generic;
using UnityEngine;

public enum LaserColor
{
    Black = 0,
    Red = 1 << 0,
    Green = 1 << 1,
    Blue = 1 << 2,
    Yellow = Red | Green,
    Magenta = Red | Blue,
    Cyan = Green | Blue,
    White = Red | Green | Blue
}

public enum Direction
{
    Left,
    Forward,
    Right
}

[System.Serializable]
public class Laser
{
    [SerializeField]
    private LaserColor color;
    public LaserColor Color { get { return color; } set { color = value; } }

    [SerializeField]
    [Min(0)]
    private int power;
    public int Power { get { return power; } set { power = value; } }

    public Laser()
    {
        color = LaserColor.Black;
        power = 0;
    }

    public Laser(LaserColor color, int power)
    {
        this.color = color;
        this.power = power;
    }

    public Laser(Laser laser)
    {
        color = laser.color;
        power = laser.power;
    }

    public static Laser NullLaser => new Laser();

    public static Laser[] CreateArray(int count)
    {
        var lasers = new Laser[count];

        for(int i = 0; i < lasers.Length; i++)
        {
            lasers[i] = NullLaser;
        }

        return lasers;
    }

    public void Set(LaserColor color, int power)
    {
        this.color = color;
        this.power = power;
    }

    public void Set(Laser laser)
    {
        Set(laser.color, laser.power);
    }

    public Laser Reset()
    {
        Set(LaserColor.Black, 0);
        return this;
    }

    public Laser Combine(Laser other, bool combinePower = true)
    {
        Combine(other.color);
        power = Combine(power, other.power, combinePower);
        return this;
    }

    public static Laser Combine(Laser laser1, Laser laser2, bool combinePower = true)
    {
        LaserColor color = Combine(laser1.color, laser2.color);
        int power = Combine(laser1.power, laser2.power, combinePower);

        return new Laser(color, power);
    }

    public static Laser Combine(Laser[] lasers, bool combinePower = true)
    {
        if (lasers.Length <= 0)
        {
            return NullLaser;
        }
        if (lasers.Length == 1)
        {
            return lasers[0];
        }
        else
        {
            var laser = lasers[0];

            var laserList = new List<Laser>(lasers);
            laserList.Remove(laser);

            return Combine(laser, Combine(laserList, combinePower), combinePower);
        }
    }

    public static Laser Combine(List<Laser> lasers, bool combinePower = true)
    {
        return Combine(lasers.ToArray(), combinePower);
    }

    public LaserColor Combine(LaserColor otherColor)
    {
        return color = Combine(color, otherColor);
    }

    private static LaserColor Combine(LaserColor color1, LaserColor color2)
    {
        return color1 | color2;
    }

    public Laser Filter(LaserColor color)
    {
        this.color = Filter(this.color, color);
        return this;
    }

    public static Laser Filter(Laser laser, LaserColor color)
    {
        return new Laser(Filter(laser.color, color), laser.power);
    }

    private static LaserColor Filter(LaserColor color1, LaserColor color2)
    {
        return color1 & color2;
    }

    private static int Combine(int power1, int power2, bool combinePower)
    {
        return combinePower ? power1 + power2 : MaxPower(power1, power2);
    }

    private static int MaxPower(int power1, int power2)
    {
        return power1 >= power2 ? power1 : power2;
    }

    public static bool operator ==(Laser left, Laser right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Laser left, Laser right)
    {
        return !Equals(left, right);
    }

    public static bool operator >=(Laser left, Laser right)
    {
        return Filter(left.color, right.color) == right.color &&
            left.power >= right.power;
    }

    public static bool operator <=(Laser left, Laser right)
    {
        return Filter(left.color, right.color) == left.color &&
            left.power <= right.power;
    }

    public override bool Equals(object obj)
    {
        Laser laser = obj as Laser;

        if (laser == null) return false;

        return color == laser.color && power == laser.power;
    }

    public override int GetHashCode()
    {
        var hash = (int)color;
        hash += power * 1000000;

        return hash;
    }

    public override string ToString()
    {
        return string.Format("Laser:[{0}, {1}]", color, power);
    }
}
