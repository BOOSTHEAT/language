using System;
using ImpliciX.Language.Model;
using NFluent;
using NUnit.Framework;
using static ImpliciX.Language.Metrics.Metrics;

namespace ImpliciX.Language.Tests.Metrics;

public class DslDeviceMonitoringTests
{
    [Test]
    public void GivenInvalidDeviceNode_WhenIDeviceMonitoringOf_ThenIGetAnException()
    {
        var exception = Check.ThatCode(() => Metric(mmodel.metrics.device_communications)
                .Is
                .Minutely
                .DeviceMonitoringOf(new DeviceNode("", null!))
                .ToSemantic())
            .Throws<InvalidOperationException>()
            .Value;

        Check.That(exception.Message).Contains("All data must be completed", "inputUrn=;");
    }
}