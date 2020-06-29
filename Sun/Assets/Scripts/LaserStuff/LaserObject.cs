using System;
using System.Collections.Generic;
using UnityEngine;

public enum Face
{
    Front,
    Back,
    Left,
    Right,
    Top,
    Bottom
}

public enum LaserObjectType
{
    Starter,
    PrimarySplitter,
    Combiner
}

[Serializable]
public class LaserObject : MonoBehaviour
{
    // TODO: separate functionality better...  scriptable object maybe??

    [SerializeField]
    private LaserObjectType type;

    // TODO: make list of modules???

    [SerializeField]
    private Module front;

    [SerializeField]
    private Module back;

    [SerializeField]
    private Module left;

    [SerializeField]
    private Module right;

    [SerializeField]
    private Laser starterLaser;

    [SerializeField]
    private Laser PowerRequirement;
    private Laser combinedPowerLaser = Laser.NullLaser;
    public bool IsPowered => FindAllModules<PowerInputModule>().Count <= 0 || combinedPowerLaser >= PowerRequirement;

    private Laser combinedManipulationLaser = Laser.NullLaser;

    private Collider collider;

    private void Awake()
    {
        collider = GetComponent<Collider>();
    }

    private void Start()
    {
        if (type == LaserObjectType.Starter)
        {
            UpdateOutput();
        }
    }

    public void UpdatePowerInput()
    {
        var inputModules = FindAllModules<PowerInputModule>();
        combinedPowerLaser = CombinedLaser(inputModules);

        Debug.Log($"<b>{typeof(LaserObject)}:</b> <i>UPDATING</i> combined power input: <b>{combinedPowerLaser}</b>");
    }

    public void UpdateManipulationInput()
    {
        var inputModules = FindAllModules<ManipulationInputModule>();
        combinedManipulationLaser = CombinedLaser(inputModules);

        Debug.Log($"<b>{name}({typeof(LaserObject)}):</b> <i>UPDATING</i> combined manipulation input: <b>{combinedManipulationLaser}</b>");

        UpdateOutput();
    }

    private void UpdateOutput()
    {
        var outputModules = FindAllModules<OutputModule>();

        if (outputModules.Count <= 0) return;

        switch (type)
        {
            case LaserObjectType.Starter:
                foreach (var module in outputModules)
                {
                    module.UpdateLaser(starterLaser, module.FaceDirection);
                }

                break;
            case LaserObjectType.PrimarySplitter:
                foreach (var module in outputModules)
                {
                    module.UpdateLaser(Laser.Filter(combinedManipulationLaser, LaserColor.Red),
                        LaserUtil.GetNextDirection(module.FaceDirection, false));
                    module.UpdateLaser(Laser.Filter(combinedManipulationLaser, LaserColor.Green),
                        module.FaceDirection);
                    module.UpdateLaser(Laser.Filter(combinedManipulationLaser, LaserColor.Blue),
                        LaserUtil.GetNextDirection(module.FaceDirection, true));
                }

                break;
        }
    }

    private Laser CombinedLaser<T>(List<T> inputModules) where T : InputModule
    {
        // Combine lasers from each input module
        Laser combinedLaser = Laser.NullLaser;
        foreach (var module in inputModules)
        {
            combinedLaser.Combine(module.CombinedLaser);
        }
        return combinedLaser;
    }

    private List<T> FindAllModules<T>()
        where T : Module
    {
        List<T> modules = new List<T>();

        if (front is T) modules.Add((T)front);
        if (back is T) modules.Add((T)back);
        if (left is T) modules.Add((T)left);
        if (right is T) modules.Add((T)right);

        return modules;
    }

    public void SetColliderEnabled(bool enabled = true)
    {
        if (collider != null)
        {
            collider.enabled = enabled;
        }
    }
}