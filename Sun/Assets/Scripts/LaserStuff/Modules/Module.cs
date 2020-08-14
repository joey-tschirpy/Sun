using UnityEngine;

public class Module : MonoBehaviour
{
    protected static LaserObjectSettings settings;

    protected LaserObject laserObject;

    private Collider hitCollider;

    public Direction FaceDirection => LaserUtil.GetDirection(transform.position - laserObject.transform.position);

    protected virtual void Awake()
    {
        settings = LaserObjectManager.Instance.Settings;

        laserObject = transform.GetComponentInParent<LaserObject>();

        hitCollider = GetComponent<Collider>();
    }

    public virtual void OnLaserHit(Laser laser, Direction direction, Vector3 hitPosition)
    {

    }

    public void SetColliderEnabled(bool enabled = true)
    {
        if (hitCollider != null)
        {
            hitCollider.enabled = enabled;
        }
    }
}