using System;
using ImpliciX.Language.Model;

namespace ImpliciX.Language.Control
{
    public class OnEntryDsl<TState> where TState : Enum
    {
        private SubSystemDefinition<TState> _ssd;
        private readonly TState _state;
        public OnStateDsl<TState> OnState => new OnStateDsl<TState>(_ssd,_state);
        public OnExitDsl<TState> OnExit => new OnExitDsl<TState>(_ssd,_state);
        public TransitionsDsl<TState> Transitions => new TransitionsDsl<TState>(_ssd, _state);

        public OnEntryDsl(SubSystemDefinition<TState> ssd, TState state)
        {
            _ssd = ssd;
            _state = state;
        }

        public OnEntryDsl<TState> Set(CommandUrn<NoArg> urn)
        {
            _ssd._stateDefinitions[_state].OnEntry._setsValues.Add(new SetValue()
            {
                _urn = urn,
                _value = default(NoArg)
            });
            return this;
        }

        public OnEntryDsl<TState> Set<T>(CommandUrn<T> urn, T value)
        {
            _ssd._stateDefinitions[_state].OnEntry._setsValues.Add(new SetValue()
            {
                _urn = urn,
                _value = value
                
            });
            return this;
        }

        public OnEntryDsl<TState> Set<T>(CommandUrn<T> urn, PropertyUrn<T> configurationUrn) => SetWithProperty(urn, configurationUrn);

        public OnEntryDsl<TState> Set<T>(PropertyUrn<T> operand1, PropertyUrn<T> operand2) => SetWithProperty(operand1, operand2);

        public OnEntryDsl<TState> Set<T>(PropertyUrn<T> propertyUrn, FuncRef funcRef, PropertyUrn<FunctionDefinition> funcDef, params Urn[] xUrn)
        {
            _ssd._stateDefinitions[_state].OnEntry._setsWithComputations.Add(new SetWithComputation(propertyUrn,funcRef,funcDef,xUrn));
            return this;
        }

        private OnEntryDsl<TState> SetWithProperty<T>(Urn operand1, PropertyUrn<T> operand2)
        {
            _ssd._stateDefinitions[_state].OnEntry._setsWithProperties.Add(new SetWithProperty()
            {
                _urn = operand1,
                _propertyUrn = operand2
            });
            return this;
        }

        public DefineDsl<TState> Define(TState state)
        {
            var define = new DefineDsl<TState>(_ssd, state);
            return define;
        }

        public OnEntryDsl<TState> StartTimer(PropertyUrn<Duration> timerUrn)
        {
            _ssd._stateDefinitions[_state].OnEntry._startTimers.Add(timerUrn);
            return this;
        }

        public OnEntryDsl<TState> Set<T>(PropertyUrn<T> operand1, T value)
        {
            _ssd._stateDefinitions[_state].OnEntry._setsValues.Add(new SetValue()
            {
                _urn = operand1,
                _value = value
                
            });
            return this;
        }
    }
}