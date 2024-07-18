using ImpliciX.Language.Model;

namespace ImpliciX.Language.Tests.Records;

internal sealed class rmodel : RootModelNode
{
  public static RecordsNode<TheAlarm> the_alarms { get; }
  public static RecordWriterNode<TheAlarm> alarm { get; }
  public static RecordWriterNode<TheAlarm> other_alarm { get; }

  static rmodel()
  {
    var modelNode = new rmodel();
    the_alarms = new RecordsNode<TheAlarm>(nameof(the_alarms), modelNode);
    alarm = new RecordWriterNode<TheAlarm>(nameof(alarm), modelNode, (n,p) => new TheAlarm(n, p));
    other_alarm = new RecordWriterNode<TheAlarm>(nameof(other_alarm), modelNode, (n,p) => new TheAlarm(n, p));
  }

  private rmodel() : base(nameof(rmodel)) { }
}

enum TheAlarmKind
{
  Error1,
  Error2,
  Error3
}

internal sealed class TheAlarm : ModelNode
{
  public PropertyUrn<TheAlarmKind> kind { get; }
  public PropertyUrn<Temperature> some_value { get; }

  public TheAlarm(string name, ModelNode parent) : base(name, parent)
  {
    kind = PropertyUrn<TheAlarmKind>.Build(Urn, nameof(kind));
    some_value = PropertyUrn<Temperature>.Build(Urn, nameof(some_value));
  }
}
