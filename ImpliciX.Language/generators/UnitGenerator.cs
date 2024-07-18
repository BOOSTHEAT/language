using System;
using System.Linq;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;

namespace ImpliciX.Language.Generators;

[Generator]
public class UnitGenerator : ISourceGenerator
{
  public void Execute(GeneratorExecutionContext context)
  {
    try
    {
      var units =
        from additionalFile in context.AdditionalFiles
        where additionalFile.Path.EndsWith(".ipcx", StringComparison.OrdinalIgnoreCase)
        let content = additionalFile.GetText()!.ToString()
        let element = XElement.Parse(content)
        from model in element.DescendantsAndSelf("model")
        let nameSpace = model.Attribute("name")!.Value
        from unit in model.Descendants("unit")
        let unitName = unit.Attribute("name")!.Value
        let source = $@"// Generated from: {additionalFile.Path}
using System;
using System.Globalization;
using ImpliciX.Language.Core;
using ImpliciX.Language.Model;

namespace {nameSpace};

[ValueObject]
public readonly struct {unitName} : IFloat<{unitName}>, IPublicValue
{{
  [ModelFactoryMethod]
  public static Result<{unitName}> FromString(string value)
  {{
    var isFloat = float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var pct);
    if (!isFloat) return new InvalidValueError(
      $""Value: {{value}} is invalid. For type {unitName}, it should be a float"");
    return FromFloat(pct);
  }}

  public static Result<{unitName}> FromFloat(float value) => new {unitName}(value);
  private {unitName}(float value) => Value = value;
  private float Value {{ get; }}
  public override string ToString() => Value.ToString(""F3"", CultureInfo.InvariantCulture);
  public object PublicValue() => Value;
  public float ToFloat() => Value;
  public override int GetHashCode() => Value.GetHashCode();
  
  public override bool Equals(object obj) =>
    obj is IEquatable<{unitName}> other && other.Equals(this);
}}
"
        select (FileName:$"Unit.{nameSpace}.{unitName}.g.cs",Source:source);

      foreach (var unit in units)
      {
        context.AddSource(unit.FileName, unit.Source);
      }
    }
    catch (Exception e)
    {
      context.ReportDiagnostic(Diagnostic.Create(new DiagnosticDescriptor(      
        nameof(UnitGenerator),
        "Generation Error",
        e.Message,
        "Unit",
        DiagnosticSeverity.Error,
        true), Location.None));
    }
  }

  public void Initialize(GeneratorInitializationContext context)
  {
  }
}