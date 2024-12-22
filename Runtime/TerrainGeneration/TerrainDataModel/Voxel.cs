using System;
using System.Collections.Generic;
using UnityEngine;

namespace TerrainGeneration.TerrainDataModel
{
    [Serializable]
    public class Voxel
    {
        public VoxelType voxelType;
        
        public Vector3Int position;
        public List<Side> sides = new();
    }
}