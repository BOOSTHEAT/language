using System.Linq;
using System.Reflection;
using System.Threading;
using ImpliciX.Language.Generators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using Moq;
using NUnit.Framework;

namespace ImpliciX.Language.Tests.Generators;

public class UnitGeneratorTests
{
  [Test]
  public void UnitGeneratorTest()
  {
    var driver =
      CSharpGeneratorDriver
        .Create(new UnitGenerator())
        .AddAdditionalTexts([
          CreateAdditionalText("whatever.ipcx", @"<?xml version=""1.0""?>
<model name=""Yolo"">
 <unit name=""Foo"" />
 <unit name=""Bar"" />
</model>
")
        ])
        .RunGeneratorsAndUpdateCompilation(
          CreateCompilation(),
          out var outputCompilation,
          out var diagnostics
        );

    Assert.True(diagnostics.IsEmpty);
    Assert.That(
      outputCompilation
        .Assembly.GetTypeByMetadataName("Yolo.Foo")!.Interfaces
        .Select(x => x.ToString()),
      Is.EqualTo(new []
      {
        "IFloat<Yolo.Foo>",
        "IPublicValue"
      })
    );
    Assert.That(
      outputCompilation
        .Assembly.GetTypeByMetadataName("Yolo.Bar")!.Interfaces
        .Select(x => x.ToString()),
      Is.EqualTo(new []
      {
        "IFloat<Yolo.Bar>",
        "IPublicValue"
      })
    );
  }

  private AdditionalText CreateAdditionalText(string path, string content)
  {
    var at = new Mock<AdditionalText>();
    at.Setup(x => x.Path).Returns(path);
    at.Setup(x => x.GetText(It.IsAny<CancellationToken>()))
      .Returns(SourceText.From(content));
    return at.Object;
  }

  private static Compilation CreateCompilation()
    => CSharpCompilation.Create("compilation",
      new CSharpSyntaxTree[] { },
      new[] { MetadataReference.CreateFromFile(typeof(Binder).GetTypeInfo().Assembly.Location) },
      new CSharpCompilationOptions(OutputKind.ConsoleApplication));

  private static Compilation CreateCompilation(string source)
    => CSharpCompilation.Create("compilation",
      new[] { CSharpSyntaxTree.ParseText(source) },
      new[] { MetadataReference.CreateFromFile(typeof(Binder).GetTypeInfo().Assembly.Location) },
      new CSharpCompilationOptions(OutputKind.ConsoleApplication));
}