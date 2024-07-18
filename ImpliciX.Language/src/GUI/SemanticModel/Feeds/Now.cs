namespace ImpliciX.Language.GUI
{
  public class NowFeed : Feed
  {
    public string Format { get; }

    public NowFeed(string format)
    {
      Format = format;
    }

    public static readonly NowFeed WeekDay = new NowFeed("dddd");
    public static readonly NowFeed Date = new NowFeed("L");
    public static readonly NowFeed HoursMinutes = new NowFeed("LT");
    public static readonly NowFeed HoursMinutesSeconds = new NowFeed("LTS");
  }
}