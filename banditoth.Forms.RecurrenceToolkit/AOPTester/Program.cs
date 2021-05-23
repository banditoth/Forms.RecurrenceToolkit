using System;
using banditoth.Forms.RecurrenceToolkit.AOP;

namespace AOPTester
{
    class Program
    {
        static void Main(string[] args)
        {
            AssemblyBuilder builder = new AssemblyBuilder();
            builder.Process("/Users/banditoth/Documents/GitHub/Forms.RecurrenceToolkit/banditoth.Forms.RecurrenceToolkit/AOPTester/bin/Debug/netcoreapp3.1/AOPTester.dll");
            TesterClass classNew = new TesterClass();
            classNew.ExampleMethod();
        }


    }
}
