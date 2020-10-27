using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum LaserObjectType
{
    PrimarySplitter,
    Combiner
}

[Serializable]
public class LaserObject : MonoBehaviour
{
    [SerializeField]
    private Collider hitCollider;

    [Header("Settings"), Space()]

    [SerializeField]
    private Laser PowerRequirement;
    private Laser combinedPowerLaser = Laser.NullLaser;
    public bool IsPowered => FindAllModules<PowerInputModule>().Count <= 0 || combinedPowerLaser >= PowerRequirement;

    private Module[] modules;

    private void Awake()
    {
        modules = GetComponentsInChildren<Module>();
    }

    /// <summary>
    /// Updates combined power with total combined laser from all power input modules
    /// </summary>
    public void UpdatePowerInput()
    {
        var inputModules = FindAllModules<PowerInputModule>();
        combinedPowerLaser = CombinedLaser(inputModules);

        Debug.Log($"<b>{typeof(LaserObject)}:</b> <i>UPDATING</i> combined power input: <b>{combinedPowerLaser}</b>");
    }

    /// <summary>
    /// Gets total combined laser from all manipulation input modules and tells output to update
    /// </summary>
    public void UpdateManipulationInput()
    {
        var inputModules = FindAllModules<ManipulationInputModule>();
        var combinedManipulationLaser = CombinedLaser(inputModules);

        Debug.Log($"<b>{name}({typeof(LaserObject)}):</b> <i>UPDATING</i> combined manipulation input: <b>{combinedManipulationLaser}</b>");

        UpdateOutput(combinedManipulationLaser);
    }

    /// <summary>
    /// Sends laser to each output for processing
    /// </summary>
    /// <param name="combinedLaser"> Laser to be processed by each output module </param>
    private void UpdateOutput(Laser combinedLaser)
    {
        var outputModules = FindAllModules<OutputModule>();
        foreach (var module in outputModules)
        {
            module.ProcessLaserInput(combinedLaser);
        }
    }

    public void SetColliderEnabled(bool enabled)
    {
        if (hitCollider != null)
        {
            hitCollider.enabled = enabled;
        }
    }

    /// <summary>
    /// Combines combined laser from each input module
    /// </summary>
    /// <typeparam name="T"> The class type extended from inputModule </typeparam>
    /// <param name="inputModules"> Input modules to get combined laser from </param>
    /// <returns> Total combined laser from all modules </returns>
    private Laser CombinedLaser<T>(List<T> inputModules) where T : InputModule
    {
        // Combine combinedLasers from each input module
        Laser combinedLaser = Laser.NullLaser;
        foreach (var module in inputModules)
        {
            combinedLaser.Combine(module.CombinedLaser);
        }
        return combinedLaser;
    }

    /// <summary>
    /// Finds all Modules of type 'T' and returns them as a list
    /// </summary>
    /// <typeparam name="T"> Type of module </typeparam>
    /// <returns> A list of found modules </returns>
    private List<T> FindAllModules<T>()
        where T : Module
    {
        return modules.OfType<T>().ToList();
    }
}