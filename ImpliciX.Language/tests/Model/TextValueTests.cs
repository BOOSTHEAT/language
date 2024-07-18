using System;
using System.Collections.Generic;
using ImpliciX.Language.Core;
using ImpliciX.Language.Model;
using NUnit.Framework;

namespace ImpliciX.Language.Tests.Model;

[TestFixture(typeof(Literal))]
[TestFixture(typeof(Text10))]
[TestFixture(typeof(Text50))]
[TestFixture(typeof(Text200))]
public class TextValueTests<T> where T: ITextValue
{
    private static readonly Dictionary<Type,int> MaxLength = new()
    {
        {typeof(Text10), 10},
        {typeof(Text50), 50},
        {typeof(Text200), 200},
        {typeof(Literal), 200}
    };

    [Test]
    public void create_from_string_text_filed_with_maxlength()
    {
        var maxLenght = MaxLength[typeof(T)];
        var str = new string('a', maxLenght+1);
        var text = (Result<T>)(typeof(T).GetMethod("FromString")?.Invoke(null, new object[] { str }));
        Assert.That(text!.IsSuccess, Is.True);
        Assert.That(text.Value.ToString().Length, Is.EqualTo(maxLenght));
    }
    
}