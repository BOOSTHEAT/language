using ImpliciX.Language.StdLib;

namespace ImpliciX.Language.Tests.StdLib;

public class my_device : Device
{
  public static my_device _ = new ();

  private my_device() : base(nameof(my_device))
  {
  }
}
