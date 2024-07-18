
// ReSharper disable once CheckNamespace
namespace System.Runtime.CompilerServices;

// The following declarations allow to use C# language version 9 (and following) keywords
// in a .NET standard 2.1 project 

// init
internal static class IsExternalInit {}

// required
public class RequiredMemberAttribute : Attribute { }
public class CompilerFeatureRequiredAttribute : Attribute
{
  public CompilerFeatureRequiredAttribute(string name) { }
}