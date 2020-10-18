using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILaserHittable
{
    public void OnLaserHit(DirectionalLaser dirLaser, Vector3 hitPosition);
}
