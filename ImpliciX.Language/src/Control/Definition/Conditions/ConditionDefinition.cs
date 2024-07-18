using System;
using System.Collections.Generic;
using System.Linq;
using ImpliciX.Language.Model;

namespace ImpliciX.Language.Control
{
    public class ConditionDefinition
    {
        public List<(Op @operator, Urn[] operandsByUrn, object[] operandsByValue)> Parts { get; } = new List<(Op, Urn[], object[] )>();

        public void Add(Op op, Urn[] operandsByUrn)
        {
            Parts.Add((@op, operandsByUrn, new object[] { }));
        }

        public void Add(Op op, Urn[] operandsByUrn, object[] operandsByValue)
        {
            Parts.Add((@op, operandsByUrn, operandsByValue));
        }

        public ConditionDefinition And(ConditionDefinition operand)
        {
            Parts.Add(operand.Parts[0]);
            return this;
        }

        public Urn[] GetUrns()
        {
            var urns = new List<Urn>();

            foreach (var (_, _, operandsByValue) in Parts.Where(c => c.@operator == Op.Any))
            {
                urns.AddRange(((ConditionDefinition) operandsByValue[0]).GetUrns());
                urns.AddRange(((ConditionDefinition) operandsByValue[1]).GetUrns());
            }

            var partsWithoutAnyOperator = Parts.Where(c => c.@operator != Op.Any).Select(c => c.operandsByUrn).ToArray();
            if (partsWithoutAnyOperator.Any()) urns.AddRange(partsWithoutAnyOperator.Aggregate((urns1, urns2) => urns1.Concat(urns2).ToArray()));

            return urns.ToArray();
        }

        public bool ContainsAnyOperator()
        {
            return Parts.Any(c => c.@operator == Op.Any);
        }
    }

    public static class Condition
    {
        public static ConditionDefinition LowerThan<TOperand>(PropertyUrn<TOperand> operand1, PropertyUrn<TOperand> operand2)
        {
            var conditionDefinition = new ConditionDefinition();
            conditionDefinition.Add(Op.Lt, new Urn[] {operand1, operand2});
            return conditionDefinition;
        }

        public static ConditionDefinition GreaterThan<TOperand>(PropertyUrn<TOperand> operand1, PropertyUrn<TOperand> operand2)
        {
            var conditionDefinition = new ConditionDefinition();
            conditionDefinition.Add(Op.Gt, new Urn[] {operand1, operand2});
            return conditionDefinition;
        }

        public static ConditionDefinition LowerOrEqualTo<TOperand>(PropertyUrn<TOperand> operand1, PropertyUrn<TOperand> operand2)
        {
            var conditionDefinition = new ConditionDefinition();
            conditionDefinition.Add(Op.LtOrEqTo, new Urn[] {operand1, operand2});
            return conditionDefinition;
        }

        public static ConditionDefinition GreaterOrEqualTo<TOperand>(PropertyUrn<TOperand> operand1, PropertyUrn<TOperand> operand2)
        {
            var conditionDefinition = new ConditionDefinition();
            conditionDefinition.Add(Op.GtOrEqTo, new Urn[] {operand1, operand2});
            return conditionDefinition;
        }

        public static ConditionDefinition EqualWithEpsilon<TOperand>(PropertyUrn<TOperand> operand1, PropertyUrn<TOperand> operand2, PropertyUrn<TOperand> epsilon)
        {
            var conditionDefinition = new ConditionDefinition();
            conditionDefinition.Add(Op.EqWithEpsilon, new Urn[] {operand1, operand2, epsilon});
            return conditionDefinition;
        }

        public static ConditionDefinition EqualWithTolerance<TOperand>(PropertyUrn<TOperand> operand1, PropertyUrn<TOperand> operand2, PropertyUrn<Percentage> tolerance)
        {
            var conditionDefinition = new ConditionDefinition();
            conditionDefinition.Add(Op.EqWithTolerance, new Urn[] {operand1, operand2, tolerance});
            return conditionDefinition;
        }

        public static ConditionDefinition NotEqualWithTolerance<TOperand>(PropertyUrn<TOperand> operand1, PropertyUrn<TOperand> operand2, PropertyUrn<Percentage> tolerance)
        {
            var conditionDefinition = new ConditionDefinition();
            conditionDefinition.Add(Op.NotEqWithTolerance, new Urn[] {operand1, operand2, tolerance});
            return conditionDefinition;
        }

        public static ConditionDefinition LowerPlusEpsilon<TOperand>(PropertyUrn<TOperand> operand1, PropertyUrn<TOperand> operand2, PropertyUrn<TOperand> epsilon)
        {
            var conditionDefinition = new ConditionDefinition();
            conditionDefinition.Add(Op.LtPlusEpsilon, new Urn[] {operand1, operand2, epsilon});
            return conditionDefinition;
        }

        public static ConditionDefinition LowerMinusEpsilon<TOperand>(PropertyUrn<TOperand> operand1, PropertyUrn<TOperand> operand2, PropertyUrn<TOperand> epsilon)
        {
            var conditionDefinition = new ConditionDefinition();
            conditionDefinition.Add(Op.LtMinusEpsilon, new Urn[] {operand1, operand2, epsilon});
            return conditionDefinition;
        }

        public static ConditionDefinition GreaterPlusEpsilon<TOperand>(PropertyUrn<TOperand> operand1, PropertyUrn<TOperand> operand2, PropertyUrn<TOperand> epsilon)
        {
            var conditionDefinition = new ConditionDefinition();
            conditionDefinition.Add(Op.GtPlusEpsilon, new Urn[] {operand1, operand2, epsilon});
            return conditionDefinition;
        }

        public static ConditionDefinition GreaterMinusEpsilon<TOperand>(PropertyUrn<TOperand> operand1, PropertyUrn<TOperand> operand2, PropertyUrn<TOperand> epsilon)
        {
            var conditionDefinition = new ConditionDefinition();
            conditionDefinition.Add(Op.GtMinusEpsilon, new Urn[] {operand1, operand2, epsilon});
            return conditionDefinition;
        }

        public static ConditionDefinition LowerOrEqualPlusEpsilon<TOperand>(PropertyUrn<TOperand> operand1, PropertyUrn<TOperand> operand2, PropertyUrn<TOperand> epsilon)
        {
            var conditionDefinition = new ConditionDefinition();
            conditionDefinition.Add(Op.LtOrEqPlusEpsilon, new Urn[] {operand1, operand2, epsilon});
            return conditionDefinition;
        }

        public static ConditionDefinition LowerOrEqualMinusEpsilon<TOperand>(PropertyUrn<TOperand> operand1, PropertyUrn<TOperand> operand2, PropertyUrn<TOperand> epsilon)
        {
            var conditionDefinition = new ConditionDefinition();
            conditionDefinition.Add(Op.LtOrEqMinusEpsilon, new Urn[] {operand1, operand2, epsilon});
            return conditionDefinition;
        }

        public static ConditionDefinition GreaterOrEqualPlusEpsilon<TOperand>(PropertyUrn<TOperand> operand1, PropertyUrn<TOperand> operand2, PropertyUrn<TOperand> epsilon)
        {
            var conditionDefinition = new ConditionDefinition();
            conditionDefinition.Add(Op.GtOrEqPlusEpsilon, new Urn[] {operand1, operand2, epsilon});
            return conditionDefinition;
        }

        public static ConditionDefinition GreaterOrEqualMinusEpsilon<TOperand>(PropertyUrn<TOperand> operand1, PropertyUrn<TOperand> operand2, PropertyUrn<TOperand> epsilon)
        {
            var conditionDefinition = new ConditionDefinition();
            conditionDefinition.Add(Op.GtOrEqMinusEpsilon, new Urn[] {operand1, operand2, epsilon});
            return conditionDefinition;
        }

        public static ConditionDefinition Is<TOperand>(PropertyUrn<TOperand> operand1, TOperand operand2) where TOperand : Enum
        {
            var conditionDefinition = new ConditionDefinition();
            conditionDefinition.Add(Op.Is, new Urn[] {operand1}, new object[] {operand2});
            return conditionDefinition;
        }

        public static ConditionDefinition IsNot<TOperand>(PropertyUrn<TOperand> operand1, TOperand operand2) where TOperand : Enum
        {
            var conditionDefinition = new ConditionDefinition();
            conditionDefinition.Add(Op.IsNot, new Urn[] {operand1}, new object[] {operand2});
            return conditionDefinition;
        }

        public static ConditionDefinition InState<TState>(SubSystemNode operand1, TState operand2)
        {
            var conditionDefinition = new ConditionDefinition();
            conditionDefinition.Add(Op.InState, new Urn[] {operand1.Urn}, new object[] {operand2});
            return conditionDefinition;
        }

        public static ConditionDefinition Any(ConditionDefinition operand1, ConditionDefinition operand2)
        {
            var conditionDefinition = new ConditionDefinition();
            conditionDefinition.Add(Op.Any, new Urn[] { }, new object[] {operand1, operand2});
            return conditionDefinition;
        }
    }
}