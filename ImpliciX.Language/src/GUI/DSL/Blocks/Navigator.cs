// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.GUI;

internal sealed class Navigator : Block
{
    private readonly GuiNode _target;
    private readonly Block _visual;
    private readonly Block _selectionMark;

    public Navigator(GuiNode target, Block visual, Block selectionMark)
    {
        _target = target;
        _visual = visual;
        _selectionMark = selectionMark;
    }

    public override Widget CreateWidget()
    {
        return new NavigatorWidget
        {
            Visual = _visual.CreateWidget(),
            TargetScreen = _target,
            OnTarget = _selectionMark.CreateWidget()
        };
    }
}