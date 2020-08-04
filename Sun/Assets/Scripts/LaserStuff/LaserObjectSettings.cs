using UnityEngine;

[CreateAssetMenu(fileName = "LaserObjectSettings", menuName = "LaserObject/Settings")]
public class LaserObjectSettings : ScriptableObject
{
    [SerializeField]
    [Min(0)]
    public float inputLaserStopDelay;
}
