using Editor._Scripts._Model.FSM;
using Editor._Scripts._View;
using UnityEngine.UIElements;

namespace Editor._Scripts._Controller
{
    public class TerrainBuilderController
    {
        private TerrainBuilderView _terrainBuilderView;
        
        public TerrainBuilderController(TerrainBuilderView terrainBuilderView)
        {
            _terrainBuilderView = terrainBuilderView;
        }

        public void Bind(TerrainBuilderFSM terrainBuilderFsm)
        {
            var stateNames = terrainBuilderFsm.StateNames;
            foreach (var stateName in stateNames)
            {
                _terrainBuilderView.InspectorUI.NavigationButtons.Q<Button>(stateName).clickable.clicked +=
                    () => terrainBuilderFsm.Transition(stateName);
            }
        }
    }
}