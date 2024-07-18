using ImpliciX.Language.Model;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.GUI
{
  public class Screens
  {
    protected static GUI GUI => new ();

    protected static AlignedBlock[] Screen(params AlignedBlock[] content) => content;
    protected static Font Font => new ();

    protected static Block Label(string text) => new Label {Text = text};
    protected static Block Translate(string text) => new Translation {Text = text};
    protected static Block Show<T>(MeasureNode<T> measure) => new Measure<T> {Node = measure};
    protected static Block Show<T>(PropertyUrn<T> property) => new Property<T> {Urn = property};

    protected static AlignedBlock.Empty At => new ();
    protected static Switch.Partial Switch => new ();
    protected static Canvas.Empty Canvas => new ();

    protected static Canvas.Empty Background(Block background) => new () {Background = background};
    protected static Box Box => new ();
    protected static OnOffButton<T> OnOff<T>(PropertyUrn<T> urn) => new (urn);
    protected static DropDownList<T> DropDownList<T>(PropertyUrn<T> urn) => new (urn);
    protected static Input Input(PropertyUrn<Literal> urn) => new (urn);
    
    protected static readonly ChartsFacade Chart = new ();
    protected static DecoratedUrn Of(Urn urn) => new (urn);
    protected static DecoratedUrn Of<T>(PropertyUrn<T> urn) => new DecoratedPropertyUrn<T>(urn);
    protected static DecoratedUrn Of<T>(MeasureNode<T> measure) => new DecoratedMeasureUrn<T>(measure);

    protected static Image Image(string imagePath) => new () {Path = imagePath};
    protected static Stack.Empty Row { get; } = new () {ArrangeAs = Composite.ArrangeAs.Row};
    protected static Stack.Empty Column { get; } = new () {ArrangeAs = Composite.ArrangeAs.Column};

    protected static Value Value<T>(MeasureNode<T> measure) => new MeasureValue<T>(measure);
    protected static Value Value<T>(PropertyUrn<T> property) => new PropertyValue<T>(property);

    public static Now Now { get; } = new ();
  }
}