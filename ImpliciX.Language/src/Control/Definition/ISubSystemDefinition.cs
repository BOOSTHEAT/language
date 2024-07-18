
using System;
using ImpliciX.Language.Model;
namespace ImpliciX.Language.Control
{
  public interface ISubSystemDefinition
  {
    Urn ID {get;}
    PropertyUrn<SubsystemState> StateUrn { get; }
    Type StateType { get; }
    
  }
  
  public interface IFragmentDefinition
  {
  }
}