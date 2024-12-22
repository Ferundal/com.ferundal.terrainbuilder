using Editor._Scripts._Model.FSM.States;

namespace Editor._Scripts._Model.FSM
{
    public class TerrainBuilderFSM
    {
        public State CurrentState { get; private set; } = null;

        private bool _isActive = false;
        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                if (_isActive)
                {
                    CurrentState.OnEnter();
                }
                else
                {
                    CurrentState.OnExit();
                }

            }
        }

        public TerrainBuilderFSM()
        {
            
        }

        public void OnDestroy() { }

        public void Add(State newState)
        {
            if (CurrentState != null) return;
            
            CurrentState = newState;
        }

        public void Transition(State state)
        {
            CurrentState?.OnExit();
            CurrentState = state;
            CurrentState.OnEnter();
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