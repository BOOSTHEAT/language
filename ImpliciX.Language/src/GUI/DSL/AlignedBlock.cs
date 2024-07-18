namespace ImpliciX.Language.GUI
{
  public class AlignedBlock
  {
    internal record Parameters
    {
      public int? _top = null;
      public int? _bottom = null;
      public int? _centerV = null;
      public int? _left = null;
      public int? _right = null;
      public int? _centerH = null;
    }
    
    public class Empty
    {
      public Fully Origin => new (new Parameters { _top = 0, _left = 0 });
      public Vertically Top(int pixels) => new (new Parameters { _top = pixels });
      public Vertically Bottom(int pixels) => new (new Parameters { _bottom = pixels });
      public Vertically VerticalCenterOffset(int pixels) => new (new Parameters { _centerV = pixels });
      public Horizontally Left(int pixels) => new (new Parameters { _left = pixels });
      public Horizontally Right(int pixels) => new (new Parameters { _right = pixels });
      public Horizontally HorizontalCenterOffset(int pixels) => new (new Parameters { _centerH = pixels });
    }

    public class Vertically
    {
      internal Vertically(Parameters parameters) => _parameters = parameters;
      private readonly Parameters _parameters;
      public Fully Left(int pixels) => new (_parameters with { _left = pixels });
      public Fully Right(int pixels) => new (_parameters with { _right = pixels });
      public Fully HorizontalCenterOffset(int pixels) => new (_parameters with { _centerH = pixels });
    }

    public class Horizontally
    {
      internal Horizontally(Parameters parameters) => _parameters = parameters;
      private readonly Parameters _parameters;
      public Fully Top(int pixels) => new Fully(_parameters with { _top = pixels });
      public Fully Bottom(int pixels) => new Fully(_parameters with { _bottom = pixels });
      public Fully VerticalCenterOffset(int pixels) => new Fully(_parameters with { _centerV = pixels });
    }
    
    public class Fully
    {
      internal Fully(Parameters parameters) => _parameters = parameters;
      private readonly Parameters _parameters;

      public AlignedBlock Put(Block block) => new AlignedBlock(_parameters, block);
    }

    private readonly Block _block;
    private readonly Parameters _parameters;

    private AlignedBlock(Parameters parameters, Block block)
    {
      _parameters = parameters;
      _block = block;
    }

    public Widget CreateWidget()
    {
      var widget = _block.CreateWidget();
      widget.Left = _parameters._left;
      widget.Right = _parameters._right;
      widget.Top = _parameters._top;
      widget.Bottom = _parameters._bottom;
      widget.HorizontalCenterOffset = _parameters._centerH;
      widget.VerticalCenterOffset = _parameters._centerV;
      return widget;
    }
  }
}