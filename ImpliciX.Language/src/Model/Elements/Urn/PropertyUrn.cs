using System.Linq;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.Model;

[UrnObject]
public class PropertyUrn<TTarget> : Urn
{
  protected PropertyUrn(string value) : base(value)
  {
  }

  [ModelFactoryMethod]
  public static PropertyUrn<TTarget> Build(params string[] components) =>
    new (Format(components));
}

public interface ISettingUrn
{
}

public abstract class SettingUrn<TTarget> : PropertyUrn<TTarget>, ISettingUrn
{
  protected SettingUrn(string value) : base(value)
  {
  }
}

[UrnObject]
public class VersionSettingUrn<TTarget> : SettingUrn<TTarget>
{
  private VersionSettingUrn(string value) : base(value)
  {
  }

  [ModelFactoryMethod]
  public new static VersionSettingUrn<TTarget> Build(params string[] components) =>
    new (Format(components));
}

[UrnObject]
public class UserSettingUrn<TTarget> : SettingUrn<TTarget>
{
  private UserSettingUrn(string value) : base(value)
  {
  }

  [ModelFactoryMethod]
  public new static UserSettingUrn<TTarget> Build(params string[] components) =>
    new (Format(components));
}

[UrnObject]
public class FactorySettingUrn<TTarget> : SettingUrn<TTarget>
{
  private FactorySettingUrn(string value) : base(value)
  {
  }

  [ModelFactoryMethod]
  public new static FactorySettingUrn<TTarget> Build(params string[] components) =>
    new (Format(components));
}

[UrnObject]
public class PersistentCounterUrn<TTarget> : PropertyUrn<TTarget>
{
  private PersistentCounterUrn(string value) : base(value)
  {
  }

  [ModelFactoryMethod]
  public new static PersistentCounterUrn<TTarget> Build(params string[] components) =>
    new (Format(components));
}

[UrnObject]
public class MetricUrn : PropertyUrn<MetricValue>
{
  public static readonly string OCCURRENCE = "occurrence";
  public static readonly string DURATION = "duration";
  public static readonly string SAMPLES_COUNT = "samples_count";
  public static readonly string ACCUMULATED_VALUE = "accumulated_value";

  public MetricUrn(string value) : base(value)
  {
  }

  [ModelFactoryMethod]
  public new static MetricUrn Build(params string[] components) => new MetricUrn(Format(components));

  //TODO : A mettre coté Platform :
  public static MetricUrn BuildOccurence(params string[] components) =>
    new MetricUrn(Format(components.Append(OCCURRENCE).ToArray()));

  public static MetricUrn BuildDuration(params string[] components) =>
    new MetricUrn(Format(components.Append(DURATION).ToArray()));

  public static MetricUrn BuildSamplesCount(params string[] components) =>
    new MetricUrn(Format(components.Append(SAMPLES_COUNT).ToArray()));

  public static MetricUrn BuildAccumulatedValue(params string[] components) =>
    new MetricUrn(Format(components.Append(ACCUMULATED_VALUE).ToArray()));
}