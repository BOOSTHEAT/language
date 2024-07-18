using System;
using ImpliciX.Language.Model;

namespace ImpliciX.Language.Control
{
    public class TransitionsDsl<TState> where TState : Enum
    {
        private SubSystemDefinition<TState> _ssd;
        private readonly TState _state;

        public TransitionsDsl(SubSystemDefinition<TState> foo, TState state)
        {
            _ssd = foo;
            _state = state;
        }

        public DefineDsl<TState> Define(TState state)
        {
            return new DefineDsl<TState>(_ssd, state);
        }

        public WhenMessageDsl<TState> WhenMessage<T>(CommandUrn<T> urn, T value)
        {
            var whenMessage = new WhenMessageDsl<TState>(_ssd, _state, urn, value);
            return whenMessage;
        }

        public WhenMessageDsl<TState> WhenMessage(CommandUrn<NoArg> urn)
        {
            var whenMessage = new WhenMessageDsl<TState>(_ssd, _state, urn,default(NoArg));
            return whenMessage;
        }

        public WhenTimeoutDsl<TState> WhenTimeout(PropertyUrn<Duration> timer)
        {
            var whenTimeout = new WhenTimeoutDsl<TState>(_ssd, _state, timer);
            return whenTimeout;
        }

        public WhenDsl<TState> When(ConditionDefinition conditionDefinition)
        {
            var when = new WhenDsl<TState>(_ssd, _state, conditionDefinition);
            return when;
        }
    }

    public class WhenMessageDsl<TState> where TState : Enum
    {
        private readonly Urn _urn;
        private readonly object _value;
        private SubSystemDefinition<TState> _ssd;
        private readonly TState _state;

        public WhenMessageDsl(SubSystemDefinition<TState> foo, TState state, Urn urn, object value)
        {
            _ssd = foo;
            _state = state;
            _urn = urn;
            _value = value;
        }

        public TransitionsDsl<TState> Then(TState transitionState)
        {
            _ssd._stateDefinitions[_state].Transitions._whenMessages.Add(new WhenMessage<TState>()
            {
                _state = transitionState,
                _urn = _urn,
                _value = _value
            });
            return new TransitionsDsl<TState>(_ssd, _state);
        }
    }

    public class WhenDsl<TState> where TState : Enum
    {
        private SubSystemDefinition<TState> _ssd;
        private TState _state;
        private readonly ConditionDefinition _conditionDefinition;

        public WhenDsl(SubSystemDefinition<TState> foo, TState state, ConditionDefinition conditionDefinition)
        {
            _ssd = foo;
            _state = state;
            _conditionDefinition = conditionDefinition;
        }

        public TransitionsDsl<TState> Then(TState transitionState)
        {
            _ssd._stateDefinitions[_state].Transitions._whenConditions.Add(new When<TState>()
            {
                _target = transitionState,
                Definition = _conditionDefinition
            });
            return new TransitionsDsl<TState>(_ssd, _state);
        }
    }

    public class WhenTimeoutDsl<TState> where TState : Enum
    {
        private SubSystemDefinition<TState> _ssd;
        private TState _state;
        private readonly Urn _urn;

        public WhenTimeoutDsl(SubSystemDefinition<TState> foo, TState state, Urn urn)
        {
            _ssd = foo;
            _state = state;
            _urn = urn;
        }

        public TransitionsDsl<TState> Then(TState transitionState)
        {
            _ssd._stateDefinitions[_state].Transitions._whenTimeouts.Add(new WhenTimeout<TState>()
            {
                _timerUrn = _urn,
                _state = transitionState
            });
            return new TransitionsDsl<TState>(_ssd, _state);
        }
    }
}