using System;
using System.Collections.Generic;
using ImpliciX.Language.Core;
using ImpliciX.Language.Model;
using TimeZone = ImpliciX.Language.Model.TimeZone;

namespace ImpliciX.Language
{
    public class HarmonyModuleDefinition
    {
        public HarmonyModuleDefinition()
        {
            LiveData = new LiveDataModel();
            AdditionalId = new Dictionary<string, PropertyUrn<Literal>>();
        }
        public PropertyUrn<Literal> DeviceId { get; set; }
        public PropertyUrn<Literal> IDScope { get; set; }
        public PropertyUrn<Literal> SymmetricKey { get; set; }
        public PropertyUrn<Literal> DeviceSerialNumber { get; set; }
        public Dictionary<string,PropertyUrn<Literal>> AdditionalId { get; set; }
        public PropertyUrn<SoftwareVersion> ReleaseVersion { get; set; }
        public PropertyUrn<TimeZone> UserTimeZone { get; set; }
        public Func<PropertyUrn<AlarmState>, Option<string>> AlarmCodeFromAlarmStateUrn { get; set; }
        public PropertyUrn<Duration> RetryDelay { get; set; }
        public PropertyUrn<Duration> EnableDelay { get; set; }
        public LiveDataModel LiveData { get; }
        
        public class LiveDataModel
        {
            public PropertyUrn<Presence> Presence { get; set; }
            public TimeSpan Period { get; set; }
            public IEnumerable<Urn> Content { get; set; }    
        }
    }
}