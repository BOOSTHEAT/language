namespace ImpliciX.Language.Model
{
    public class SensorError<T> : SubSystemNode
    {
        public SensorError(ModelNode parent) : base("sensor_error", parent)
        {
            public_state = PropertyUrn<Alert>.Build(Urn, nameof(public_state));
            settings = new AlertSettings<T>(this);
        }
        public PropertyUrn<Alert> public_state { get; } 
        public AlertSettings<T> settings { get; } 
    }
}