using System.Linq;
using Editor.Scripts.EditorLevel;
using TerrainGeneration;
using UnityEditor;
using UnityEngine;

namespace Editor.Scripts.FSM.States.VoxelEditor
{
    public class Brush
    {
        public bool IsActive;

        private Mesh _mesh;

        private VoxelType _voxelType;
        private Editor.Scripts.LevelConstructor _levelConstructor;

        private Vector3 _worldPosition;
        private Vector3Int _position;
        public Vector3Int Position
        {
            get => _position;
            set
            {
                _position = value;
                _worldPosition = _levelConstructor.levelSO.VoxelToWorldPosition(_position);
            }
        }
        
        public VoxelType VoxelType
        {
            get => _voxelType;
            set
            {
                _voxelType = value;

                var shape = CenteredVoxel.Shape(_voxelType, _levelConstructor.levelSO.voxelSize);
                _mesh = shape.MeshInfo.Mesh;
            }
        }

        public Brush(VoxelType voxelType, Editor.Scripts.LevelConstructor levelConstructor)
        {
            _levelConstructor = levelConstructor;
            VoxelType = voxelType;
        }

        public void UseBrush(Shape shape)
        {
            if (!IsActive) return;
            
            if (shape == null)
            {
                Debug.LogWarning("Select shape to edit");
                return;
            }
            
            shape.AddVoxel(VoxelType, _position);
            
            // TODO: [#1] Should be done in one array
            foreach (var direction in TerrainGeneration.ModelLevel.Voxel.SideDirections)
            {
                var neighbourVoxelPosition = direction + _position;
                
                var neighbourVoxel = _levelConstructor.EditorLevel.VoxelMatrix[neighbourVoxelPosition];
                
                if (neighbourVoxel == null || neighbourVoxel.VoxelSO.ParentShape != shape.shapeSO) continue;
                
                var side = neighbourVoxel.sides.FirstOrDefault(side => side.SideSO.sideDirection == -direction);
                
                if (side == null) continue;
                
                side.Destroy();
            }
        }

        public void Render()
        {
            if (!IsActive) return;
            
            VoxelType.Material.SetPass(0);
            Graphics.DrawMeshNow(_mesh, _worldPosition, Quaternion.identity, 1);
            HandleUtility.Repaint();
        }
    }
}