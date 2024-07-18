using ImpliciX.Language.Core;
using ImpliciX.Language.Model;

namespace ImpliciX.Language.Driver
{
  public interface IDriverStateKeeper
  {
    Result<IDriverState> TryRead(Urn urn);
    IDriverState Read(Urn urn);
    Result<Unit> TryUpdate(IDriverState state);
    IDriverState Update(IDriverState state);
    ILog Log { get; }
  }
}