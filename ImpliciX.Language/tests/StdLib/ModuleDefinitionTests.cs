using ImpliciX.Language.StdLib;
using NFluent;
using NUnit.Framework;

namespace ImpliciX.Language.Tests.StdLib;

public class ModuleDefinitionTests
{
  [Test]
  public void CanCreate()
  {
    Check.That(ModuleDefinition.DataModel(my_device._)).IsNotNull();
    Check.That(ModuleDefinition.SystemSoftware(my_device._,_ => false)).IsNotNull();
    Check.That(ModuleDefinition.MmiHost(my_device._)).IsNotNull();
  }
}