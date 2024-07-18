namespace ImpliciX.Language.GUI;

public class MultiChart : FixedSizeBlock<MultiChart>
{
  private readonly ILeftYAxisChart _left;
  private readonly IRightYAxisChart _right;

  public MultiChart(ILeftYAxisChart left, IRightYAxisChart right)
  {
    _left = left;
    _right = right;
  }

  public override Widget CreateWidget()
  {
    return new MultiChartWidget
    {
      Left = _left.CreateXTimeWidget(),
      Right = _right.CreateXTimeWidget(),
      Width = WidthValue,
      Height = HeightValue,
    };
  }
}