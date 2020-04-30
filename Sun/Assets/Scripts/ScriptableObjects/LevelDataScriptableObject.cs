
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelData")]
public class LevelDataScriptableObject : ScriptableObject
{
    public List<GameObject> BlocksInLevel;
    public ObjectGrid LevelObjectGrid;
    public MeshManager LevelMeshManager;
}
