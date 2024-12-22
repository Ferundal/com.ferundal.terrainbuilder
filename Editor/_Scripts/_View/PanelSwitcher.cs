using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Graphs;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor._Scripts._View
{
    public class PanelSwitcher
    {
        private const string NavigationButtonName = "navigation-button";
        private const string PanelBodyName = "body";
        
        private Dictionary<string, string> _panelSources;
        private Dictionary<string, string> _panelStyleSources;
        private VisualElement _navigationButtons;
        private VisualElement _loadingPanel;
        private int _statePanelBodyIndex;

        public VisualElement Root { get; private set; }

        public PanelSwitcher()
        {
            _panelStyleSources = new Dictionary<string, string>();
            _panelStyleSources = new Dictionary<string, string>();

            Root = new VisualElement();
            _navigationButtons = new VisualElement();
            Root.Add(_navigationButtons);

            _loadingPanel = CreateLoadingLabel();
            Root.Add(_loadingPanel);
            _statePanelBodyIndex = Root.IndexOf(_loadingPanel);
        }

        public void Add(string stateName, string visualTreeAssetPath, string stylesheetPath)
        {
            _panelSources.Add(stateName, visualTreeAssetPath);
            _panelStyleSources.Add(stateName, stylesheetPath);

            AddStatePanelSwitchButton(stateName, visualTreeAssetPath, stylesheetPath);
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
            _navigationButtons.Add(navigationButton);
        }
        
        // Method to create the loading message label
        private Label CreateLoadingLabel()
        {
            var label = new Label("Loading... Please wait...");

            // Styling the label
            label.style.color = Color.white;
            label.style.backgroundColor = Color.yellow;
            label.style.borderTopWidth = 2;
            label.style.borderBottomWidth = 2;
            label.style.borderLeftWidth = 2;
            label.style.borderRightWidth = 2;
            label.style.borderTopColor = Color.red;
            label.style.borderBottomColor = Color.red;
            label.style.borderLeftColor = Color.red;
            label.style.borderRightColor = Color.red;
            label.style.paddingLeft = 10;
            label.style.paddingRight = 10;
            label.style.paddingTop = 5;
            label.style.paddingBottom = 5;

            return label;
        }
    }
}