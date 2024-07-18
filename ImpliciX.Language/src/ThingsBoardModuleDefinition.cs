using System;
using System.Collections.Generic;
using ImpliciX.Language.Model;

namespace ImpliciX.Language
{
  public class ThingsBoardModuleDefinition
  {
    public ThingsBoardModuleDefinition()
    {
      Connection = new ConnectionModel();
      Telemetry = Array.Empty<(Urn Value, TimeSpan Period)>();
    }

    public ConnectionModel Connection { get; }
    public IEnumerable<(Urn, TimeSpan)> Telemetry { get; set; }
    public PropertyUrn<Duration> RetryDelay { get; set; }
    public PropertyUrn<Duration> EnableDelay { get; set; }

    public class ConnectionModel
    {
      public PropertyUrn<Literal> Host { get; set; }
      public PropertyUrn<Literal> AccessToken { get; set; }
    }
  }
}