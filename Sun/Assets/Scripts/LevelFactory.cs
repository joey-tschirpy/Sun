using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct FloorGenerationData
{
    [Range(1, 50)]
    public int width, height;
    public int startX, startY;
}
public class LevelFactory
{
    public void GenerateLevelFromData(List<FloorGenerationData> data, out ObjectGrid newGrid, out MeshManager meshManager, int gameSpacing = 1)
    {
        int maxX = 0;
        int maxY = 0;
        foreach (var d in data)
        {
            if (d.startX + d.width > maxX) maxX = d.startX + d.width;
            if (d.startY + d.height > maxY) maxY = d.startY + d.height;
        }

        newGrid =  new ObjectGrid(data, maxX, maxY, gameSpacing);
        meshManager = new MeshManager(maxX, maxY, data.Count);
    }
}
