namespace ImpliciX.Language.Model;

public interface ITextValue
{
    string ToString();
    int MaxLength { get; }
}