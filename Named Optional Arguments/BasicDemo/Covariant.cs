using System;

namespace Samples
{
    internal class CovariantDemo
    {
        private class theBase { }

        private class Derived : theBase { }

        //泛型参数 协变使用out修饰，逆变使用in来修饰的
        // __covariant:协变 子类转父类
        //__contravariant:逆变 父类转子类
        private delegate TParent CovariantFunc<out TParent>();

        private delegate void ContravariantAction<in TDecendent>(TDecendent a);

        public static void Run()
        {
            //父类函数指向子类函数 协变 子类方法转变为父类方法
            CovariantFunc<Derived> DecendentFunc = () => new Derived();
            CovariantFunc<theBase> ParentFunc = DecendentFunc;

            //子类方法指向父类方法 逆变 父类方法转变为子类方法
            ContravariantAction<theBase> ParentAction = (parent) => { Console.WriteLine(parent); };
            ContravariantAction<Derived> DecendentAction = ParentAction;

            //.net内置逆变协变泛型类
            //Action<in T>
            //IEnumerable<out T>
            //Func<out TResult>
            //IReadOnlyList<out T>
            //IReadOnlyCollection<out T>
            Action<theBase> Act = (p) => Console.WriteLine(p);
            Func<Derived> func = () => new Derived();

            Console.WriteLine(ParentFunc());
            DecendentAction(new Derived());

            Console.WriteLine(func());
            Act(new Derived());
        }
    }
}