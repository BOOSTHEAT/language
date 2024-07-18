using System;
using ImpliciX.Language.Model;

namespace ImpliciX.Language.Control
{
    public class FragmentDefinition<TState> : SubSystemDefinition<TState>, IFragmentDefinition where TState : Enum
    {
        public override bool IsFragment => true;
        public override Urn ParentId { get; set; }

        protected FragmentDefinition()
        {
        }

        protected FragmentDsl<TState> Fragment(SubSystemNode self, SubSystemNode parent)
        {
            SubSystemNode = self;
            ParentId = parent.id;
            return new FragmentDsl<TState>(this);
        }

        public new SubSystemDsl<TState> Subsystem(SubSystemNode subSystemNode)
        {
            throw new NotImplementedException($"Use Fragment instead Subsystem function for {subSystemNode.id}");
        }
    }
}