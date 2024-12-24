using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Graphs;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor._Scripts._View
{
    public class InspectorUI
    {
        private const string NavigationButtonName = "navigation-button";
        private const string PanelBodyName = "body";
        
        private Dictionary<string, string> _panelSources;
        private Dictionary<string, string> _panelStyleSources;

        private VisualElement _loadingPanel;
        private int _statePanelBodyIndex;
        
        public VisualElement NavigationButtons { get; private set; }
        public VisualElement Root { get; private set; }

        public InspectorUI()
        {
            _panelSources = new Dictionary<string, string>();
            _panelStyleSources = new Dictionary<string, string>();

            Root = new VisualElement();
            NavigationButtons = new VisualElement();
            Root.Add(NavigationButtons);

            _loadingPanel = CreateLoadingLabel();
            Root.Add(_loadingPanel);
            _statePanelBodyIndex = Root.IndexOf(_loadingPanel);
        }

        public InspectorUI AddPanel(string stateName, string visualTreeAssetPath, string stylesheetPath)
        {
            _panelSources.Add(stateName, visualTreeAssetPath);
            _panelStyleSources.Add(stateName, stylesheetPath);

            AddStatePanelSwitchButton(stateName, visualTreeAssetPath, stylesheetPath);
            return this;
        }

        public void SetStatePanel(string stateName)
        {
            var visualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(_panelSources[stateName]);
            var instantiatedTemplate = visualTreeAsset.Instantiate();

            var statePanel = instantiatedTemplate.Q<VisualElement>(PanelBodyName);
            statePanel.RemoveFromHierarchy();
            var stylesheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(_panelStyleSources[stateName]);
            statePanel.styleSheets.Add(stylesheet);
            
            Root.RemoveAt(_statePanelBodyIndex);
            Root.Insert(_statePanelBodyIndex, statePanel);
        }

        private void AddStatePanelSwitchButton(string stateName, string visualTreeAssetPath, string stylesheetPath)
        {
            var visualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(visualTreeAssetPath);
            var statePanelTemplateContainer = visualTreeAsset.Instantiate();
            
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(stylesheetPath);
            var navigationButton = statePanelTemplateContainer.Q<Button>(NavigationButtonName);
            navigationButton.RemoveFromHierarchy();
            navigationButton.name = stateName;
            navigationButton.styleSheets.Add(styleSheet);
            NavigationButtons.Add(navigationButton);
        }
        
        // Method to create the loading message label
        private Label CreateLoadingLabel()
        {
            var label = new Label("Loading... Please wait...");

            // Styling the label
            label.style.color = new Color(0.2f, 0.2f, 0.2f); // Dark gray color for text (easier on the eyes than white)
            label.style.backgroundColor = new Color(0.9f, 0.9f, 0.9f); // Light gray background (more neutral than yellow)

            // Border styling
            label.style.borderTopWidth = 2;
            label.style.borderBottomWidth = 2;
            label.style.borderLeftWidth = 2;
            label.style.borderRightWidth = 2;
            label.style.borderTopColor = new Color(0.4f, 0.6f, 0.8f); // Blue color for the top border
            label.style.borderBottomColor = new Color(0.4f, 0.6f, 0.8f); // Blue color for the bottom border
            label.style.borderLeftColor = new Color(0.4f, 0.6f, 0.8f); // Blue color for the left border
            label.style.borderRightColor = new Color(0.4f, 0.6f, 0.8f); // Blue color for the right border

            // Padding styling
            label.style.paddingLeft = 10;
            label.style.paddingRight = 10;
            label.style.paddingTop = 5;
            label.style.paddingBottom = 5;

            return label;
        }
    }
}