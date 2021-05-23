using System;
namespace banditoth.Forms.RecurrenceToolkit.AOP.Interfaces
{
    public interface IMethodInterceptor
    {
        void OnEnter();

        void OnException();

        void OnExit();
    }
}
