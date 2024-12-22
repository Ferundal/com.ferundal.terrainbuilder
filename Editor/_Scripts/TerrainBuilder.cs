#if UNITY_EDITOR

using TerrainGeneration.TerrainDataModel;
using UnityEngine;

namespace Editor.Scripts
{
    [ExecuteInEditMode]
    public class TerrainBuilder : MonoBehaviour, ISerializationCallbackReceiver
    {
        [SerializeField] public TerrainScriptableObject terrainScriptableObject;
        
        public TerrainBuilderEventHandler Handler { get; } = new();

        public void OnBeforeSerialize()
        {
            Handler.HasUnprocessedSerialization = true;
        }

        public void OnAfterDeserialize()
        {
            Handler.HasUnprocessedDeserialization = true;
        }
    }
}

#endif
