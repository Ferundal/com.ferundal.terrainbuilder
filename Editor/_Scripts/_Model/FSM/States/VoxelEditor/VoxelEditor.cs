using Editor._Scripts._Model.FSM.States;
using Editor._Scripts._View;
using Editor.Scripts;
using Editor.Scripts.FSM.States;
using Editor.Scripts.FSM.States.VoxelEditor;
using Editor.Scripts.VoxelRaycaster;
using UnityEngine;
using UnityEngine.UIElements;

namespace LevelConstructor
{
    public class VoxelEditor : State
    {
        
        public VoxelRaycaster Raycaster { get; private set; }
        
        private TerrainBuilderEventHandler _terrainBuilderEventHandler;
        private Editor.Scripts.TerrainBuilder _terrainBuilder;
        private VisualPanel _visualPanel;
        private ShapeManager _shapeManager;
        private BrushSelector _brushSelector;
        private Vector3Int _lastHit = new();
        private Vector3Int _newHit = new();

        public override VisualElement Root => Panel.Body;

        public VoxelEditor(VisualPanel panel, Editor.Scripts.TerrainBuilder terrainBuilder) : base(panel)
        {
            Raycaster = new VoxelRaycaster(terrainBuilder);
            _terrainBuilderEventHandler = terrainBuilder.Handler;
            _terrainBuilder = terrainBuilder;
            _visualPanel = panel;
        }

        public override void OnEnter()
        {   _brushSelector = new BrushSelector(_visualPanel, _terrainBuilder);
            _shapeManager = new ShapeManager(_visualPanel, _terrainBuilder);
            _terrainBuilderEventHandler.OnMouseMove += ChangeBrushPosition;
            _terrainBuilderEventHandler.OnMouseDown += UseBrush;
            base.OnEnter();
        }

        public override void OnExit()
        {
            _shapeManager?.OnDestroy();
            _terrainBuilderEventHandler.OnMouseMove -= ChangeBrushPosition;
            _terrainBuilderEventHandler.OnMouseDown -= UseBrush;
            base.OnExit();
        }

        private void UseBrush(Event currentEvent)
        {
            _brushSelector.CurrentBrush.UseBrush(_shapeManager.CurrentShape);
        }

        private void ChangeBrushPosition(Event currentEvent)
        {
            if (!Raycaster.Raycast(currentEvent.mousePosition, ref _newHit))
            {
                _brushSelector.CurrentBrush.IsActive = false;
                return;
            }
            if (_lastHit == _newHit)
            {
                return;
            }

            _lastHit = _newHit;
            _brushSelector.CurrentBrush.Position = _newHit;
            _brushSelector.CurrentBrush.IsActive = true;
        }
    }
}