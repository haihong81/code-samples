using System;

namespace Samples
{
    internal partial class Program
    {
        private static void Main(string[] args)
        {
            Do("MyDerived.DerivedOverideAndHide", new Action(MyDerived.DerivedOverideAndHide));
            Do("NamedAndOptional.Run", new Action(NamedAndOptional.Run));
            Do("Generics.SortList", new Action(Generics.SortList));
            Do("UnsafeCode.FastCopy", new Action(UnsafeCode.FastCopy));
            Do("PythonScript.Run", new Action(PythonScript.Run));
            Do("CovariantDemo.Run", new Action(CovariantDemo.Run));
            Do("DomainPermission.SetDemo", new Action(DomainPermission.SetDemo));
            Do("IEnumerableDemo.Yield", new Action(IEnumerableDemo.Yield));

            Console.ReadLine();
        }

        private static void Do(string MethodName, Action action)
        {
            Console.WriteLine(MethodName);
            action();
            Console.ReadLine();
        }
    }
}
