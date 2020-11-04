using System;

using dnlib.DotNet;
using dnlib.DotNet.Writer;

namespace AttributeInjector
{
    class Program
    {
        static void Main(string[] args) => new Program().Run(args);

        ModuleDef module;

        string[] options =
        {
            @"Type number of the attribute you want to inject: ",
            @"[01] => ConfuserEx Attribute", 
            @"[02] => Babel Obfuscator Attribute", 
            @"[03] => Crypto Obfuscator Attribute",
            @"[04] => DNGuard HVM Attribute",
            @"[05] => SmartAssembly Attribute",
            @"[06] => Agile.NET Attribute" 
        };

        public void Run(string[] args)
        {
            if (args.Length is 0)
                Environment.Exit(0);

            module = ModuleDefMD.Load(args[0]);
            Options();
            Console.ReadKey();
        }

        public void Options()
        {
            for (int i = 0; i < options.Length; i++)
                Console.WriteLine(options[i]);

            int value = int.Parse(Console.ReadLine());

            switch (value)
            {
                case 01:
                    ConfuserEx.Execute(module);
                    break;
                case 02:
                    BabelObfuscator.Execute(module);
                    break;
                case 03:
                    CryptoObfuscator.Execute(module);
                    break;
                case 04:
                    DNGuard.Execute(module);
                    break;
                case 05:
                    SmartAssembly.Execute(module);
                    break;
                case 06:
                    Agile.Execute(module);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Unknown value");
            }

            var opt = new ModuleWriterOptions(module)
            {
                Logger = DummyLogger.NoThrowInstance,
            };
            opt.MetadataOptions.Flags = MetadataFlags.PreserveAll;
            module.Write(@$"{Environment.CurrentDirectory}\{module.FullName} -AttributeInjector.exe", opt);

            Console.WriteLine("Attribute injected successfully! press a key to close.");
        }
    }
}