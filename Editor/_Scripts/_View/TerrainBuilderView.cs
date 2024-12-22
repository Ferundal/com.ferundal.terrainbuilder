using System;
using System.Collections.Generic;
using Editor.Scripts;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Editor._Scripts._View
{
    public class TerrainBuilderView
    {
        public PanelSwitcher PanelSwitcher;
        
        private VisualElement _root;
        public VisualElement Root { get; private set; }

        public TerrainBuilderView(SerializedObject serializedTerrainBuilder)
        {
            Root = new VisualElement();

            var terrainScriptableObjectPropertyField = serializedTerrainBuilder.FindProperty(nameof(TerrainBuilder.terrainScriptableObject));
            var terrainScriptableObjectPropertyFieldView = new PropertyField(terrainScriptableObjectPropertyField);
            Root.Add(terrainScriptableObjectPropertyFieldView);

            PanelSwitcher = new PanelSwitcher();
            Root.Add(PanelSwitcher.Root);
        }
    }
}