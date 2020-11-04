using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace AttributeInjector
{
    public class DNGuard
    {
        public static void Execute(ModuleDef module)
        {
            TypeRef typeRef = module.CorLibTypes.GetTypeRef("System", "Attribute");
            var typeDefUser = new TypeDefUser("", "ZYXDNGuarder", typeRef);
            module.Types.Add(typeDefUser);

            var ctor = new MethodDefUser(".ctor", MethodSig.CreateInstance(module.CorLibTypes.Void, module.CorLibTypes.String), MethodImplAttributes.Managed, MethodAttributes.HideBySig | MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName)
            {
                Body = new CilBody()
            };
            ctor.Body.MaxStack = 1;
            typeDefUser.Methods.Add(ctor);
        }
    }
}