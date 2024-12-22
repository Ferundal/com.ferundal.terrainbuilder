using Editor._Scripts._View;
using LevelConstructor;
using TerrainGeneration;
using UnityEngine;
using UnityEngine.UIElements;
using Utility;

namespace Editor.Scripts.FSM.States.VoxelEditor
{
    public class BrushSelector
    {
        private readonly VoxelType[] _voxelTypes;
        public Brush CurrentBrush;
        public BrushSelector(VisualPanel visualPanel, Editor.Scripts.TerrainBuilder terrainBuilder)
        {
            var objects = Resources.LoadAll($"{ResourcesPathUtility.VoxelTypeFolder}", typeof(VoxelType));
            _voxelTypes = new VoxelType [objects.Length];
            for (int i = 0; i < objects.Length; i++)
            {
                _voxelTypes[i] = (VoxelType)objects[i];
            }
                
            var brushesSelector = visualPanel.Body.Q<DropdownField>("brush_selector");
            brushesSelector.choices.Clear();

            foreach (var voxelType in _voxelTypes)
            {
                brushesSelector.choices.Add(voxelType.name);
            }
            
            CurrentBrush = new Brush(_voxelTypes[0], terrainBuilder);
            brushesSelector.value = _voxelTypes[0].name;
                
            brushesSelector.RegisterCallback<ChangeEvent<string>>((evt) =>
            {
                brushesSelector.value = evt.newValue;
                CurrentBrush.VoxelType = _voxelTypes[brushesSelector.index];
            });
        }
    }
}