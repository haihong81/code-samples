using System;
using System.Collections.Generic;

namespace Samples
{
    internal partial class Program
    {
        private static void Main(string[] args)
        {
            foreach (var demo in Demos())
            {
                Run(demo);
            }
        }

        private static List<Action> Demos()
        {
            List<Action> actions = new List<Action>();

            actions.Add(new Action(MyDerived.DerivedOverideAndHide));
            actions.Add(new Action(NamedAndOptional.Run));
            actions.Add(new Action(Generics.SortList));
            actions.Add(new Action(UnsafeCode.FastCopy));
            actions.Add(new Action(PythonScript.Run));
            actions.Add(new Action(CovariantDemo.Run));
            actions.Add(new Action(DomainPermission.SetDemo));
            actions.Add(new Action(IEnumerableDemo.Yield));

            return actions;
        }

        private static void Run(Action action)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(action.Method.DeclaringType.Name + " : " + action.Method.ToString());

            Console.ForegroundColor = ConsoleColor.White;
            action();

            Console.ReadLine();
        }
    }
}