using System;
using ImpliciX.Language.Core;
namespace ImpliciX.Language.Control
{
    public class Define<TState> where TState : Enum
    {
        public Option<TState> _parentState;
        public FragmentDefinition<TState> _fragment;

        public readonly TState _stateToConfigure;
        public readonly SubSystemDefinition<TState> _subSystemDefinition;

        public readonly Transitions<TState> Transitions;
        public readonly OnEntry OnEntry;
        public readonly OnState OnState;
        public readonly OnExit OnExit;

        public bool _isInitialSubState;
        public bool _isLeaf;

        public Define(SubSystemDefinition<TState> subSystem, TState stateToConfigure)
        {
            _subSystemDefinition = subSystem;
            _stateToConfigure = stateToConfigure;
            _isLeaf = true;
            _parentState = Option<TState>.None();

            Transitions = new Transitions<TState>();
            OnEntry = new OnEntry();
            OnExit = new OnExit();
            OnState = new OnState();
        } 
    }
}