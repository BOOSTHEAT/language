using System;
using System.Runtime.Serialization;
namespace ImpliciX.Language.Core
{
    [Serializable]
    public class ContractException : Exception
    {
        protected ContractException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ContractException(string message) : base(message)
        {
        }
    }
}