using System;
using System.Collections.Generic;
using System.Linq;

namespace ImpliciX.Language.Control
{
    public class DslNavigator<TState, TOperation, TTransition>
        where TState : Enum
    {
        public IDictionary<string, Define<TState>> GetStates(SubSystemDefinition<TState> ss) =>
            ss._stateDefinitions.ToDictionary(s => s.Key.ToString(), s => s.Value);

        public Section<TSection> DefineSection<TSection>(Func<Define<TState>, TSection> localSection, Func<SubSystemDefinition<TState>, TSection> globalSection)
            => new Section<TSection>(this, localSection, globalSection);
        public Section<TSection> DefineSection<TSection>(Func<Define<TState>, TSection> localSection)
            => DefineSection(localSection, null);
        public Section<TSection> DefineSection<TSection>(Func<SubSystemDefinition<TState>, TSection> globalSection)
            => DefineSection(null, globalSection);
        
        public IEnumerable<TOperation> GetOperationsIn(Define<TState> stateDefinition)
            => _operations.SelectMany(d => d.ComputeOutputFor(stateDefinition));
        public IEnumerable<TOperation> GetGlobalOperations(SubSystemDefinition<TState> ss)
            => _operations.SelectMany(d => d.ComputeOutputFor(ss));
        
        public IDictionary<Define<TState>, IEnumerable<TTransition>> GetTransitionsFrom(Define<TState> stateDefinition,
            IDictionary<string, Define<TState>> allStateDefinitions)
        {
            var groups =
                from it in _transitions
                from outcome in it.ComputeOutputFor(stateDefinition, allStateDefinitions)
                group outcome by outcome.Item2
                into g
                select g;
            var d = groups.ToDictionary(g => g.Key, g => g.Select(x => x.Item1));
            return d;
        }
        
        public class Section<TSection>
        {
            private readonly DslNavigator<TState, TOperation, TTransition> _navigator;
            private readonly Func<Define<TState>, TSection> _localSection;
            private readonly Func<SubSystemDefinition<TState>, TSection> _globalSection;

            public Section(DslNavigator<TState, TOperation, TTransition> navigator,
                Func<Define<TState>, TSection> localSection, Func<SubSystemDefinition<TState>, TSection> globalSection)
            {
                _navigator = navigator;
                _localSection = localSection;
                _globalSection = globalSection;
            }

            public Section<TSection> DescribeOperation<TSectionItem>(
                Func<TSection, IEnumerable<TSectionItem>> gi,
                Func<TSectionItem, TOperation> f)
            {
                _navigator._operations.Add(new Operation<TSection, TSectionItem>(_localSection, _globalSection, gi, f));
                return this;
            }

            public Section<TSection> DescribeTransition<TSectionItem>(
                Func<TSection, IEnumerable<TSectionItem>> gi,
                Func<TSectionItem, TState> ts,
                Func<TSectionItem, TTransition> f)
            {
                _navigator._transitions.Add(new Transition<TSection, TSectionItem>(_localSection, gi, ts, f));
                return this;
            }
        }
        
        private readonly List<IOperate> _operations = new List<IOperate>();
        private readonly List<ITransit> _transitions = new List<ITransit>();

        private interface IOperate
        {
            IEnumerable<TOperation> ComputeOutputFor(Define<TState> s);
            IEnumerable<TOperation> ComputeOutputFor(SubSystemDefinition<TState> ss);
        }

        private class Operation<TSection, TSectionItem> : IOperate
        {
            private readonly Func<Define<TState>, TSection> _localSection;
            private readonly Func<SubSystemDefinition<TState>, TSection> _globalSection;
            private readonly Func<TSection, IEnumerable<TSectionItem>> _getItems;
            private readonly Func<TSectionItem, TOperation> _format;

            public Operation(Func<Define<TState>, TSection> localSection,
                Func<SubSystemDefinition<TState>, TSection> globalSection,
                Func<TSection, IEnumerable<TSectionItem>> gi,
                Func<TSectionItem, TOperation> f)
            {
                _localSection = localSection;
                _globalSection = globalSection;
                _getItems = gi;
                _format = f;
            }
            
            public IEnumerable<TOperation> ComputeOutputFor(Define<TState> s)
                => _localSection==null
                    ? Enumerable.Empty<TOperation>()
                    : from x in _getItems(_localSection(s)) select _format(x);

            public IEnumerable<TOperation> ComputeOutputFor(SubSystemDefinition<TState> ss)
                => _globalSection==null
                    ? Enumerable.Empty<TOperation>()
                    : from x in _getItems(_globalSection(ss)) select _format(x);

        }

        private interface ITransit
        {
            IEnumerable<(TTransition, Define<TState>)> ComputeOutputFor(Define<TState> s,
                IDictionary<string, Define<TState>> allStateDefinitions);
        }
        
        private class Transition<TSection, TSectionItem> : ITransit
        {
            private readonly Func<Define<TState>, TSection> _section;
            private readonly Func<TSection, IEnumerable<TSectionItem>> _getItems;
            private readonly Func<TSectionItem, TTransition> _format;
            private readonly Func<TSectionItem, TState> _targetState;

            internal Transition(Func<Define<TState>, TSection> s, Func<TSection, IEnumerable<TSectionItem>> gi,
                Func<TSectionItem, TState> ts, Func<TSectionItem, TTransition> f)
            {
                _section = s;
                _getItems = gi;
                _format = f;
                _targetState = ts;
            }

            public IEnumerable<(TTransition, Define<TState>)> ComputeOutputFor(Define<TState> s,
                IDictionary<string, Define<TState>> allStateDefinitions)
            {
                return from x in _getItems(_section(s))
                    let ts = _targetState(x).ToString()
                    select (_format(x), allStateDefinitions[ts]);
            }
        }
    }
}