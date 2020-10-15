using System;
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

[Serializable]
public class Laser
{
    public static Color VisualColor(LaserColor color)
    {
        switch(color)
        {
            default:
            case LaserColor.Black:
                return UnityEngine.Color.black;
            case LaserColor.Red:
                return UnityEngine.Color.red;
            case LaserColor.Green:
                return UnityEngine.Color.green;
            case LaserColor.Blue:
                return UnityEngine.Color.blue;
            case LaserColor.Yellow:
                return UnityEngine.Color.yellow;
            case LaserColor.Magenta:
                return UnityEngine.Color.magenta;
            case LaserColor.Cyan:
                return UnityEngine.Color.cyan;
            case LaserColor.White:
                return UnityEngine.Color.white;
        }
    }

    // Serialized properties only needed for property drawer
    // TODO: Find a way to use auto properties in property drawer

    [SerializeField]
    private LaserColor color;
    public LaserColor Color { get { return color; } set { color = value; } }

    [SerializeField]
    private int power;
    public int Power { get { return power; } set { power = value; } }

    public Laser()
    {
        Color = LaserColor.Black;
        Power = 0;
    }

    public Laser(LaserColor color, int power)
    {
        Color = color;
        Power = power;
    }

    public Laser(Laser laser)
    {
        Color = laser.Color;
        Power = laser.Power;
    }

    public bool IsNullLaser => Color == LaserColor.Black;

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
        Color = color;
        Power = power;
    }

    public void Set(Laser laser)
    {
        Set(laser.Color, laser.Power);
    }

    public Laser Reset()
    {
        Set(LaserColor.Black, 0);
        return this;
    }

    public Laser Combine(Laser other, bool combinePower = true)
    {
        Combine(other.Color);
        Power = Combine(Power, other.Power, combinePower);
        return this;
    }

    public static Laser Combine(Laser laser1, Laser laser2, bool combinePower = true)
    {
        LaserColor color = Combine(laser1.Color, laser2.Color);
        int power = Combine(laser1.Power, laser2.Power, combinePower);

        return new Laser(color, power);
    }

    public static Laser Combine(Laser[] lasers, bool combinePower = true)
    {
        var combinedLaser = NullLaser;

        foreach(var laser in lasers)
        {
            combinedLaser.Combine(laser, combinePower);
        }

        return combinedLaser;

        //if (lasers.Length <= 0)
        //{
        //    return NullLaser;
        //}
        //if (lasers.Length == 1)
        //{
        //    return lasers[0];
        //}
        //else
        //{
        //    var laser = lasers[0];

        //    var laserList = lasers.ToList();
        //    laserList.Remove(laser);

        //    return Combine(laser, Combine(laserList, combinePower), combinePower);
        //}
    }

    public LaserColor Combine(LaserColor otherColor)
    {
        return Color = Combine(Color, otherColor);
    }

    private static LaserColor Combine(LaserColor color1, LaserColor color2)
    {
        return color1 | color2;
    }

    public Laser Filter(LaserColor color)
    {
        Color = Filter(Color, color);
        return this;
    }

    public static Laser Filter(Laser laser, LaserColor color)
    {
        return new Laser(Filter(laser.Color, color), laser.Power);
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
        return Filter(left.Color, right.Color) == right.Color &&
            left.Power >= right.Power;
    }

    public static bool operator <=(Laser left, Laser right)
    {
        return Filter(left.Color, right.Color) == left.Color &&
            left.Power <= right.Power;
    }

    public static bool operator >(Laser left, Laser right)
    {
        return !(left <= right);
    }

    public static bool operator <(Laser left, Laser right)
    {
        return !(left >= right);
    }

    public override bool Equals(object obj)
    {
        Laser laser = obj as Laser;

        if (laser == null) return false;

        return Color == laser.Color && Power == laser.Power;
    }

    public override int GetHashCode()
    {
        var hash = (int)Color;
        hash += Power * 1000000;

        return hash;
    }

    public override string ToString()
    {
        return $"<color={Color}>Laser:[{Color}, {Power}]</color>";
    }
}
