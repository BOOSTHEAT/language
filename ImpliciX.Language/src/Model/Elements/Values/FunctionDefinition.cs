using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ImpliciX.Language.Core;

namespace ImpliciX.Language.Model
{
  [ValueObject]
  public readonly struct FunctionDefinition: IPublicValue
  {
    public IDictionary<string, float> Params { get; }
    private readonly int _hashcode;

    public FunctionDefinition((string Name, float Value)[] parameters)
    {
      _hashcode = parameters.Select(p => HashCode.Combine(p.Name, p.Value)).Aggregate((a, b) => a ^ b);
      try
      {
        Params = parameters.ToDictionary(p => p.Name, p => p.Value);
      }
      catch (ArgumentException)
      {
        Params = new Dictionary<string, float>();
      }
    }

    public static Result<FunctionDefinition> From((string Name, float Value)[] @params)
    {
      return new FunctionDefinition(@params);
    }

    public static Result<FunctionDefinition> FromString(string value)
    {
      var parsedDef = value.Split("|")
        .Select(s => s.Split(":"))
        .Select(p => (p[0], p[1]))
        .ToArray();
      return FromString(parsedDef);
    }
    
    [ModelFactoryMethod]
    public static Result<FunctionDefinition> FromString((string Name, string Value)[] parameters)
    {
      var parsedResults = parameters.Select(p =>
      {
        var isParsed = float.TryParse(p.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out var value);
        return (p.Name, value, isParsed);
      }).ToArray();
      var coefficientsWithErrors = parsedResults
        .Where(pr => pr.isParsed == false)
        .Aggregate("", (acc, n) => $"{n},{acc}");

      if (coefficientsWithErrors != string.Empty)
      {
        return new InvalidValueError($"The following coefficients {coefficientsWithErrors} have incorrect values");
      }

      return From(parsedResults.Select(pr => (pr.Name, pr.value)).ToArray());
    }

    public bool ParamExist(string paramStr) => Params.ContainsKey(paramStr);

    public float GetValueParam(string paramStr)
    {
      if (Params.TryGetValue(paramStr, out float value))
        return value;
      throw new ArgumentException($"Parameter {paramStr} is not defined.");
    }

    public override bool Equals(object obj)
    {
      if (obj is FunctionDefinition _ == false)
        return false;
      var definition = (FunctionDefinition)obj;
      if (GetHashCode() != definition.GetHashCode())
        return false;
      var myParams = Params.OrderBy(p => p.Key);
      var theirParams = definition.Params.OrderBy(p => p.Key);
      return myParams.Zip(theirParams, (first, second) => (first, second))
        .All(c => c.first.Equals(c.second));
    }

    public override int GetHashCode() => _hashcode;

    public override string ToString() => string.Join("|", Params.Select(p => p.Key + ":" + p.Value.ToString(CultureInfo.InvariantCulture)));
    public object PublicValue()
    {
      return ToString();
    }
  }
}