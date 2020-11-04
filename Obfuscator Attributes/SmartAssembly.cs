using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace AttributeInjector
{
    public class SmartAssembly
    {
        public static void Execute(ModuleDef module)
        {
            TypeRef typeRef = module.CorLibTypes.GetTypeRef("System", "Attribute");
            TypeDefUser typeDefUser = new TypeDefUser("", "SmartAssembly.Attributes.PoweredByAttribute", typeRef);
            module.Types.Add(typeDefUser);

            var ctor = new MethodDefUser(".ctor", MethodSig.CreateInstance(module.CorLibTypes.Void, module.CorLibTypes.String), MethodImplAttributes.Managed, MethodAttributes.HideBySig | MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName)
            {
                Body = new CilBody
                {
                    MaxStack = 1
                }
            };
            typeDefUser.Methods.Add(ctor);
        }
    }
}