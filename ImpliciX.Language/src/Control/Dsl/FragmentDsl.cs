using System;

namespace ImpliciX.Language.Control
{
    public class FragmentDsl<TState> where TState : Enum
    {
        private readonly SubSystemDefinition<TState> _ssd;

        public FragmentDsl(SubSystemDefinition<TState> ssd)
        {
            _ssd = ssd;
        }

        public FragmentDsl<TState> Initial(TState state)
        {
            _ssd.InitialState = state;
            return this;
        }

        public DefineDsl<TState> Define(TState state)
        {
            return new DefineDsl<TState>(_ssd, state);
        }
    }
}