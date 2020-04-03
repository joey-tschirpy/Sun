using System.Collections.Generic;
using UnityEngine;
using Zenject;

public struct HitObject
{
    public readonly LaserObject LaserObject;
    public readonly Face Face;

    public HitObject(LaserObject lo, Face f)
    {
        LaserObject = lo;
        Face = f;
    }
}

public abstract class LaserObject : MonoBehaviour
{
    // TODO: inject grid (using grid interface) for accessing other laser objects
    [Inject]
    protected IGrid grid;

    [SerializeField]
    private ModuleType[] moduleTypes = new ModuleType[System.Enum.GetValues(typeof(Face)).Length];

    [SerializeField]
    protected Laser powerRequirement;

    protected Module[] modules;

    protected Laser previousPowerInput = new Laser();
    protected Laser previousManipulationInput = new Laser();

    protected Laser combinedPowerInput = new Laser();
    protected Laser combinedManipulationInput = new Laser();

    private int setting = 0;

    private bool IsPowered
    {
        get
        {
            if (combinedPowerInput == Laser.NullLaser || combinedPowerInput >= powerRequirement)
            {
                return true;
            }

            return false;
        }
    }

    protected virtual void Awake()
    {
        CreateModules();
    }

    private void CreateModules()
    {
        modules = new Module[moduleTypes.Length];

        for (int i = 0; i < modules.Length; i++)
        {
            var moduleAssigned = false;
            for (int j = 0; j < i; j++)
            {
                if (moduleTypes[i] == moduleTypes[j])
                {
                    Debug.LogFormat("Module {0} already exists. Assigning {1} face to existing {0}", moduleTypes[i], (Face)i);
                    modules[i] = modules[j];
                    modules[i].AddFace((Face)i);
                    moduleAssigned = true;
                    break;
                }
            }

            if (!moduleAssigned)
            {
                modules[i] = CreateModuleFromType(moduleTypes[i]);
                modules[i].AddFace((Face)i);
                Debug.LogFormat("Creating module {0} on {1} face", moduleTypes[i], (Face)i);
            }
        }
    }

    private Module CreateModuleFromType(ModuleType type)
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

    protected Module GetModule(ModuleType type)
    {
        foreach (var module in modules)
        {
            if (module.ModuleType == type)
            {
                return module;
            }
        }

        return null;
    }

    protected Face[] GetFaces(ModuleType type)
    {
        var faces = new List<Face>();

        for (int i = 0; i < modules.Length; i++)
        {
            if (modules[i].ModuleType == type)
            {
                faces.Add((Face)i);
            }
        }

        return faces.ToArray();
    }

    protected bool hasModule(ModuleType type)
    {
        foreach (var moduleType in moduleTypes)
        {
            if (moduleType == type)
            {
                return true;
            }
        }

        return false;
    }

    public void OnMove()
    {
        //TODO: turn off lasers
    }

    public void OnStable()
    {
        //TODO: restart starter lasers
    }

    public void ReceiveLaser(Laser laser, Face face, Direction direction)
    {
        var module = modules[(int)face];

        if (module is InputModule)
        {
            Debug.LogFormat("{0} received {1} on {2} face from a {3} direction", gameObject.name, laser, face, direction);
            ((InputModule)module).ReceiveLaser(laser, face, direction);
            SetupLasers();
        }
        else
        {
            Debug.LogFormat("{0} blocked {1} on {2} face from a {3} direction", gameObject.name, laser, face, direction);
        }
    }

    private void SetupLasers()
    {
        setting++;
        Debug.LogFormat("{0} setting: {1}", gameObject.name, setting);

        var inModule = GetModule(ModuleType.ManipulationInput);
        var outModule = GetModule(ModuleType.Output);

        if (IsPowered && inModule != null && outModule != null)
        {
            var input = (ManipulationInputModule)inModule;
            var output = (OutputModule)outModule;

            var outputLasers = input.manipulateFunction(input.GetCombinedLaser());
            for(int i = 0; i < outputLasers.Length; i++)
            {
                output.AssignLaser(outputLasers[i], (Direction)i);
            }
        }

        if (--setting <= 0)
        {
            var output = (OutputModule)outModule;

            output.SendLasers(grid, transform.position);
        }
        Debug.LogFormat("{0} setting: {1}", gameObject.name, setting);
    }
}