using System;
using ImpliciX.Language.Model;

namespace ImpliciX.Language.Alarms
{
    public struct Triggers
    {
        public Urn Dependency { get; set; }
        public Func<IDataModelValue, bool>[] Predicates { get; set; }
    }
}