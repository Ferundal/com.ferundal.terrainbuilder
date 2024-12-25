using System.Collections.Generic;
using Editor._Scripts._Model.FSM.States;

namespace Editor._Scripts._Model.FSM
{
    public class TerrainBuilderFSM
    {
        public string CurrentStateName = null;
        private readonly StateFactory _stateFactory;
        private State _currentState = null;
        public List<string> StateNames => _stateFactory.StateNames;

        private bool _isActive = false;
        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                if (_isActive)
                {
                    _currentState.OnEnter();
                }
                else
                {
                    _currentState.OnExit();
                }

            }
        }

        public TerrainBuilderFSM()
        {
            _stateFactory = new StateFactory();
        }

        public void OnDestroy() { }

        public TerrainBuilderFSM AddState<T>(string name) where T : State, new()
        {
            _stateFactory.RegisterState<T>(name);
            return this;
        }

        public void Transition(string stateName)
        {
            if (stateName == CurrentStateName) return;

            _currentState?.OnExit();
            _currentState = _stateFactory.CreateState(stateName);
            _currentState.OnEnter();
        }

        public void OnSceneGUI()
        {
            if (!IsActive) return;
        }

        private void CreateNavigationPanel()
        {
            
        }
    }
}