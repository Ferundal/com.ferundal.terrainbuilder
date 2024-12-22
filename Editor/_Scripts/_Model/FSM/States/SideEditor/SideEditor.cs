using Editor._Scripts._Model.FSM.States;
using Editor._Scripts._View;
using Editor.Scripts.EditorLevel;
using LevelConstructor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.Scripts.FSM.States.SideEditor
{
    public class SideEditor : State
    {
        private TerrainBuilderEditor _terrainBuilderEditor;

        public override VisualElement Root { get; protected set; } = new();

        public SideEditor(VisualPanel panel, TerrainBuilderEditor terrainBuilderEditor) : base(panel)
        {
            _terrainBuilderEditor = terrainBuilderEditor;
        }

        public override void OnEnter()
        {
            _terrainBuilderEditor.terrainBuilder.Handler.OnMouseDown += DestroySide;
            base.OnEnter();
        }

        public override void OnExit()
        {
            _terrainBuilderEditor.terrainBuilder.Handler.OnMouseDown -= DestroySide;
            base.OnExit();
        }
        

        private void DestroySide(Event currentEvent)
        {
            var ray = HandleUtility.GUIPointToWorldRay(currentEvent.mousePosition);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit)
                && raycastHit.collider.gameObject.TryGetComponent<Side>(out var side))
            {
                side.Destroy();
            }
        }
    }
}