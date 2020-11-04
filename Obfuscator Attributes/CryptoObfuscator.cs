using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace AttributeInjector
{
    public class CryptoObfuscator
    {
        public static void Execute(ModuleDef module)
        {
            TypeRef typeRef = module.CorLibTypes.GetTypeRef("System", "Attribute");
            TypeDefUser typeDefUser = new TypeDefUser("", "CryptoObfuscator.ProtectedWithCryptoObfuscatorAttribute", typeRef);
            module.Types.Add(typeDefUser);

            var ctor1 = new MethodDefUser(".ctor", MethodSig.CreateInstance(module.CorLibTypes.Void, module.CorLibTypes.String), MethodImplAttributes.Managed, MethodAttributes.HideBySig | MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName)
            {
                Body = new CilBody()
            };
            ctor1.Body.MaxStack = 1;
            typeDefUser.Methods.Add(ctor1);
        }
    }
}