using System;
using System.Collections.Generic;
using TerrainGeneration.MeshGeneration;
using UnityEngine;

namespace TerrainGeneration.ModelLevel
{
    [Serializable]
    public class Shape
    {
        [NonSerialized] public Level ParentLevel;
        
        public string shapeName;
        public List<Voxel> voxels = new();
        
        public Shape () {}

        public Shape(string shapeName)
        {
            this.shapeName = shapeName;
        }

        public MeshInfo MeshInfo
        {
            get
            {
                MeshInfo meshInfo = new();

                var multiviewProjection = MultiviewProjection;

                foreach (var view in multiviewProjection)
                {
                    var viewMesh = new MeshInfo(view.MeshInfo);
                    viewMesh.AddOffsetToIndexes(meshInfo.Points.Count);
                    meshInfo.Points.AddRange(viewMesh.Points);
                    meshInfo.TrianglesVertexIndexes.AddRange(viewMesh.TrianglesVertexIndexes);
                }

                return meshInfo;
            }
        }

        public List<Surface> MultiviewProjection
        {
            get
            {
                var multiviewProjection = new Dictionary<Vector3Int, List<Side>>();

                foreach (var voxel in voxels)
                {
                    foreach (var side in voxel.sides)
                    {
                        if (!multiviewProjection.ContainsKey(side.sideDirection))
                        {
                            multiviewProjection.Add(side.sideDirection, new List<Side>());
                        }

                        multiviewProjection[side.sideDirection].Add(side);
                    }
                }

                var surfaces = new List<Surface>();

                foreach (var view in multiviewProjection)
                {
                    surfaces.Add(new Surface(view.Value, view.Key));
                }
                return surfaces;
            }
        }
    }
}