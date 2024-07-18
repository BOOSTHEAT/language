using System;
using ImpliciX.Language.Model;

namespace ImpliciX.Language.Core;

public class Over<TNext>
{
  private readonly Func<int, TimeUnit, Func<double,TimeSpan>, TNext> _nexter;

  public Over(Func<int,TimeUnit, Func<double,TimeSpan>,TNext> nexter)
  {
    _nexter = nexter;
  }
  
  public Unit ThePast(int multiplier) =>
    new Unit((u,f) => _nexter(multiplier,u,f));

  public class Unit
  {
    private readonly Func<TimeUnit, Func<double,TimeSpan>, TNext> _nexter;

    public Unit(Func<TimeUnit, Func<double,TimeSpan>, TNext> nexter)
    {
      _nexter = nexter;
    }

    public TNext Milliseconds => _nexter(TimeUnit.Milliseconds, TimeSpan.FromMilliseconds);
    public TNext Seconds => _nexter(TimeUnit.Seconds, TimeSpan.FromSeconds);
    public TNext Minutes => _nexter(TimeUnit.Minutes, TimeSpan.FromMinutes);
    public TNext Hours => _nexter(TimeUnit.Hours, TimeSpan.FromHours);
    public TNext Days => _nexter(TimeUnit.Days, TimeSpan.FromDays);
    public TNext Weeks => _nexter(TimeUnit.Weeks, n => TimeSpan.FromDays(7*n));
    public TNext Months => _nexter(TimeUnit.Months, n => TimeSpan.FromDays(31*n));
    public TNext Quarters => _nexter(TimeUnit.Quarters, n => TimeSpan.FromDays(92*n));
    public TNext Years => _nexter(TimeUnit.Years, n => TimeSpan.FromDays(366*n));
  }
}