using System;

namespace ImpliciX.Language.Model
{
    [ModelObject]
    public class Property<T> : DataModelValue<T>
    {
        [ModelFactoryMethod]
        public static Property<T> Create(PropertyUrn<T> urn, T value, TimeSpan at)
        {
            return new Property<T>(urn, value, at);
        }

        private Property(PropertyUrn<T> urn, T value, TimeSpan at) : base(urn, value, at)
        {
        }

    }
}