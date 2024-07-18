using ImpliciX.Language.Core;
using ImpliciX.Language.Model;

namespace ImpliciX.Language.Driver
{
  public interface IDriverState
  {
    Urn Id { get; }
    Result<T> GetValue<T>(string key);
    Result<T> GetValueOrDefault<T>(string key, T defaultValue);
    IDriverState New(Urn id);
    IDriverState WithValue(string key, object value);
    bool Contains(string key);
    bool IsEmpty { get; }
  }
}