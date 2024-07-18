using System;
using System.Collections.Generic;
using ImpliciX.Language.Core;
using ImpliciX.Language.Model;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.Records;

public interface IRecord
{
  Urn Urn { get; }
  Type Type { get; }
  Option<int> Retention { get; }
  IReadOnlyList<(Urn FormUrn, CommandUrn<NoArg> CommandUrn)> Writers { get; }
}