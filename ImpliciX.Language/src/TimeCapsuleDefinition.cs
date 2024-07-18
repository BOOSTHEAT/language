using System;
using ImpliciX.Language.Metrics;

namespace ImpliciX.Language;

public class TimeCapsuleDefinition
{
  public required IMetricDefinition[] Metrics { get; init; }
  public required Func<GUI.GUI> UserInterface { get; init; }
}