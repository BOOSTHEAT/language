using System;
using ImpliciX.Language.Core;
using ImpliciX.Language.Driver;
using ImpliciX.Language.Modbus;
using ImpliciX.Language.Model;
using NFluent;
using NUnit.Framework;

namespace ImpliciX.Language.Tests.Modbus
{
    [TestFixture]
    public class MeasureTests
    {
        [Test]
        public void when_has_status_urn_and_created_with_success_value()
        {
            var measureUrn = PropertyUrn<int>.Build("a", "b");
            var measureStatusUrn = PropertyUrn<MeasureStatus>.Build("a", "b","status");
            var currentTime = TimeSpan.Zero;
            var measure = Measure<int>.Create(measureUrn, measureStatusUrn, Result<int>.Create(1), currentTime);

            var measureValues = measure.ModelValues();
            var expectedValues = new IDataModelValue[] {Property<int>.Create(measureUrn, 1, currentTime), Property<MeasureStatus>.Create(measureStatusUrn, MeasureStatus.Success, currentTime)};

            Check.That(measureValues).ContainsExactly(expectedValues);
        }
        
        [Test]
        public void when_has_status_urn_and_created_with_error_value()
        {
            var measureUrn = PropertyUrn<int>.Build("a", "b");
            var measureStatusUrn = PropertyUrn<MeasureStatus>.Build("a", "b","status");
            var currentTime = TimeSpan.Zero;
            var measure = Measure<int>.Create(measureUrn, measureStatusUrn, Result<int>.Create(new DecodeError("error")), currentTime);

            var measureValues = measure.ModelValues();
            var expectedValues = new IDataModelValue[] {Property<MeasureStatus>.Create(measureStatusUrn, MeasureStatus.Failure, currentTime)};

            Check.That(measureValues).ContainsExactly(expectedValues);
        }
    }
}