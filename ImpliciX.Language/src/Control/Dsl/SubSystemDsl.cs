using System;

namespace ImpliciX.Language.Control
{
    public class SubSystemDsl<TState> where TState : Enum
    {
        public SubSystemDefinition<TState> _ssd;
        public AlwaysDsl<TState> Always => new AlwaysDsl<TState>(_ssd);
        
        public SubSystemDsl(SubSystemDefinition<TState> ssd)
        {
            _ssd = ssd;
        }
        
        public SubSystemDsl<TState> Initial(TState state)
        {
            _ssd.InitialState = state;
            return this;
        }

        public DefineDsl<TState> Define(TState state)
        {
            var define = new DefineDsl<TState>(_ssd,state);
            return define;
        }
    }
}