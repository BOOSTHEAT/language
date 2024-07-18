using System;
using ImpliciX.Language.Model;
namespace ImpliciX.Language.Control
{
    public class WhenTimeout<TState> where TState : Enum
    {
        public  Urn _timerUrn;
        public TState _state;
    }
}