using NFluent;
using NUnit.Framework;
using static ImpliciX.Language.Records.Records;

namespace ImpliciX.Language.Tests.Records;

public class DslSimpleRecordTests
{
  [Test]
  public void DeclareWithSingleForm()
  {
    var record = Record(rmodel.the_alarms).Is.Snapshot.Of(rmodel.alarm).Instance;
    Check.That(record.Retention.IsNone).IsTrue();
    Check.That(record.Urn).IsEqualTo(rmodel.the_alarms.Urn);
    Check.That(record.Type).IsEqualTo(typeof(TheAlarm));
    Check.That(record.Writers.Count).IsEqualTo(1);
    Check.That(record.Writers[0]).IsEqualTo((rmodel.alarm.form.Urn, rmodel.alarm.write));
  }

  [Test]
  public void DeclareWithMultipleForms()
  {
    var record = Record(rmodel.the_alarms).Is.Snapshot.Of(rmodel.alarm, rmodel.other_alarm).Instance;
    Check.That(record.Retention.IsNone).IsTrue();
    Check.That(record.Urn).IsEqualTo(rmodel.the_alarms.Urn);
    Check.That(record.Type).IsEqualTo(typeof(TheAlarm));
    Check.That(record.Writers.Count).IsEqualTo(2);
    Check.That(record.Writers[0]).IsEqualTo((rmodel.alarm.form.Urn, rmodel.alarm.write));
    Check.That(record.Writers[1]).IsEqualTo((rmodel.other_alarm.form.Urn, rmodel.other_alarm.write));
  }

  [Test]
  public void DeclareStorageRetentionRecord()
  {
    var record = Record(rmodel.the_alarms).Is.Last(3).Snapshot.Of(rmodel.alarm, rmodel.other_alarm).Instance;
    Check.That(record.Retention.IsSome).IsTrue();
    Check.That(record.Retention.GetValue()).IsEqualTo(3);

    Check.That(record.Urn).IsEqualTo(rmodel.the_alarms.Urn);
    Check.That(record.Type).IsEqualTo(typeof(TheAlarm));
    Check.That(record.Writers.Count).IsEqualTo(2);
    Check.That(record.Writers[0]).IsEqualTo((rmodel.alarm.form.Urn, rmodel.alarm.write));
    Check.That(record.Writers[1]).IsEqualTo((rmodel.other_alarm.form.Urn, rmodel.other_alarm.write));
  }
}