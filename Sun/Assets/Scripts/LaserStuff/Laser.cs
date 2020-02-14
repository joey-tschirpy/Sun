using System.Collections.Generic;

public enum LaserColor
{
    K = 1 << 0,
    R = 1 << 1,
    G = 1 << 2,
    B = 1 << 4,
    Y = R | G,
    M = R | B,
    C = G | B,
    W = R | G | B
}

public class Laser
{
    private readonly LaserColor color;
    private readonly int power;

    public Laser(LaserColor color, int power)
    {
        this.color = color;
        this.power = power;
    }

    public static Laser Combine(Laser laser1, Laser laser2, bool combinePower)
    {
        LaserColor color = Combine(laser1.color, laser2.color);
        int power = combinePower ? laser1.power + laser2.power : MaxPower(laser1.power, laser2.power);

        return new Laser(color, power);
    }

    public static Laser Combine(List<Laser> lasers, bool combinePower)
    {
        if (lasers.Count <= 0)
        {
            return new Laser(LaserColor.K, 0);
        }
        if (lasers.Count == 1)
        {
            return lasers[0];
        }
        else
        {
            Laser laser = lasers[0];
            lasers.RemoveAt(0);

            return Combine(laser, Combine(lasers, combinePower), combinePower);
        }
    }

    private static LaserColor Combine(LaserColor color1, LaserColor color2)
    {
        return color1 | color2;
    }

    private static int MaxPower(int power1, int power2)
    {
        return power1 >= power2 ? power1 : power2;
    }
}
