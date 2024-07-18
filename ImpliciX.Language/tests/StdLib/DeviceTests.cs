using System;
using System.Collections.Generic;
using ImpliciX.Data.Factory;
using ImpliciX.Language.Model;
using NFluent;
using NUnit.Framework;
using TimeZone = ImpliciX.Language.Model.TimeZone;

namespace ImpliciX.Language.Tests.StdLib;

public class DeviceTests
{
  [TestCaseSource(nameof(UrnCases))]
  public void CheckUrns(Urn urn, string expectedUrn, Type expectedType)
  {
    Check.That(urn.Value).IsEqualTo(expectedUrn);
    Check.That(urn.GetType()).IsEqualTo(expectedType);
  }
  
  [TestCaseSource(nameof(UrnCases))]
  public void FindUrnInModelFactory(Urn _, string urn, Type expectedType)
  {
    var factory = new ModelFactory(typeof(DeviceTests).Assembly);
    Check.That(factory.UrnExists(urn)).IsTrue();
    var r = factory.FindUrnType(urn);
    Check.That(r.IsSuccess).IsTrue();
    Check.That(r.Value).IsEqualTo(expectedType);
  }

  public static object[][] UrnCases = new []
  {
    new object[] { my_device._._reboot, "my_device:REBOOT", typeof(CommandUrn<NoArg>) },
    new object[] { my_device._._restart, "my_device:RESTART", typeof(CommandUrn<NoArg>) },
    new object[] { my_device._.environment, "my_device:environment", typeof(PropertyUrn<Literal>) },
    new object[] { my_device._.serial_number, "my_device:serial_number", typeof(FactorySettingUrn<Literal>) },
    new object[] { my_device._.locale, "my_device:locale", typeof(UserSettingUrn<Locale>) },
    new object[] { my_device._.timezone, "my_device:timezone", typeof(UserSettingUrn<TimeZone>) },
    new object[] { my_device._.software.update_state, "my_device:software:update_state", typeof(PropertyUrn<UpdateState>) },
  };

  [TestCaseSource(nameof(NodeCases))]
  public void CheckNodes(ModelNode node, string expectedUrn, Type expectedType)
  {
    Check.That(node.Urn.Value).IsEqualTo(expectedUrn);
    Check.That(node.GetType()).IsEqualTo(expectedType);
  }
  
  [TestCaseSource(nameof(NodeCases))]
  public void FindNodeInModelFactory(ModelNode _, string urn, Type expectedType)
  {
    var factory = new ModelFactory(typeof(DeviceTests).Assembly);
    Check.That(factory.UrnExists(urn)).IsTrue();
    var r = factory.FindUrnType(urn);
    Check.That(r.IsSuccess).IsTrue();
    Check.That(r.Value).IsEqualTo(NodeTypeToUrnType(expectedType));
  }

  private static Type NodeTypeToUrnType(Type nodeType)
  {
    var conversions = new Dictionary<Type,Type>()
    {
      [typeof(CommandNode<>)] = typeof(CommandUrn<>),
    };
    return nodeType.IsGenericType
           && conversions.TryGetValue(nodeType.GetGenericTypeDefinition(), out var urnType)
      ? urnType.MakeGenericType(nodeType.GetGenericArguments())
      : nodeType;
  }

  public static object[][] NodeCases = new []
  {
    new object[] { my_device._.app, "my_device:app", typeof(SoftwareDeviceNode) },
    new object[] { my_device._.gui, "my_device:gui", typeof(SoftwareDeviceNode) },
    new object[] { my_device._.bsp, "my_device:bsp", typeof(SoftwareDeviceNode) },
    new object[] { my_device._._clean_version_settings, "my_device:CLEAN_VERSION_SETTINGS", typeof(CommandNode<NoArg>) },
    new object[] { my_device._.software._update, "my_device:software:UPDATE", typeof(CommandNode<PackageLocation>) },
    new object[] { my_device._.software._commit_update, "my_device:software:COMMIT_UPDATE", typeof(CommandNode<NoArg>) },
    new object[] { my_device._.software._rollback_update, "my_device:software:ROLLBACK_UPDATE", typeof(CommandNode<NoArg>) },
    new object[] { my_device._.software.version, "my_device:software:version", typeof(MeasureNode<SoftwareVersion>) },
  };
  
}