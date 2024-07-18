using System;
using ImpliciX.Language.Model;
namespace ImpliciX.Language.Control
{
    public class WhenMessage<TState> where TState : Enum
    {
        public Urn _urn;
        public object _value;
        public TState _state;

    }
}