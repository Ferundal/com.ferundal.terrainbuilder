using System;
using System.Collections.Generic;
using System.Linq;

namespace Editor._Scripts._Model.FSM.States
{
    internal class StateFactory
    {
        private readonly Dictionary<string, Func<State>> _stateFactories = new();
        
        internal List<string> StateNames => _stateFactories.Keys.ToList();

        public void RegisterState<T>(string name) where T : State, new()
        {
            _stateFactories[name] = () => new T();
        }

        public State CreateState(string name)
        {
            if (_stateFactories.TryGetValue(name, out var factory))
            {
                return factory();
            }

            throw new InvalidOperationException($"State not found: {name}");
        }
    }
}