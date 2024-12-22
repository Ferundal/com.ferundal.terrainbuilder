using TerrainGeneration.ModelLevel;

namespace TerrainGeneration.MeshGeneration.VoxelMeshGenerators
{
    public class Empty : IVoxelMeshGenerator
    {
        public MeshInfo GenerateSideMeshInfo(Side side)
        {
            var meshInfo = new MeshInfo();

            return meshInfo;
        }

        public void GenerateMesh(Voxel voxel)
        {
            foreach (var side in voxel.sides)
            {
                
            }
        }
    }
}