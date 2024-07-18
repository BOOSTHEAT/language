using ImpliciX.Language.Model;
#pragma warning disable CS8981

namespace ImpliciX.Language.Tests.Metrics
{
    internal sealed class mmodel : RootModelNode
    {
        public static metrics metrics { get; set; }
        public static measures measures { get; set; }
        public static analyticsInstrumentation instrumentation { get; }
        public static analyticsProduction production { get; }

        static mmodel()
        {
            var modelNode = new mmodel();
            metrics = new metrics(nameof(metrics), modelNode);
            measures = new measures(nameof(measures), modelNode);
            instrumentation = new analyticsInstrumentation(nameof(instrumentation), modelNode);
            production = new analyticsProduction(nameof(production), modelNode);
        }

        private mmodel() : base(nameof(mmodel)) { }
    }

    internal sealed class analyticsProduction : ModelNode
    {
        public analyticsProductionMainCircuit main_circuit { get; }

        public analyticsProduction(string name, ModelNode parent) : base(name, parent)
            => main_circuit = new analyticsProductionMainCircuit(nameof(main_circuit), this);
    }

    internal sealed class analyticsProductionMainCircuit : ModelNode
    {
        public MetricUrn supply_temperature;

        public analyticsProductionMainCircuit(string name, ModelNode parent) : base(name, parent)
            => supply_temperature = MetricUrn.Build(Urn, nameof(supply_temperature));
    }

    internal sealed class production : RootModelNode
    {
        public static main_circuit main_circuit { get; }
        static production()
            => main_circuit = new main_circuit(new production());
        private production() : base(nameof(production)) { }
    }

    internal sealed class main_circuit : SubSystemNode
    {
        public supply_temperature supply_temperature { get; }
        public main_circuit(ModelNode parent) : base(nameof(main_circuit), parent)
            => supply_temperature = new supply_temperature(this);
    }

    internal sealed class supply_temperature : MeasureNode<Temperature>
    {
        public supply_temperature(ModelNode parent) : base(nameof(supply_temperature), parent) { }
    }

    internal sealed class metrics : ModelNode
    {
        public metrics(string name, ModelNode parent) : base(name, parent)
        {
            temperature_history = MetricUrn.Build(Urn, nameof(temperature_history));
            pressure_stats = MetricUrn.Build(Urn, nameof(pressure_stats));
            electricity_consumption = MetricUrn.Build(Urn, nameof(electricity_consumption));
            long_term_temperature_variations = MetricUrn.Build(Urn, nameof(long_term_temperature_variations));
            state_monitoring = MetricUrn.Build(Urn, nameof(state_monitoring));
            state_monitoring_with_detailed_data = MetricUrn.Build(Urn, nameof(state_monitoring_with_detailed_data));
            device_communications = new AnalyticsCommunicationCountersNode(nameof(device_communications), this);
            electrical_index = MetricUrn.Build(Urn, nameof(electrical_index));
        }

        public MetricUrn temperature_history { get; }
        public MetricUrn pressure_stats { get; }
        public MetricUrn electricity_consumption { get; }
        public MetricUrn long_term_temperature_variations { get; }
        public MetricUrn state_monitoring { get; }
        public MetricUrn state_monitoring_with_detailed_data { get; }
        public AnalyticsCommunicationCountersNode device_communications { get; }
        public MetricUrn electrical_index { get; }
    }

    internal sealed class measures : ModelNode
    {
        public measures(string name, ModelNode parent) : base(name, parent)
        {
            the_temperature = PropertyUrn<Temperature>.Build(Urn, nameof(the_temperature));
            the_pressure = PropertyUrn<Pressure>.Build(Urn, nameof(the_pressure));
            the_electrical_index = PropertyUrn<Energy>.Build(Urn, nameof(the_electrical_index));
            the_gas_index = PropertyUrn<Volume>.Build(Urn, nameof(the_gas_index));
            some_state = PropertyUrn<TheState>.Build(Urn, nameof(some_state));
            some_other_state = PropertyUrn<TheState>.Build(Urn, nameof(some_other_state));
            the_device = new DeviceNode(nameof(the_device), this);
        }

        public PropertyUrn<Temperature> the_temperature { get; }
        public PropertyUrn<Pressure> the_pressure { get; }
        public PropertyUrn<Energy> the_electrical_index { get; }
        public PropertyUrn<Volume> the_gas_index { get; }
        public PropertyUrn<TheState> some_state { get; }
        public PropertyUrn<TheState> some_other_state { get; }
        public DeviceNode the_device { get; }
    }

    internal sealed class instrumentation : RootModelNode
    {
        static instrumentation()
        {
            electrical_index = new MeasureNode<Energy>(nameof(electrical_index), new instrumentation());
        }
        public static MeasureNode<Energy> electrical_index { get; }
        private instrumentation() : base(nameof(instrumentation)) { }
    }

    internal sealed class analyticsInstrumentation : ModelNode
    {
        public analyticsInstrumentation(string name, ModelNode parent) : base(name, parent)
        {
            electrical_index = MetricUrn.Build(Urn, nameof(electrical_index));
        }

        public MetricUrn electrical_index { get; }
    }


    internal enum TheState
    {
        StateA,
        StateB,
        StateC,
    }
}