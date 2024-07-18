using System;
using ImpliciX.Language.Model;

namespace ImpliciX.Language.Control
{
    public class OnExitDsl<TState> where TState : Enum
    {
        private SubSystemDefinition<TState> _ssd;
        private TState _state;
        public  TransitionsDsl<TState> Transitions => new TransitionsDsl<TState>(_ssd, _state);

        public OnExitDsl(SubSystemDefinition<TState> ssd, TState state)
        {
            _ssd = ssd;
            _state = state;
        }

        public DefineDsl<TState> Define(TState state)
        {
            var define = new DefineDsl<TState>(_ssd,state);
            return define;
        }

        public OnExitDsl<TState> Set<T>(CommandUrn<T> urn, T value)
        {
            _ssd._stateDefinitions[_state].OnExit._setsValues.Add(new SetValue()
            {
                _urn = urn,
                _value = value
                
            });
            return this;
        }
        
        public OnExitDsl<TState> Set(CommandUrn<NoArg> urn)
        {
            _ssd._stateDefinitions[_state].OnExit._setsValues.Add(new SetValue()
            {
                _urn = urn,
                _value = default(NoArg)
                
            });
            return this;
        }

        public OnExitDsl<TState> Set<T>(CommandUrn<T> command, PropertyUrn<T> property)
        {
            _ssd._stateDefinitions[_state].OnExit._setsWithProperty.Add(new SetWithProperty()
            {
                _urn = command,
                _propertyUrn = property
                
            });
            return this;
        }

        public OnExitDsl<TState> Set<T>(PropertyUrn<T> property, T value)
        {
            _ssd._stateDefinitions[_state].OnExit._setsValues.Add(new SetValue()
            {
                _urn = property,
                _value = value
                
            });
            return this;
        }

        public OnExitDsl<TState> Set<T>(PropertyUrn<T> destinationProperty, PropertyUrn<T> sourceProperty)
        {
            _ssd._stateDefinitions[_state].OnExit._setsWithProperty.Add(new SetWithProperty()
            {
                _urn = destinationProperty,
                _propertyUrn = sourceProperty
                
            });
            return this;
        }
        
       
    }
}