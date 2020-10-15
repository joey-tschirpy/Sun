using UnityEngine;

public class Module : MonoBehaviour
{
    protected static LaserObjectSettings settings;

    protected LaserObject laserObject;

    private Collider hitCollider;

    public Direction FaceDirection => LaserUtil.GetDirection(transform.position - laserObject.transform.position);

    protected bool[] directionsHit;

    protected virtual void Awake()
    {
        settings = LaserObjectManager.Instance.Settings;

        laserObject = transform.GetComponentInParent<LaserObject>();

        hitCollider = GetComponentInChildren<Collider>();

        directionsHit = new bool[LaserUtil.DirectionCount];
    }

    private void LateUpdate()
    {
        for (int i = 0; i < directionsHit.Length; i++)
        {
            directionsHit[i] = false;
        }
    }

    public virtual void OnLaserHit(DirectionalLaser dirLaser, Vector3 hitPosition)
    {
        directionsHit[(int)dirLaser.Direction] = !dirLaser.Laser.IsNullLaser;
    }

    public void SetColliderEnabled(bool enabled = true)
    {
        if (hitCollider != null)
        {
            hitCollider.enabled = enabled;
        }
    }

    protected bool IsFrontHitFrom(Direction laserDirection)
    {
        return LaserUtil.IsObtuse(laserDirection, FaceDirection);
    }
}