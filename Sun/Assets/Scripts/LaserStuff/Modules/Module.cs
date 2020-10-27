using UnityEngine;

public class Module : MonoBehaviour, ILaserHittable
{
    protected static LaserObjectSettings settings;

    protected LaserObject laserObject;

    private Collider hitCollider;

    public Direction FaceDirection => LaserUtil.GetDirection(transform.position - laserObject.transform.position);

    protected bool[] isDirectionsHit; // Whether or not each direction is being hit by a laser

    protected virtual void Awake()
    {
        settings = LaserObjectManager.Instance.Settings;

        laserObject = transform.GetComponentInParent<LaserObject>();

        hitCollider = GetComponentInChildren<Collider>();

        isDirectionsHit = new bool[LaserUtil.DirectionCount];
    }

    private void LateUpdate()
    {
        for (int i = 0; i < isDirectionsHit.Length; i++)
        {
            isDirectionsHit[i] = false;
        }
    }

    public virtual void OnLaserHit(DirectionalLaser dirLaser, Vector3 hitPosition)
    {
        isDirectionsHit[(int)dirLaser.Direction] = !dirLaser.Laser.IsNullLaser;
    }

    public void SetColliderEnabled(bool enabled = true)
    {
        if (hitCollider != null)
        {
            hitCollider.enabled = enabled;
            laserObject.SetColliderEnabled(enabled);
        }
    }

    protected bool IsFrontHitFrom(Direction laserDirection)
    {
        return LaserUtil.IsObtuse(laserDirection, FaceDirection);
    }
}