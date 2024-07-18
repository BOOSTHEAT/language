namespace ImpliciX.Language.GUI;

public interface IYAxisChart
{
  ChartXTimeYWidget CreateXTimeWidget();
}

public interface ILeftYAxisChart : IYAxisChart
{
}

public interface IRightYAxisChart : IYAxisChart
{
}
