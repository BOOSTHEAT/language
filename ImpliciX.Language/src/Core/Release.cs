using System;
namespace ImpliciX.Language.Core
{
    public static class Release
    {
        public static void Ensure(Func<bool> predicate, Func<string> message)
        {
            VerifyCondition(predicate(), message());          
        }

        private static void VerifyCondition(bool condition, string message)
        {
            if (!condition)
            {
                throw new ContractException($"Contract is not satisfied. {message}");
            }
        }
    }
}