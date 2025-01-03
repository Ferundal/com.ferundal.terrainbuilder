using Editor._Scripts._Model.FSM;
using Editor._Scripts._Model.FSM.States.NavMeshBaker;
using Editor._Scripts._Model.FSM.States.SideEditor;
using Editor._Scripts._Model.FSM.States.VoxelEditor;
using Editor._Scripts._View;
using Editor._Scripts.Utility;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.Scripts
{
    /// <summary>
    /// Unity Editor tools for level construction
    /// </summary>
    [CustomEditor(typeof(TerrainBuilder))]
    public class TerrainBuilderEditor : UnityEditor.Editor
    {
        private TerrainBuilderView _terrainBuilderView;
        private TerrainBuilderFSM _terrainBuilderFSM;

        private TerrainBuilderEventHandler _terrainBuilderEventHandler;
        
        /// <summary>
        /// Initialization of objects independent from the parent script
        /// </summary>
        private void OnEnable()
        {
            _terrainBuilderView = new TerrainBuilderView(serializedObject);
            _terrainBuilderView.InspectorUI
                .AddPanel("voxelEditor",
                    $"{PathUtility.PanelsPath}/VoxelEditorPanel.uxml",
                    $"{PathUtility.PanelsPath}/Panel.uss")
                .AddPanel("sideEditor",
                    $"{PathUtility.PanelsPath}/SideEditorPanel.uxml",
                    $"{PathUtility.PanelsPath}/Panel.uss")
                .AddPanel("navMeshBaker",
                    $"{PathUtility.PanelsPath}/SideEditorPanel.uxml",
                    $"{PathUtility.PanelsPath}/Panel.uss");

            _terrainBuilderFSM = new TerrainBuilderFSM();
            _terrainBuilderFSM
                .AddState<VoxelEditor>("voxelEditor")
                .AddState<SideEditor>("sideEditor")
                .AddState<NavMeshBaker>("navMeshBaker");

            var terrainBuilder = target as TerrainBuilder;

            _terrainBuilderEventHandler = terrainBuilder.Handler;
        }
        
        public override VisualElement CreateInspectorGUI()
        {
            _terrainBuilderEventHandler.HasUnprocessedGUIStart = true;
            
            return _terrainBuilderView.Root;
        }
        
        private void OnDisable()
        {

        }


        private void OnSceneGUI()
        {
            //base.OnInspectorGUI();
            _terrainBuilderEventHandler.ProcessEvent(Event.current);
        }
        
        


        /*/// <summary>
        /// The method responsible for toggling the level editor interface based on the presence of a reference to the level object
        /// </summary>
        private void CheckLevelSerializedProperty()
        {
            if (_levelSerializedProperty.objectReferenceValue == null)
            {
                _fsm.IsActive = false;
                if (_root.childCount > 1)
                {
                    _root.RemoveAt(1);
                }
            }
            else
            {
                
                _fsm.IsActive = true;
                if (_root.childCount < 2)
                {
                    _root.Add(_fsm.Root);
                }
            }
        }
        
        /// <summary>
        /// Disables the standard Unity editor controls
        /// </summary>
        void OnDuringSceneGui(SceneView sceneView)
        {
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
        }*/
    }
}
