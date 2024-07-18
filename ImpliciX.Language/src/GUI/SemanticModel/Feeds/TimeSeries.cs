using ImpliciX.Language.Model;

namespace ImpliciX.Language.GUI;

public class TimeSeriesFeed : Node
{
    protected TimeSeriesFeed(Urn urn) : base(urn)
    {
    }
    public static TimeSeriesFeed Subscribe(Urn urn) => new(urn);
}

