using System;
using System.IO;
using System.Linq;
using banditoth.Forms.RecurrenceToolkit.AOP.Interfaces;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace banditoth.Forms.RecurrenceToolkit.AOP
{
    public class AssemblyBuilder
    {
        public void Process(string assemblyFileName)
        {
            if(File.Exists(assemblyFileName) == false)
            {
                // LOG
                return;
            }

            ModuleDefinition module = ModuleDefinition.ReadModule(assemblyFileName);

            if(module == null)
            {
                // LOG
                throw new Exception();
            }

            if(module.HasTypes == false)
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
                        return;

                    foreach (var attribute in method.CustomAttributes)
                    {
                        if(attribute.AttributeType is TypeDefinition typeDef)
                        {
                            if (typeDef.HasInterfaces == false)
                                continue;

                            if(typeDef.Interfaces.Any(z=> z.InterfaceType.FullName == typeof(IMethodInterceptor).FullName))
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

            module.Write(assemblyFileName + "-modified.dll");
        }
    }
}
