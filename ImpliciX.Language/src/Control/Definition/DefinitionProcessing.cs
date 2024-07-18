using System;
using System.Collections.Generic;
using System.Linq;
using ImpliciX.Language.Core;
using ImpliciX.Language.Model;

namespace ImpliciX.Language.Control
{
    public static class DefinitionProcessing
    {
        public static void AddMetaDataToDefinition<TState>(SubSystemDefinition<TState> subSystemDefinition) where TState : Enum
        {
            FlagLeafStates(subSystemDefinition);
            ProcessFragments(Option<TState>.None(), subSystemDefinition);
            FlattenStateDefinitions(subSystemDefinition);
            BuildChainsOfStates(subSystemDefinition);
            PreprocessDefinitions(subSystemDefinition);
        }

        public static HashSet<Urn> GetCurrentConcernedUrnsForPropertiesChanged<TState>(
            SubSystemDefinition<TState> self, TState state) where TState : Enum =>
            self._concernedUrnsByState[state];


        public static HashSet<Urn> GetCurrentConcernedCommandRequestedUrns<TState>(SubSystemDefinition<TState> self, TState state) where TState : Enum
            => self._concernedCommandRequestedUrnsByState[state];

        public static HashSet<Urn> GetCurrentConcernedTimeoutUrns<TState>(SubSystemDefinition<TState> self, TState state) where TState : Enum
            => self._concernedTimeoutOccuredUrnsByState[state];

        public static bool IsConcernedBySystemTicked<TState>(SubSystemDefinition<TState> self, TState state)
            where TState : Enum =>
            self._concernedUrnsBySystemTicked[state];

        public static IEnumerable<TState> GetChainOfStates<TState>(SubSystemDefinition<TState> self, TState state)
            where TState : Enum => self._chainsOfStates[state];

        private static void PreprocessDefinitions<TState>(SubSystemDefinition<TState> subSystemDefinition)
            where TState : Enum
        {
            foreach (var definition in subSystemDefinition.StateDefinitionsFlattened.Values)
            {
                var state = definition._stateToConfigure;
                subSystemDefinition._concernedUrnsByState[state] = CurrentConcernedUrnsForPropertiesChanged(subSystemDefinition, state);
                subSystemDefinition._concernedUrnsBySystemTicked[state] = ConcernedBySystemTicked(subSystemDefinition, state);
                subSystemDefinition._concernedCommandRequestedUrnsByState[state] = CurrentConcernedUrnsForCommandRequested(subSystemDefinition, state);
                subSystemDefinition._concernedTimeoutOccuredUrnsByState[state] = CurrentConcernedUrnsForTimeoutOccured(subSystemDefinition, state);
            }
        }

        private static void BuildChainsOfStates<TState>(SubSystemDefinition<TState> subSystemDefinition)
            where TState : Enum
        {
            foreach (var definition in subSystemDefinition.StateDefinitionsFlattened.Values)
            {
                var state = definition;
                var states = new List<TState>();
                do
                {
                    states.Add(state._stateToConfigure);
                    state = state._parentState.IsSome
                        ? subSystemDefinition.StateDefinitionsFlattened[state._parentState.GetValue()]
                        : null;
                } while (state != null);

                states.Reverse();
                subSystemDefinition._chainsOfStates[definition._stateToConfigure] = states;
            }
        }

        private static HashSet<Urn> CurrentConcernedUrnsForPropertiesChanged<TState>(SubSystemDefinition<TState> self,
            TState state) where TState : Enum
        {
            if (self.StateDefinitionsFlattened.Count == 0)
                return new HashSet<Urn>();
            var currentChainOfStates = self._chainsOfStates[state];
            var statesDefinitions =
                self.StateDefinitionsFlattened.Values.Where(c => currentChainOfStates.Contains(c._stateToConfigure));
            return new HashSet<Urn>(
                statesDefinitions.SelectMany(c => GetUrnsConcernedByStateForPropertiesChanged(c).Append(self.StateUrn)));
        }

        private static bool ConcernedBySystemTicked<TState>(SubSystemDefinition<TState> self, TState state)
            where TState : Enum
        {
            if (self.StateDefinitionsFlattened.Count == 0)
                return false;
            var currentChainOfStates = self._chainsOfStates[state];
            var statesDefinitions =
                self.StateDefinitionsFlattened.Values.Where(c => currentChainOfStates.Contains(c._stateToConfigure));
            return statesDefinitions.Any(IsConcernedBySystemTicked);
        }

        private static HashSet<Urn> CurrentConcernedUrnsForCommandRequested<TState>(SubSystemDefinition<TState> self, TState state)
            where TState : Enum
        {
            if (self.StateDefinitionsFlattened.Count == 0)
                return new HashSet<Urn>();
            var currentChainOfStates = self._chainsOfStates[state];
            var statesDefinitions =
                self.StateDefinitionsFlattened.Values.Where(c => currentChainOfStates.Contains(c._stateToConfigure));
            return new HashSet<Urn>(
                statesDefinitions.SelectMany(c => c.Transitions._whenMessages.Select(c => c._urn)));
        }

        private static HashSet<Urn> CurrentConcernedUrnsForTimeoutOccured<TState>(SubSystemDefinition<TState> self, TState state)
            where TState : Enum
        {
            if (self.StateDefinitionsFlattened.Count == 0)
                return new HashSet<Urn>();
            var currentChainOfStates = self._chainsOfStates[state];
            var statesDefinitions =
                self.StateDefinitionsFlattened.Values.Where(c => currentChainOfStates.Contains(c._stateToConfigure));
            return new HashSet<Urn>(
                statesDefinitions.SelectMany(c => c.Transitions._whenTimeouts.Select(timeout => timeout._timerUrn)));
        }

        public static IEnumerable<Urn> GetUrnsConcernedByStateForPropertiesChanged<TState>(Define<TState> self)
            where TState : Enum =>
            new HashSet<Urn>(GetUrnsConcernedByOnState(self.OnState)
                .Concat(self.Transitions._whenConditions.SelectMany(c => c.Definition.GetUrns()))
                .Concat(GetUrnsConcernedByAlways(self._subSystemDefinition.Always))
            );

        private static IEnumerable<Urn> GetUrnsConcernedByOnState(OnState self) => new HashSet<Urn>(self
            ._setWithComputations.SelectMany(c => c.TriggersUrn)
            .Concat(self._setWithProperties.Select(c => c._propertyUrn)));

        public static IEnumerable<Urn> GetUrnsConcernedByAlways(OnState self) =>
            new HashSet<Urn>(
                self._setWithConditions.Values.SelectMany(c => c.TriggerUrns)
                .Concat(self._setWithComputations.SelectMany(c => c.TriggersUrn)));

        private static bool IsConcernedBySystemTicked<TState>(Define<TState> self)
            where TState : Enum => self.OnState._setPeriodical.Any();

        private static void FlagLeafStates<TState>(SubSystemDefinition<TState> subSystemDefinition)
            where TState : Enum
        {
            foreach (var define in subSystemDefinition._stateDefinitions.Values)
            {
                if (define._parentState.IsSome)
                    subSystemDefinition._stateDefinitions[define._parentState.GetValue()]._isLeaf = false;
                if (define._fragment != null)
                {
                    define._isLeaf = false;
                    FlagLeafStates(define._fragment);
                }
            }
        }

        private static void FlattenStateDefinitions<TState>(SubSystemDefinition<TState> subSystemDefinition)
            where TState : Enum
        {
            var values = subSystemDefinition._stateDefinitions.Values;
            var result = subSystemDefinition._stateDefinitions.ToList();
            foreach (var define in values)
            {
                if (define._fragment != null)
                {
                    FlattenStateDefinitions(define._fragment);
                    result.AddRange(define._fragment.StateDefinitionsFlattened);
                }
            }

            subSystemDefinition.StateDefinitionsFlattened = result.ToDictionary(c => c.Key, c => c.Value);
        }

        private static void ProcessFragments<TState>(Option<TState> parentState, SubSystemDefinition<TState> ssd)
            where TState : Enum
        {
            foreach (var stateDefinition in ssd._stateDefinitions.Values)
            {
                if (parentState.IsSome && stateDefinition._parentState.IsNone)
                    stateDefinition._parentState = parentState;
                if (stateDefinition._fragment != null)
                    ProcessFragments(stateDefinition._stateToConfigure, stateDefinition._fragment);
            }

            ssd._stateDefinitions[ssd.InitialState]._isInitialSubState = true;
        }
    }
}