using System;
using System.Collections.Generic;
namespace ImpliciX.Language.Control
{
    public class Transitions<TState> where TState : Enum
    {
        public readonly List<WhenTimeout<TState>> _whenTimeouts = new List<WhenTimeout<TState>>();
        public readonly List<WhenMessage<TState>> _whenMessages = new List<WhenMessage<TState>>();
        public readonly List<When<TState>> _whenConditions = new List<When<TState>>();
    }
}