using UnityEngine;

public class LaserObject : MonoBehaviour
{
    [SerializeField]
    private ModuleType[] moduleTypes = new ModuleType[System.Enum.GetValues(typeof(Face)).Length];
}