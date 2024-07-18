using System;
using System.Collections.Generic;
using ImpliciX.Language.Model;

namespace ImpliciX.Language.Driver
{
  public interface IPropertySimulation
  {
    Func<TimeSpan, IEnumerable<IDataModelValue>> Discrete<T>(PropertyUrn<T> urn, T nominalValue, params (T, double)[] otherValues);
    Func<TimeSpan, IEnumerable<IDataModelValue>> Timed<T>(PropertyUrn<T> urn, Func<TimeSpan,double> f) where T:IFloat<T>;
    Func<TimeSpan, IEnumerable<IDataModelValue>> Stepper<T>(PropertyUrn<T> urn, double start, TimeSpan period, double delta) where T:IFloat<T>;
    Func<TimeSpan, IEnumerable<IDataModelValue>> Stepper<T>(PropertyUrn<T> urn, double start, TimeSpan period, Func<TimeSpan,double,double> delta) where T:IFloat<T>;
    Func<TimeSpan, IEnumerable<IDataModelValue>> Sinusoid<T>(PropertyUrn<T> urn, double min, double max) where T:IFloat<T>;
    Func<TimeSpan, IEnumerable<IDataModelValue>> Sinusoid<T>(MeasureNode<T> node, double min, double max, double failThreshold) where T:IFloat<T>;
  }
}