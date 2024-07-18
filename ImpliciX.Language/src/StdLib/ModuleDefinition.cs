using System;
using System.Collections.Generic;
using ImpliciX.Language.Model;

namespace ImpliciX.Language.StdLib;

public static class ModuleDefinition
{
  public static DataModelDefinition DataModel(Device device) => new DataModelDefinition
  {
    Assembly = device.GetType().Assembly,
    AppVersion = device.software.version.measure,
    AppEnvironment = device.environment,
  };
  
  public static SystemSoftwareModuleDefinition SystemSoftware(Device device, Func<string,bool> allowUpdate) => new SystemSoftwareModuleDefinition
  {
    IsPackageAllowedForUpdate = allowUpdate,
    RebootCommand = device._reboot,
    ReleaseVersion = device.software.version.measure,
    CommitUpdateCommand = device.software._commit_update,
    CleanVersionSettings = device._clean_version_settings,
    GeneralUpdateCommand = device.software._update,
    UpdateState = device.software.update_state,
    SoftwareMap = new Dictionary<string, SoftwareDeviceNode>
    {
      [device.app.Urn.Value] = device.app,
      [device.gui.Urn.Value] = device.gui,
      [device.bsp.Urn.Value] = device.bsp
    }
  };

  public static MmiHostModuleDefinition MmiHost(Device device) => new MmiHostModuleDefinition
  {
    CommitUpdate = device.software._commit_update,
    Restart = device._restart,
    Reboot = device._reboot,
    ApplicationsSoftwareDevices = new Dictionary<SoftwareDeviceNode, string>
    {
      [device.gui] = "gui",
      [device.app] = "app"
    },
    BspSoftwareDeviceNode = device.bsp
  };
}