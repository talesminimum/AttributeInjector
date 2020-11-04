using dnlib.DotNet;

namespace AttributeInjector
{
    public class ConfuserEx
    {
        public static void Execute(ModuleDef module)
        {
            TypeRef typeRef = module.CorLibTypes.GetTypeRef("System", "Attribute");
            var typeDefUser = new TypeDefUser("", "ConfusedByAttribute", typeRef);
            module.Types.Add(typeDefUser);
        }
    }
}