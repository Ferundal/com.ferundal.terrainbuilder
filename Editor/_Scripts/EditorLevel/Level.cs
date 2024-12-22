using System.Collections.Generic;
using TerrainGeneration.ModelLevel;
using UnityEngine;

namespace Editor.Scripts.EditorLevel
{
    public class Level
    {
        public List<Shape> Shapes = new();
        public ThreeDimensionalMatrix<Voxel> VoxelMatrix = new ();

        private TerrainGeneration.ModelLevel.Level _levelSO;
        private GameObject _rootGameObject;
        private TerrainBuilder _terrainBuilder;

        public Level(TerrainGeneration.ModelLevel.Level levelSO, TerrainBuilder terrainBuilder)
        {
            _levelSO = levelSO;
            _rootGameObject = terrainBuilder.gameObject;
            _terrainBuilder = terrainBuilder;
            
            foreach (var shape in _levelSO.shapes)
            {
                var newShape = Shape.Create(shape, terrainBuilder, VoxelMatrix);
                Shapes.Add(newShape);
            }
        }

        public Shape AddShape(string shapeName)
        {
            var shapeSO = new TerrainGeneration.ModelLevel.Shape();
            shapeSO.shapeName = shapeName;
            shapeSO.ParentLevel = _levelSO;
            _levelSO.shapes.Add(shapeSO);
            
            var newShape = Shape.Create(shapeSO, _terrainBuilder, VoxelMatrix);
            Shapes.Add(newShape);
            return newShape;
        }
    }
}