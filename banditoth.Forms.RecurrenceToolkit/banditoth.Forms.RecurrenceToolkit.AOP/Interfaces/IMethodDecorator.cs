using System;
namespace banditoth.Forms.RecurrenceToolkit.AOP.Interfaces
{
    public interface IMethodDecorator
    {
        void OnEnter();

        void OnExit();
    }
}
