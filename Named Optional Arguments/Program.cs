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
            return new List<Action>(){

                //new Action(PartialClassesDemo.Run),             //部分类
                //new Action(MyDerived.OverideDemo),              //类的重写 overide
                //new Action(NamedAndOptional.Run),               //可选的命名参数
                //new Action(Generics.SortList),                  //泛型                                    
                //new Action(CovariantDemo.Run),                  //继承与协变逆变

                ////Struct与Class的区别
                //new Action(ClassAndStructDemo.Run),
                //new Action(ClassAndStructDemo.GetSetMethod),

                ////IEnumerable
                //new Action(IEnumerableDemo.Yield),
                //new Action(IEnumerableDemo.EnumerateTokens),

                ////可空对象
                //new Action(NullableOperator.BoxingDemo),
                //new Action(NullableOperator.OperatorDemo),
                //new Action(NullableOperator.DisplayValueDemo),

                ////属性
                //new Action(PropertiesDemo.PropertiesOveride),
                //new Action(PropertiesDemo.PropertiesOveride),

                //特性 Custom Attribute
                //new Action(AttributeDemo.Run),                //Custom Attribute
                //new Action(AttributeDemo.ShowConditional),    //ConditionalAttribute

                //索引
                new Action(IndexDemo.Reverse),
                new Action(IndexDemo.RunReplace), 

                ////Event
                //new Action(EventListenDemo.Run),

                //Delegate
                //new Action(DelegateDemo.ProcessBookMethods),
                //new Action(DelegateDemo.Compose),

                ////Interface
                //new Action(SingleInterfaceDemoBox.Show),
                //new Action(MultiInterfaceDemoBox.Show),  

                //Threads
                //new Action(ThreadDemo.RunWorkers),                                                 
                //new Action(ThreadDemo.RunPool),
                //new Action(ThreadDemo.RunSync),                                                 

                //Conversion
                //new Action(ConversionDemo.RunConversion),
                //new Action(ConversionDemo.RunStructConverssion),

                //Operator Overlording
                //new Action(OperatorOverlordingDemo.RunDBBool),
                //new Action(OperatorOverlordingDemo.RunComplex),
                

                ////不安全代码
                //new Action(UnsafeCode.FastCopy),
                //new Action(UnsafeCode.ReadFile),

                //不安全代码 此方法无法查询出版本 原因不知 Can't PrintVersion Don't know Why
                //new Action(UnsafeCode.PrintVersion),

                //权限
                //new Action(DomainPermission.SetDemo),           

                //Python脚本
                //new Action(PythonScript.Run),                   

                ////类库引用
                //new Action(LibraryRefDemo.Run),

                ////平台调用 调用错误 堆栈不平衡  
                ////new Action(PlatformInvokeDemo.Run),
                ////new Action(PlatformInvokeDemo1.Run),
                ////new Action(PlatformInvokeDemo2.Run),
            };
        }

        //private static List<Func<int>> FuncDemos()
        //{
        //    List<Func<int>> funcs = new List<Func<int>>(){


        //    };

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