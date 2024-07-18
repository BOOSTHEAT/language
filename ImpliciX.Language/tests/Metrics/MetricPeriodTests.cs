using System;
using ImpliciX.Language.Metrics;
using NFluent;
using NUnit.Framework;
using MetricsDSL = ImpliciX.Language.Metrics.Metrics;

namespace ImpliciX.Language.Tests.Metrics;

public class MetricPeriodTests : MetricsDSL
{
  [TestCase(0)]
  [TestCase(-1)]
  public void GivenIChooseNotAllowedEveryXHourPeriod_ThenIGetAnError(int everyParameter)
  {
    var ex = Check.ThatCode(() => new MetricPeriod<object>(null!, (_, t, _, _) => t).Every(everyParameter).Hours)
      .Throws<InvalidOperationException>()
      .Value;

    Check.That(ex.Message).Contains("must be greater than 0");
  }
}