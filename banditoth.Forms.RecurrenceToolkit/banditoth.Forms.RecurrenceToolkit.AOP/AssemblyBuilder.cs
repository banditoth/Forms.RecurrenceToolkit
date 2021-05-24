using System;
using System.IO;
using System.Linq;
using banditoth.Forms.RecurrenceToolkit.AOP.Interfaces;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Microsoft.Build.Utilities;
using Microsoft.Build.Framework;

namespace banditoth.Forms.RecurrenceToolkit.AOP
{
    public class AssemblyBuilder : Task
    {
        [Required]
        public string AssemblyFileName { get; set; }

        public override bool Execute()
        {
            Log.LogMessage(MessageImportance.High, "Invoking assembly rewriting for: " + AssemblyFileName);

            if (File.Exists(AssemblyFileName) == false)
            {
                Log.LogError("Could not find assembly with name: " + AssemblyFileName);
                return false;
            }

            ModuleDefinition module = ModuleDefinition.ReadModule(AssemblyFileName);

            if (module == null)
            {
                // LOG
                throw new Exception();
            }

            if (module.HasTypes == false)
            {
                // LOG
                throw new Exception();
            }



            foreach (var type in module.Types)
            {
                if (type.HasMethods == false)
                    continue;

                foreach (var method in type.Methods)
                {
                    if (method.HasCustomAttributes == false)
                        continue;

                    if (method.HasBody == false)
                        continue;

                    foreach (var attribute in method.CustomAttributes)
                    {
                        if (attribute.AttributeType is TypeDefinition typeDef)
                        {
                            if (typeDef.HasInterfaces == false)
                                continue;

                            if (typeDef.Interfaces.Any(z => z.InterfaceType.FullName == typeof(IMethodInterceptor).FullName))
                            {
                                MethodDefinition onEnterMethod = typeDef.Methods.Single(z => z.Name == nameof(IMethodInterceptor.OnEnter));
                                MethodDefinition onExitMethod = typeDef.Methods.Single(z => z.Name == nameof(IMethodInterceptor.OnExit));

                                var processor = method.Body.GetILProcessor();

                                processor.InsertBefore(method.Body.Instructions.First(), processor.Create(OpCodes.Call, onEnterMethod));
                                processor.InsertAfter(method.Body.Instructions.Last(), processor.Create(OpCodes.Call, onExitMethod));
                            }
                        }
                    }
                }
            }

            module.Write(AssemblyFileName);
            return true;
        }
    }
}
