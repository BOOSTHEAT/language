using System;

namespace ImpliciX.Language.Control
{
    public class DefineDsl<TState> where TState : Enum
    {
        private readonly SubSystemDefinition<TState> _ssd;
        private readonly TState _state;

        public OnEntryDsl<TState> OnEntry => new OnEntryDsl<TState>(_ssd, _state);
        public OnStateDsl<TState> OnState => new OnStateDsl<TState>(_ssd, _state);
        public OnExitDsl<TState> OnExit => new OnExitDsl<TState>(_ssd, _state);
        public TransitionsDsl<TState> Transitions => new TransitionsDsl<TState>(_ssd, _state);

        public DefineDsl(SubSystemDefinition<TState> ssd, TState state)
        {
            _ssd = ssd;
            _state = state;
            if (!_ssd._stateDefinitions.ContainsKey(state))
            {
                _ssd._stateDefinitions[state] = new Define<TState>(ssd, state);
            }
           
        }

        public AsSubStateOfDsl<TState> AsInitialSubStateOf(TState state)
        {
            _ssd._stateDefinitions[_state]._parentState = state;
            _ssd._stateDefinitions[_state]._isInitialSubState = true;
            var asSubStateOf = new AsSubStateOfDsl<TState>(_ssd, _state);
            return asSubStateOf;
        }

        public AsSubStateOfDsl<TState> AsSubStateOf(TState state)
        {
            _ssd._stateDefinitions[_state]._parentState = state;
            var asSubStateOf = new AsSubStateOfDsl<TState>(_ssd, _state);
            return asSubStateOf;
        }

        public DefineDsl<TState> Body(FragmentDefinition<TState> fragment)
        {
            _ssd._stateDefinitions[_state]._fragment = fragment;
            return this;
        }
    }

    public class AsSubStateOfDsl<TState> where TState : Enum
    {
        private readonly SubSystemDefinition<TState> _ssd;
        private readonly TState _state;

        public AsSubStateOfDsl(SubSystemDefinition<TState> ssd, TState state)
        {
            _ssd = ssd;
            _state = state;
        }

        public TransitionsDsl<TState> Transitions => new TransitionsDsl<TState>(_ssd, _state);
        public OnEntryDsl<TState> OnEntry => new OnEntryDsl<TState>(_ssd, _state);
        public OnStateDsl<TState> OnState => new OnStateDsl<TState>(_ssd, _state);
        public OnExitDsl<TState> OnExit => new OnExitDsl<TState>(_ssd, _state);
        public DefineDsl<TState> Body(FragmentDefinition<TState> fragment)
        {
            _ssd._stateDefinitions[_state]._fragment = fragment;
            return new DefineDsl<TState>(_ssd, _state);
        }
        
        public DefineDsl<TState> Define(TState state)
        {
            return new DefineDsl<TState>(_ssd, state);
        }
    }
}