using ImpliciX.Language.Model;

namespace ImpliciX.Language.GUI;

public class GuiNode : ModelNode
{
    public GuiNode(ModelNode parent, string urnToken) : base(urnToken, parent)
    {
    }
}