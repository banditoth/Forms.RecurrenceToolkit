using System;
namespace AOPTester
{
    public class TesterClass
    {
        [SampleInterceptor]
        public void ExampleMethod()
        {
            Console.WriteLine("wooow");
        }
    }
}
