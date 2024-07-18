using System;
using System.Collections.Generic;
using ImpliciX.Language.Core;
using ImpliciX.Language.Model;

namespace ImpliciX.Language.Modbus
{
    public interface IMeasure
    {
        IEnumerable<IDataModelValue> ModelValues();
        void SetData(Result<object> value, TimeSpan at);
    }

    public class LateMeasure<T> : IMeasure
    {
        
        private PropertyUrn<T> MeasureUrn { get; }
        private PropertyUrn<MeasureStatus> StatusUrn { get; }
        public LateMeasure(PropertyUrn<T> measureUrn, PropertyUrn<MeasureStatus> statusUrn)
        {
            MeasureUrn = measureUrn;
            StatusUrn = statusUrn;
        }

        public void SetData(Result<object> value, TimeSpan at)
        {
            At = at;
            MeasureData = value.Match(whenError: err => Result<T>.Create(err), whenSuccess: v => Result<T>.Create((T) v));
        }

        public TimeSpan At { get; set; }

        public Result<T> MeasureData { get; set; }

        public IEnumerable<IDataModelValue> ModelValues()
        {
            if (MeasureData.IsSuccess)
            {
                yield return Property<T>.Create(MeasureUrn, MeasureData.Value, At);
            }

            if (StatusUrn != null && MeasureData.IsSuccess)
            {
                yield return Property<MeasureStatus>.Create(StatusUrn, MeasureStatus.Success, At);
            }

            if (StatusUrn != null && MeasureData.IsError)
            {
                yield return Property<MeasureStatus>.Create(StatusUrn, MeasureStatus.Failure, At);
            }
        }
    }
    
    public readonly struct Measure<T> : IMeasure
    {
        public static Measure<T> Create(Urn measureUrn, Urn statusUrn, Result<T> measureData, TimeSpan currentTime)
            => new Measure<T>(measureUrn, statusUrn, measureData, currentTime);
        private PropertyUrn<T> MeasureUrn { get; }
        private PropertyUrn<MeasureStatus> StatusUrn { get; }
        private Result<T> MeasureData { get; }
        private TimeSpan At { get; }

        private Measure(Urn measureUrn,Urn statusUrn, Result<T> measureData, TimeSpan at)
        {
            MeasureUrn = (PropertyUrn<T>) measureUrn;
            StatusUrn = (PropertyUrn<MeasureStatus>) statusUrn;
            MeasureData = measureData;
            At = at;
        }
        

        public IEnumerable<IDataModelValue> ModelValues()
        {
            if (MeasureData.IsSuccess)
            {
                yield return Property<T>.Create(MeasureUrn, MeasureData.Value, At);
            }

            if (StatusUrn != null && MeasureData.IsSuccess)
            {
                yield return Property<MeasureStatus>.Create(StatusUrn, MeasureStatus.Success, At);
            }

            if (StatusUrn != null && MeasureData.IsError)
            {
                yield return Property<MeasureStatus>.Create(StatusUrn, MeasureStatus.Failure, At);
            }
        }

        public void SetData(Result<object> value, TimeSpan at)
        {
            throw new NotSupportedException("Data can't be changed");
        }
    }


    public readonly struct FailedMeasure : IMeasure
    {
        private readonly PropertyUrn<MeasureStatus> _statusUrn;
        private readonly TimeSpan _currentTime;

        public FailedMeasure(Urn statusUrn, TimeSpan currentTime)
        {
            _statusUrn = (PropertyUrn<MeasureStatus>) statusUrn;
            _currentTime = currentTime;
        }
        public IEnumerable<IDataModelValue> ModelValues()
        {
            yield return Property<MeasureStatus>.Create(_statusUrn,MeasureStatus.Failure,_currentTime);
        }

        public void SetData(Result<object> value, TimeSpan at)
        {
            throw new NotSupportedException("Failed measure can't have data");
        }
    }
}