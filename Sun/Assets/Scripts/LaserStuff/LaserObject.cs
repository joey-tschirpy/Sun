using UnityEngine;

public class LaserObject : MonoBehaviour
{
    [SerializeField]
    private ModuleType[] moduleTypes = new ModuleType[System.Enum.GetValues(typeof(Face)).Length];

    [SerializeField]
    protected Laser powerRequirement;

    protected IModulable[] modules;

    private void Start()
    {
        modules = new IModulable[moduleTypes.Length];

        for (int i = 0; i < modules.Length; i++)
        {
            modules[i] = GetModuleFromType(moduleTypes[i]);
        }
    }

    private IModulable GetModuleFromType(ModuleType type)
    {
        switch(type)
        {
            default:
            case ModuleType.Blank:
                return new BlankModule();
            case ModuleType.ManipulationInput:
                return new ManipulationInputModule();
            case ModuleType.PowerInput:
                return new PowerInputModule();
            case ModuleType.Output:
                return new OutputModule();
            case ModuleType.Information:
                return new InformationModule();
        }
    }
}