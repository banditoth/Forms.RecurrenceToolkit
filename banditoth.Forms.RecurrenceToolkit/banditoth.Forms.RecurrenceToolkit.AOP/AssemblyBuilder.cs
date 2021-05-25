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
                    Log.LogError("Could not read module, it is null.");
                    throw new Exception();
                }

                if (module.HasTypes == false)
                {
                    Log.LogWarning("This assembly has no own types");
                    return true;
                }

                int modificationCount = default;

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

                                if (methodAttribute.Interfaces.Any(z => z.InterfaceType.FullName == typeof(IMethodDecorator).FullName))
                                {
                                    modificationCount++;

                                    MethodDefinition onEnterMethod = methodAttribute.Methods.Single<MethodDefinition>(z => z.Name == nameof(AOP.Interfaces.IMethodDecorator.OnEnter));
                                    MethodDefinition onExitMethod = methodAttribute.Methods.Single<MethodDefinition>(z => z.Name == nameof(AOP.Interfaces.IMethodDecorator.OnExit));
                                    //MethodDefinition onExceptionMethod = methodAttribute.Methods.Single<MethodDefinition>(z => z.Name == nameof(AOP.Interfaces.IMethodDecorator.OnException));

                                    var processor = method.Body.GetILProcessor();

                                    // Searching for a parameterless constructor for the attribute. 
                                    MethodDefinition attributeCtor = methodAttribute.Methods.FirstOrDefault(z => z.IsConstructor == true && z.HasParameters == false);
                                    if (attributeCtor == null)
                                        throw new Exception($"No parameterless constructor found for attribute: " + methodAttribute.FullName);

                                    // Registering the attribute type as a local variable.
                                    var attributeInstance = new VariableDefinition(methodAttribute);
                                    method.Body.Variables.Add(attributeInstance);

                                    // Adding IMethodDecorator imethoddecorator = new IMethodDecorator like instruments to the method beggining
                                    processor.InsertBefore(method.Body.Instructions.First(), processor.Create(OpCodes.Newobj, attributeCtor)); // This will be 1st
                                    processor.InsertAfter(method.Body.Instructions.First(), processor.Create(OpCodes.Stloc, attributeInstance)); // This will be 2nd

                                    // Invoking the onenter method on the newly created instance
                                    processor.InsertAfter(method.Body.Instructions[1], processor.Create(OpCodes.Ldloc, attributeInstance)); //This will be 3rd
                                    processor.InsertAfter(method.Body.Instructions[2], processor.Create(OpCodes.Call, onEnterMethod)); // Inserting after 3rd, so it will be 4th

                                    // Invoking the onexit method on the newly created instance
                                    // We have to find the really last instruction for the method. Last() is not playing, because we want to insert code
                                    // before the return opcode.
                                    var lastInstruction = method.Body.Instructions.Last(z => z.OpCode == OpCodes.Nop || z.OpCode == OpCodes.Br_S);
                                    processor.InsertAfter(lastInstruction, processor.Create(OpCodes.Ldloc, attributeInstance));
                                    processor.InsertAfter(lastInstruction.Next, processor.Create(OpCodes.Call, onExitMethod));
                                }
                            }
                        }
                    }
                }

                Log.LogMessage(MessageImportance.High, "Finished with the assembly. Total modifications: " + modificationCount);

                module.Write(AssemblyFileName);
            }

            return true;
        }
    }
}
