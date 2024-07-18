using System;
using System.Linq;
using System.Reflection;
using System.Text;
using ImpliciX.Language.Model;
using NUnit.Framework;

namespace ImpliciX.Language.Tests.Model
{
    [TestFixture]
    public class ModelObjectsDefinitionTests
    {

        [Test]
        public void model_objects_should_define_factory_method()
        {
            var assembly = (typeof(ValueObject)).Assembly;
            var types = assembly.GetTypes().Where(MustDeclareFactoryMethod).ToArray();
            var typesNotHavingFactory =
                types.Where(vo => !HasFactoryMethod(vo)).Select(t=>t.FullName).ToArray();
            if(typesNotHavingFactory.Any()) Assert.Fail(OutputMessage(typesNotHavingFactory, $"All types should define a factory method."));
        }

 

        [Test]
        public void value_objects_should_override_to_string()
        {
            var assembly = (typeof(ValueObject)).Assembly;
            var allValueObjects = assembly.GetTypes()
                .Where(t => Reflector.HasAttribute(t,typeof(ValueObject))).ToArray();
            var typesNotOverridingToString =
                allValueObjects.Where(vo => !OverridesToString(vo)).Select(t=>t.Name).ToArray();
            if(typesNotOverridingToString.Any()) Assert.Fail(OutputMessage(typesNotOverridingToString,"All types should override ToString for compatibility with InterModuleCommand which requires Arg as string."));
        }

        private string OutputMessage(string[] typeNames, string message)
        {
            var sb = new StringBuilder(message);
            sb.AppendLine();
            sb.AppendLine("The following don't : ");
            foreach (var typeName in typeNames)
            {
                sb.AppendLine(typeName);
            }

            return sb.ToString();
        }

        private static bool MustDeclareFactoryMethod(Type t)
        {
            return Reflector.HasAttribute(t,typeof(ValueObject)) || 
                   Reflector.HasAttribute(t,typeof(UrnObject)) || 
                   Reflector.HasAttribute(t,typeof(ModelObject));
        }
        private bool HasFactoryMethod(Type type)
        {
            if (type.IsEnum) return true;
            return Reflector.GetFactoryMethod(type)!=null;
        }
        private bool OverridesToString(Type type)
        {
            if (type.IsEnum) return true;
            return type.GetMethod(nameof(ToString), BindingFlags.Instance| BindingFlags.DeclaredOnly | BindingFlags.Public) != null;
        }
    }
    
    static class Reflector
    {
        public static bool HasAttribute(Type type, Type attributeType)
        {
            return type.CustomAttributes.Any(a => a.AttributeType == attributeType);
        }

        public static MethodInfo GetFactoryMethod(Type type)
        {
            var factory = type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.DeclaredOnly)
                .FirstOrDefault(m => m.CustomAttributes.Any(a => a.AttributeType == typeof(ModelFactoryMethod)));
            return factory;
        }
    }
}