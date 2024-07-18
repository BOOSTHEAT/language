using System;
using System.Collections.Generic;
using System.Linq;
using ImpliciX.Language.Core;

namespace ImpliciX.Language.Model;

public class TimeSeriesUrn : Urn
{
  public TimeSeriesUrn(Urn urn, IEnumerable<Urn> members, TimeSpan retention) : base(urn)
  {
    Retention = retention;
    Members = members.ToArray();
  }
  
  public Urn[] Members { get; }
  public TimeSpan Retention { get; }
}

public interface ITimeSeries
{
  public Over<TimeSeriesWithRetention> Over => new((multiplier, unit, span) => new TimeSeriesWithRetention(this,multiplier,unit,span));
}

public class MinimalistTimeSeries : ITimeSeries
{
  public MinimalistTimeSeries(Urn urn) => Urn = urn;
  public Urn Urn { get; }
}

public class TimeSeriesWithRetention : ITimeSeries
{
  public ITimeSeries Definition { get; }
  public int Multiplier { get; }
  public TimeUnit Unit { get; }
  public TimeSpan TimeSpan { get; }

  public TimeSeriesWithRetention(ITimeSeries definition, int multiplier, TimeUnit unit, Func<double,TimeSpan> timeSpan)
  {
    Definition = definition;
    Multiplier = multiplier;
    Unit = unit;
    TimeSpan = timeSpan(multiplier);
  }
}