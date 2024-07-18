using System.Collections.Generic;
using System.Linq;
using ImpliciX.Language.Model;
namespace ImpliciX.Language.Control
{
    public class SetWithConditions
    {
        public Urn _setUrn;
        public readonly List<With> _withs = new List<With>();
        public List<With> ConditionalsWith => _withs.Where(c => !c._isOtherwise).ToList();
        public With Otherwise => _withs.Single(c => c._isOtherwise);

        public Urn[] TriggerUrns => _withs.SelectMany(@with => @with.TriggerUrns).ToArray();
    }

    public class With
    {
        public object _value;
        public Urn _valueUrn;
        public bool _isValueUrn;
        public ConditionDefinition _conditionDefinition;
        public bool _isOtherwise;

        public IEnumerable<Urn> TriggerUrns
        {
            get
            {
                IEnumerable<Urn> result =
                    _isOtherwise ? new List<Urn>() : new List<Urn>(_conditionDefinition.GetUrns());
                if (_isValueUrn)
                    result = result.Append(_valueUrn);

                return result;
            }
        }
    }
}