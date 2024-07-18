using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using ImpliciX.Language.Model;

namespace ImpliciX.Language.Control
{
    public class SubSystemDefinition<TState> : ISubSystemDefinition where TState : Enum
    {
        public Urn ID => SubSystemNode.id;
        public SubSystemNode SubSystemNode { get; protected set; }
        public PropertyUrn<SubsystemState> StateUrn => SubSystemNode.state;
        public Type StateType => typeof(TState);
        public TState InitialState { get; set; }
        public virtual bool IsFragment => false;
        public virtual Urn ParentId { get; set; }


        public readonly OnState Always = new OnState();

        public readonly Dictionary<TState, Define<TState>> _stateDefinitions =
            new Dictionary<TState, Define<TState>>();

        public Dictionary<TState, Define<TState>> StateDefinitionsFlattened;

        public readonly ConcurrentDictionary<TState, HashSet<Urn>> _concernedUrnsByState =
            new ConcurrentDictionary<TState, HashSet<Urn>>();

        public readonly ConcurrentDictionary<TState, HashSet<Urn>> _concernedCommandRequestedUrnsByState =
            new ConcurrentDictionary<TState, HashSet<Urn>>();
        
        public readonly ConcurrentDictionary<TState, HashSet<Urn>> _concernedTimeoutOccuredUrnsByState =
            new ConcurrentDictionary<TState, HashSet<Urn>>();

        public readonly ConcurrentDictionary<TState, bool> _concernedUrnsBySystemTicked =
            new ConcurrentDictionary<TState, bool>();

        public readonly ConcurrentDictionary<TState, List<TState>> _chainsOfStates =
            new ConcurrentDictionary<TState, List<TState>>();

        public SubSystemDsl<TState> Subsystem(SubSystemNode subSystemNode)
        {
            SubSystemNode = subSystemNode;
            return new SubSystemDsl<TState>(this);
        }

        protected static SubsystemState NameOf<T>(T state) where T:Enum => SubsystemState.Create(state);
    }
}