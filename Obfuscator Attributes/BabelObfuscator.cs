using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace AttributeInjector
{
    public class BabelObfuscator
    {
        public static void Execute(ModuleDef module)
        {
            TypeRef typeRef = module.CorLibTypes.GetTypeRef("System", "Attribute");
            TypeDefUser typeDefUser = new TypeDefUser("", "BabelObfuscatorAttribute", typeRef);
            module.Types.Add(typeDefUser);

            var ctor = new MethodDefUser(".ctor", MethodSig.CreateInstance(module.CorLibTypes.Void, module.CorLibTypes.String), MethodImplAttributes.Managed, MethodAttributes.HideBySig | MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName)
            {
                Body = new CilBody
                {
                    MaxStack = 1
                }
            };
            ctor.Body.Instructions.Add(OpCodes.Ldarg_0.ToInstruction());
            ctor.Body.Instructions.Add(OpCodes.Call.ToInstruction(new MemberRefUser(module, ".ctor", MethodSig.CreateInstance(module.CorLibTypes.Void))));
            ctor.Body.Instructions.Add(OpCodes.Ret.ToInstruction());
            typeDefUser.Methods.Add(ctor);
        }
    }
}