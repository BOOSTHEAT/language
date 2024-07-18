using System;
using ImpliciX.Language.Core;
namespace ImpliciX.Language.Tests.Core
{
    public class SomeError : Error
    {
        public SomeError(Exception e) : base(nameof(SomeError), e.Message) { }
      
        public SomeError(string message) : base(nameof(SomeError), message){}
    }
    
}