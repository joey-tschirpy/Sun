using UnityEngine;

public class Module : MonoBehaviour
{
    protected static LaserObjectSettings settings;

    protected LaserObject laserObject;

    private Collider collider;

    public Direction FaceDirection => LaserUtil.GetDirection(transform.position - laserObject.transform.position);

    protected virtual void Awake()
    {
        settings = LaserObjectManager.Instance.Settings;

        laserObject = transform.GetComponentInParent<LaserObject>();

        collider = GetComponent<Collider>();
    }

    public virtual void OnLaserHit(Laser laser, Direction direction, Vector3 hitPosition)
    {

    }

    public void SetColliderEnabled(bool enabled = true)
    {
        if (collider != null)
        {
            collider.enabled = enabled;
        }

        laserObject.SetColliderEnabled(enabled);
    }
}