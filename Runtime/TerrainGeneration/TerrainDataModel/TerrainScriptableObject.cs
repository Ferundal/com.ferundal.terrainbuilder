using System.Collections.Generic;
using TerrainGeneration.ModelLevel;
using UnityEngine;

namespace TerrainGeneration.TerrainDataModel
{
    [CreateAssetMenu(fileName = "NewTerrain", menuName = "TerrainBuilder/New Terrain")]
    public class TerrainScriptableObject : ScriptableObject
    {
        public float voxelSize;
        public Vector3 zeroVoxelWorldOffset = new Vector3(0.5f, 0.5f, 0.5f);
        public List<Shape> shapes = new();
    }
}