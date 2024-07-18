using System;
using ImpliciX.Language.Model;

namespace ImpliciX.Language.Control
{
    public class AlwaysDsl<TState> where TState : Enum
    {
        private readonly SubSystemDefinition<TState> _ssd;

        public AlwaysDsl(SubSystemDefinition<TState> ssd)
        {
            _ssd = ssd;
        }
        
        public SubSystemDsl<TState> Initial(TState state)
        {
            _ssd.InitialState = state;
            return new SubSystemDsl<TState>(_ssd);
        }
                
        public AlwaysDsl<TState> Set<T>(PropertyUrn<T> propertyUrn, FuncRef funcRef, PropertyUrn<FunctionDefinition> funcDef, params Urn[] xUrn)
        {
            _ssd.Always._setWithComputations.Add(new SetWithComputation(propertyUrn,funcRef,funcDef,xUrn));
            return this;
        }

        public SetForAlwaysDsl<TState,T> Set<T>(PropertyUrn<T> setUrn)
        {
            var set = new SetForAlwaysDsl<TState,T>(_ssd, setUrn);
            return set;
        }
    }

    public class SetForAlwaysDsl<TState,TValue> where TState : Enum
    {
        private readonly SubSystemDefinition<TState> _ssd;
        private readonly Urn _setUrn;

        public SetForAlwaysDsl(SubSystemDefinition<TState> ssd, Urn setUrn)
        {
            _ssd = ssd;
            _setUrn = setUrn;
            if (!_ssd.Always._setWithConditions.ContainsKey(setUrn))
            {
                _ssd.Always._setWithConditions[setUrn] = new SetWithConditions(){_setUrn = setUrn};
            }
        }

        public WithDsl<TState,TValue> With(TValue value)
        {
            var with = new WithDsl<TState,TValue>(_ssd, _setUrn, value);
            return with;
        }
        
        public WithDsl<TState,TValue> With(PropertyUrn<TValue> valueUrn)
        {
            var with = new WithDsl<TState,TValue>(_ssd, _setUrn, valueUrn);
            return with;
        }
    }

    public class WithDsl<TState,TValue> where TState : Enum
    {
        private readonly SubSystemDefinition<TState> _ssd;
        private readonly Urn _setUrn;
        private readonly TValue _value;
        private readonly PropertyUrn<TValue> _valueUrn;
        private readonly bool _isValueUrn;

        public WithDsl(SubSystemDefinition<TState> ssd, Urn setUrn, TValue value)
        {
            _ssd = ssd;
            _setUrn = setUrn;
            _value = value;
        }
        
        public WithDsl(SubSystemDefinition<TState> ssd, Urn setUrn, PropertyUrn<TValue> valueUrn)
        {
            _ssd = ssd;
            _setUrn = setUrn;
            _valueUrn = valueUrn;
            _isValueUrn = true;
        }

        public SetForAlwaysDsl<TState,TValue> When(ConditionDefinition conditionDefinition)
        {
            _ssd.Always._setWithConditions[_setUrn]._withs.Add(new With()
            {
                _value = _value,
                _valueUrn = _valueUrn,
                _isValueUrn = _isValueUrn,
                _conditionDefinition = conditionDefinition,
                _isOtherwise = false,
            });
            
            return new SetForAlwaysDsl<TState,TValue>(_ssd, _setUrn);
        }

        public OtherwiseDsl<TState> Otherwise()
        {

            _ssd.Always._setWithConditions[_setUrn]._withs.Add(new With()
            {
                _value = _value,
                _valueUrn = _valueUrn,
                _isValueUrn = _isValueUrn,
                _isOtherwise = true
            });

            return new OtherwiseDsl<TState>(_ssd);
        }
    }

    public class OtherwiseDsl<TState> where TState : Enum
    {
        private readonly SubSystemDefinition<TState> _ssd;

        public OtherwiseDsl(SubSystemDefinition<TState> ssd)
        {
            _ssd = ssd;
        }

        public SubSystemDsl<TState> Initial(TState state)
        {
            _ssd.InitialState = state;
            return new SubSystemDsl<TState>(_ssd);
        }

        public SetForAlwaysDsl<TState,T> Set<T>(PropertyUrn<T> setUrn)
        {
            return new SetForAlwaysDsl<TState,T>(_ssd, setUrn);
        }
    }
}