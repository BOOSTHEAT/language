using System;
using System.Linq;

namespace ImpliciX.Language.Core
{
    public static class SafeEnum
    {
        public static Result<T> TryParse<T>(string value, Func<string,Error> errFn) where T : struct, Enum
        {
            var isValid = Enum.TryParse<T>(value, true, out var result);
            if (!isValid) return errFn(FormatErrorMessage(value,typeof(T)));
            return result;
        }

        public static Result<object> TryParse(Type enumType, string value, Func<string,Error> errFn)
        {
            return SideEffect.TryRun(() => Enum.Parse(enumType, value, true), ()=>errFn(FormatErrorMessage(value, enumType)));
        }
        private static string FormatErrorMessage(string value, Type enumType)
        {
            return $"The value: '{value}' is not valid for type {enumType.Name}. Accepted values are: {EnumNamesAsString(enumType)}, the case is not checked.";
        }


        private static string EnumNamesAsString(Type enumType) 
        {
            var names = Enum.GetNames(enumType);
            if (names.Length == 0) return "''";
            return names.Skip(1).Aggregate($"'{names[0]}'", (acc, n) => acc + $",'{n}'");
        }

 
    }
}