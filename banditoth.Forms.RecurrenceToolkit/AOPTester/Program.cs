using System;
using banditoth.Forms.RecurrenceToolkit.AOP;

namespace AOPTester
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                TesterClass classNew = new TesterClass();
                classNew.ExampleMethod();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex?.ToString());
            }
        }
    }
}
