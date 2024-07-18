using ImpliciX.Language.Model;
namespace ImpliciX.Language
{
  public class ApplicationDefinition
  {
    public string AppName { get; set; }
    public string AppSettingsFile { get; set; }
    public DataModelDefinition DataModelDefinition { get; set; }
    public object[] ModuleDefinitions { get; set; }
  }
}