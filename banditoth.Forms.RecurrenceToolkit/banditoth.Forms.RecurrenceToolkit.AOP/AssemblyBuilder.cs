using System;
using System.IO;
using System.Linq;
using banditoth.Forms.RecurrenceToolkit.AOP.Interfaces;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Microsoft.Build.Utilities;
using Microsoft.Build.Framework;
using System.Threading;

namespace banditoth.Forms.RecurrenceToolkit.AOP
{
    public class AssemblyBuilder : Task
    {
        public AssemblyBuilder()
        {
        }

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

            using (ModuleDefinition module = ModuleDefinition.ReadModule(AssemblyFileName, new ReaderParameters() { ReadWrite = true, InMemory = true }))
            {
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

                        foreach (var customAttribute in method.CustomAttributes)
                        {
                            if (customAttribute.AttributeType is TypeDefinition methodAttribute)
                            {
                                if (methodAttribute.HasInterfaces == false)
                                    continue;

                                if (methodAttribute.Interfaces.Any(z => z.InterfaceType.FullName == typeof(IMethodInterceptor).FullName))
                                {
                                    MethodDefinition onEnterMethod = methodAttribute.Methods.Single<MethodDefinition>(z => z.Name == nameof(AOP.Interfaces.IMethodInterceptor.OnEnter));
                                    MethodDefinition onExitMethod = methodAttribute.Methods.Single<MethodDefinition>(z => z.Name == nameof(AOP.Interfaces.IMethodInterceptor.OnExit));
                                    MethodDefinition onExceptionMethod = methodAttribute.Methods.Single<MethodDefinition>(z => z.Name == nameof(AOP.Interfaces.IMethodInterceptor.OnException));

                                    var processor = method.Body.GetILProcessor();

                                    // Creating new instance of the attribute

                                    // Getting constructor for attribute
                                    MethodDefinition attributeCtor = methodAttribute.Methods.FirstOrDefault(z => z.IsConstructor == true && z.HasParameters == false);
                                    if (attributeCtor == null)
                                        throw new Exception($"No parameterless constructor found for attribute: " + methodAttribute.FullName);


                                    var attributeInstance = new VariableDefinition(methodAttribute);
                                    method.Body.Variables.Add(attributeInstance);

                                    processor.InsertBefore(method.Body.Instructions.First(), processor.Create(OpCodes.Newobj, attributeCtor));
                                    processor.InsertAfter(method.Body.Instructions.First(), processor.Create(OpCodes.Stloc, attributeInstance));

                                    processor.InsertAfter(method.Body.Instructions[1], processor.Create(OpCodes.Ldloc, attributeInstance));
                                    processor.InsertAfter(method.Body.Instructions[2], processor.Create(OpCodes.Call, onEnterMethod));

                                    var lastInstruction = method.Body.Instructions.Last(z => z.OpCode == OpCodes.Nop || z.OpCode == OpCodes.Br_S);
                                    processor.InsertAfter(lastInstruction, processor.Create(OpCodes.Ldloc, attributeInstance));
                                    processor.InsertAfter(lastInstruction.Next, processor.Create(OpCodes.Call, onExitMethod));
                                }
                            }
                        }
                    }
                }

                module.Write(AssemblyFileName);
            }

            return true;
        }
    }
}
