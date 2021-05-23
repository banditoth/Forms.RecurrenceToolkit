using System;
using banditoth.Forms.RecurrenceToolkit.AOP.Interfaces;

namespace AOPTester
{
    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class SampleInterceptor : Attribute, IMethodInterceptor
    {
        public SampleInterceptor()
        {
        }

        public void OnEnter()
        {
            Console.WriteLine("Interceptor onEnter");
        }

        public void OnException()
        {

        }

        public void OnExit()
        {
            Console.WriteLine("Interceptor onExit");
        }
    }
}
