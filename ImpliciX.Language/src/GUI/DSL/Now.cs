namespace ImpliciX.Language.GUI
{
  public class Now
  {
    public Block WeekDay { get; } = new DateTime { Feed = NowFeed.WeekDay };
    public Block Date { get; } = new DateTime { Feed = NowFeed.Date };
    public Block HoursMinutesSeconds { get; } = new DateTime { Feed = NowFeed.HoursMinutesSeconds };
  }
}