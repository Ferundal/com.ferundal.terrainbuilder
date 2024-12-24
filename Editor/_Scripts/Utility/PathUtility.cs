using UnityEditor;

namespace Editor._Scripts.Utility
{
    public class PathUtility
    {
        public static string GetTerrainBuilderFolder()
        {
            return "Packages/com.ferundal.terrainbuilder";
        }

        public static string GetPathToFolder(string initialRelativePath, string targetFolderName)
        {
            var folderPaths = AssetDatabase.GetSubFolders(initialRelativePath);

            foreach (var folderPath in folderPaths)
            {
                if (folderPath.EndsWith(targetFolderName))
                {
                    return folderPath;
                }
            }

            return null;
        }
        
        public static string PanelsPath => $"{PathUtility.GetTerrainBuilderFolder()}/Editor/Panels";

        public static string VoxelPrefabsPath => $"{PathUtility.GetTerrainBuilderFolder()}/Editor/Prefabs";
    }
}