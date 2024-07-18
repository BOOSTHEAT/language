using System;
using System.Collections.Generic;
using System.Text;
namespace ImpliciX.Language.Core
{
    public static class ExceptionExtensions
    {

        public static string CascadeMessage(this Exception @this)
        {
            var sb = new StringBuilder();
            foreach (var message in @this.AllMessages())
                sb.AppendLine(message);
            return sb.ToString();
        }
        
        public static IEnumerable<string> AllMessages(this Exception @this)
        {
            var currentException = @this;
            while (currentException != null)
            {
                yield return @currentException.Message;
                currentException = currentException.InnerException;
            }
        }
        
    }
}