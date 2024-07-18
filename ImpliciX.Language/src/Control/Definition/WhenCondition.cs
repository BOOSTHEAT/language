using System;

namespace ImpliciX.Language.Control
{
    public class When<TState> where TState : Enum
    {
        public ConditionDefinition Definition;
        public TState _target;
    }
}