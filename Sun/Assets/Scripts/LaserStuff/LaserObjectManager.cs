using UnityEngine;

public class LaserObjectManager : Singleton<LaserObjectManager>
{
    [SerializeField]
    private LaserObjectSettings settings;

    public LaserObjectSettings Settings => settings;
}
