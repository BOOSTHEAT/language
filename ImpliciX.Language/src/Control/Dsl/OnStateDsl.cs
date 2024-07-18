using System;
using ImpliciX.Language.Model;

namespace ImpliciX.Language.Control
{
    public class OnStateDsl<TState> where TState : Enum
    {
        private SubSystemDefinition<TState> _ssd;
        private TState _state;

        public OnStateDsl(SubSystemDefinition<TState> ssd, TState state)
        {
            _ssd = ssd;
            _state = state;
        }

        public OnExitDsl<TState> OnExit => new OnExitDsl<TState>(_ssd,_state);
        public TransitionsDsl<TState> Transitions => new TransitionsDsl<TState>(_ssd, _state);

        public OnStateDsl<TState> Set<TCommand>(CommandNode<TCommand> commandUrn, FuncRef funcRef, PropertyUrn<FunctionDefinition> funcDef, params Urn[] xUrn)
        {
            _ssd._stateDefinitions[_state].OnState._setWithComputations.Add(new SetWithComputation(commandUrn,funcRef,funcDef,xUrn));
            return this;
        }

        public OnStateDsl<TState> Set<TProperty>(PropertyUrn<TProperty> propertyUrn, FuncRef funcRef, PropertyUrn<FunctionDefinition> funcDef, params Urn[] xUrn)
        {
            _ssd._stateDefinitions[_state].OnState._setWithComputations.Add(new SetWithComputation(propertyUrn,funcRef,funcDef,xUrn));
            return this;
        }

        public OnStateDsl<TState> SetPeriodical<TCommand>(CommandNode<TCommand> commandUrn, FuncRef funcRef, PropertyUrn<FunctionDefinition> funcDef, params Urn[] xUrn)
        {
            _ssd._stateDefinitions[_state].OnState._setPeriodical.Add(new SetPeriodical(commandUrn,funcRef,funcDef,xUrn));
            return this;
        }
        
        public OnStateDsl<TState> SetPeriodical<TProperty>(PropertyUrn<TProperty> propertyUrn, FuncRef funcRef, PropertyUrn<FunctionDefinition> funcDef, params Urn[] xUrn)
        {
            _ssd._stateDefinitions[_state].OnState._setPeriodical.Add(new SetPeriodical(propertyUrn,funcRef,funcDef,xUrn));
            return this;
        }

        public DefineDsl<TState> Define(TState state)
        {
            var define = new DefineDsl<TState>(_ssd,state);
            return define;
        }

        public OnStateDsl<TState> Set<T>(CommandUrn<T> urn, PropertyUrn<T> configurationUrn)
        {
            _ssd._stateDefinitions[_state].OnState._setWithProperties.Add(new SetWithProperty()
            {
                _urn = urn,
                _propertyUrn = configurationUrn
            });
            return this;
        }

        public OnStateDsl<TState> Set<T>(PropertyUrn<T> urn, PropertyUrn<T> configurationUrn)
        {
            _ssd._stateDefinitions[_state].OnState._setWithProperties.Add(new SetWithProperty()
            {
                _urn = urn,
                _propertyUrn = configurationUrn
            });
            return this;
        }
        
    }
}