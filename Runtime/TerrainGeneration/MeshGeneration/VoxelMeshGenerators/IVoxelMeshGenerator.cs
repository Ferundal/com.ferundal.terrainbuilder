using TerrainGeneration.ModelLevel;

namespace TerrainGeneration.MeshGeneration.VoxelMeshGenerators
{
    public interface IVoxelMeshGenerator
    {
        public MeshInfo GenerateSideMeshInfo(Side side);

        public void GenerateMesh(Voxel voxel);
    }
}