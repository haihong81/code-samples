using System;
using System.Collections.Generic;

namespace Samples
{
    internal partial class Program
    {
        private static void Main(string[] args)
        {
            foreach (var demo in ActionDemos())
            {
                Run(demo);
            }
            //foreach (var demo in FuncDemos())
            //{
            //    Run(demo);
            //}
        }

        private static List<Action> ActionDemos()
        {
            List<Action> actions = new List<Action>();
            //List<Func<int>> funcs = new List<Func<int>>();


            actions.Add(new Action(NullableOperator.BoxingDemo));
            actions.Add(new Action(NullableOperator.OperatorDemo));
            actions.Add(new Action(NullableOperator.DisplayValueDemo));


            actions.Add(new Action(UnsafeCode.FastCopy));
            actions.Add(new Action(UnsafeCode.ReadFile));
            ////此方法无法查询出版本 原因不知 Can't PrintVersion Don't know Why
            //actions.Add(new Action(UnsafeCode.PrintVersion));

            actions.Add(new Action(PartialClassesDemo.Run));
            actions.Add(new Action(MyDerived.OverideDemo));
            actions.Add(new Action(NamedAndOptional.Run));
            actions.Add(new Action(Generics.SortList));

            actions.Add(new Action(PythonScript.Run));
            actions.Add(new Action(CovariantDemo.Run));

            actions.Add(new Action(DomainPermission.SetDemo));

            //调用错误 堆栈不平衡
            //actions.Add(new Action(PlatformInvokeDemo.Run));
            //actions.Add(new Action(PlatformInvokeDemo1.Run));
            //actions.Add(new Action(PlatformInvokeDemo2.Run));

            actions.Add(new Action(ClassAndStructDemo.Run));
            actions.Add(new Action(ClassAndStructDemo.GetSetMethod));

            actions.Add(new Action(IEnumerableDemo.Yield));
            actions.Add(new Action(IEnumerableDemo.EnumerateTokens));

            actions.Add(new Action(LibraryRefDemo.Run));

            return actions;
        }

        //private static List<Func<int>> FuncDemos()
        //{

        //    List<Func<int>> funcs = new List<Func<int>>();

        //    return funcs;
        //}

        private static void Run(Action action)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(action.Method.DeclaringType.Name + " : " + action.Method.ToString());

            Console.ForegroundColor = ConsoleColor.White;
            action();

            Console.ReadLine();
        }

        //private static void Run(Func<int> func)
        //{
        //    Console.ForegroundColor = ConsoleColor.Green;
        //    Console.WriteLine(func.Method.DeclaringType.Name + " : " + func.Method.ToString());

        //    Console.ForegroundColor = ConsoleColor.White;
        //    func();

        //    Console.ReadLine();
        //}

    }
}