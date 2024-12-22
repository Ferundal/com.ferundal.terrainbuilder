using System;
using TerrainGeneration.MeshGeneration.VoxelMeshGenerators;
using UnityEditor;
using UnityEngine;

namespace TerrainGeneration.TerrainDataModel
{
    [CreateAssetMenu(fileName = "NewVoxelType", menuName = "TerrainBuilder/New Voxel Type")]
    public class VoxelTypeScriptableObject : ScriptableObject
    {
        [SerializeField] private MonoScript meshGeneratorScript;
        public IVoxelMeshGenerator MeshGenerator;

        public Material material;

        private void OnEnable()
        {
            CreateMeshGenerator();
            InitializeMaterial();
        }

        private void InitializeMaterial()
        {
            if (material == null)
            {
                material = new Material(Shader.Find("Standard"));
            }
        }

        private void CreateMeshGenerator()
        {
            if (meshGeneratorScript == null)
            {
                Debug.LogWarning($"Mesh Generator Script is missing on {name}");
                MeshGenerator = null;
                return;
            }

            Type type = meshGeneratorScript.GetClass();
            if (type == null)
            {
                Debug.LogWarning($"The class in {meshGeneratorScript.name} could not be found.");
                MeshGenerator = null;
                return;
            }

            try
            {
                object instance = Activator.CreateInstance(type);
                MeshGenerator = (IVoxelMeshGenerator)instance;
            }
            catch (InvalidCastException)
            {
                Debug.LogWarning($"The class in {meshGeneratorScript.name} does not implement the IVoxelMeshGenerator interface.");
                MeshGenerator = null;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to create Mesh Generator: {ex.Message}");
                MeshGenerator = null;
            }
        }
    }
}