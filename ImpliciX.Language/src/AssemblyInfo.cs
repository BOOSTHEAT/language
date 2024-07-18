using System.Reflection;
using System.Runtime.CompilerServices;

[assembly:AssemblyCompanyAttribute("BOOSTHEAT")]
[assembly:AssemblyProductAttribute("ImpliciX.Language")]
[assembly:AssemblyDescription("DSL used to define any BOOSTHEAT device in terms of data model and control")]
[assembly:AssemblyTitleAttribute("ImpliciX Language "+ThisAssembly.Version)]
[assembly:AssemblyVersion(ThisAssembly.Version)]
[assembly:AssemblyInformationalVersionAttribute(ThisAssembly.InformationalVersion)]
[assembly:AssemblyFileVersionAttribute(ThisAssembly.Version)]
[assembly: InternalsVisibleTo("ImpliciX.Language.Tests")]

[System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "CheckNamespace")]
partial class ThisAssembly
{
  public const string Version = Git.SemVer.Major + "." + Git.SemVer.Minor + "." + Git.SemVer.Patch;
  public const string InformationalVersion = Version + "-" + Git.Branch + "+" + Git.Commit;
}
