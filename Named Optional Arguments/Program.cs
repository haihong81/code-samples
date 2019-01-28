using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Permissions;

namespace Named_Optional_Arguments
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            //RunNamedAndOptional();

            //RunPythonScripts();

            //Covariant();

            //DomainPermissionSet();

            //Yield();

            MyDerived.DerivedOverideAndHide();

            Console.ReadLine();
        }

        #region DomainPermission
        public static void DomainPermissionSet()
        {
            //创建文件 IO 读取权限
            FileIOPermission FileIOReadPermission = new FileIOPermission(PermissionState.None);
            FileIOReadPermission.AllLocalFiles = FileIOPermissionAccess.Read;

            //创建基本权限集
            PermissionSet BasePermissionSet = new PermissionSet(PermissionState.None); // PermissionState.Unrestricted 用于完全信任
            BasePermissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));

            PermissionSet grantset = BasePermissionSet.Copy();
            grantset.AddPermission(FileIOReadPermission);

            //编写示例源文件以读取
            System.IO.File.WriteAllText("TEST.TXT", "File Content");

            //-------- 完全信任地调用方法 -------- 
            try
            {
                Console.WriteLine("App Domain Name: " + AppDomain.CurrentDomain.FriendlyName);
                ReadFileMethod();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //-------- 创建具有文件 IO 读取权限的 AppDomain -------- 
            AppDomain sandbox = AppDomain.CreateDomain("Sandboxed AppDomain With FileIO.Read permission", AppDomain.CurrentDomain.Evidence, AppDomain.CurrentDomain.SetupInformation, grantset, null);
            try
            {
                Console.WriteLine("App Domain Name: " + AppDomain.CurrentDomain.FriendlyName);
                sandbox.DoCallBack(new CrossAppDomainDelegate(ReadFileMethod));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            //-------- 创建没有文件 IO 读取权限的 AppDomain -------- 
            //应当引发安全异常
            PermissionSet grantset2 = BasePermissionSet.Copy();
            //grantset2.AddPermission(FileIOReadPermission);

            AppDomain sandbox2 = AppDomain.CreateDomain("Sandboxed AppDomain Without FileIO.Read permission", AppDomain.CurrentDomain.Evidence, AppDomain.CurrentDomain.SetupInformation, grantset2, null);
            try
            {
                Console.WriteLine("App Domain Name: " + AppDomain.CurrentDomain.FriendlyName);
                sandbox2.DoCallBack(new CrossAppDomainDelegate(ReadFileMethod));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("");
            Console.WriteLine("Press any key to end.");
            Console.ReadKey();
        }

        public static void ReadFileMethod()
        {
            string S = System.IO.File.ReadAllText("TEST.TXT");
            Console.WriteLine("File Content: " + S);
            Console.WriteLine("");
        }
        #endregion

        #region Yield
        public static class NumberList
        {
            // 创建一个整数数组。
            public static int[] ints = { 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377 };

            // 定义一个仅返回偶数的属性。
            public static IEnumerable<int> GetEven()
            {
                // 使用 yield 返回列表中的偶数。
                foreach (int i in ints)
                    if (i % 2 == 0)
                        yield return i;
            }

            // 定义一个仅返回奇数的属性。
            public static IEnumerable<int> GetOdd()
            {
                // 使用 yield 仅返回奇数。
                foreach (int i in ints)
                    if (i % 2 == 1)
                        yield return i;
            }
        }

        private static void Yield()
        {
            // 显示偶数。
            Console.WriteLine("Even numbers");
            foreach (int i in NumberList.GetEven())
                Console.WriteLine(i);

            // 显示奇数。
            Console.WriteLine("Odd numbers");
            foreach (int i in NumberList.GetOdd())
                Console.WriteLine(i);
        }
        #endregion

        #region 协变和逆变代码
        private class theBase { }
        private class Derived : theBase { }

        //泛型参数 协变使用out修饰，逆变使用in来修饰的
        // __covariant:协变 子类转父类
        //__contravariant:逆变 父类转子类
        private delegate TParent CovariantFunc<out TParent>();
        private delegate void ContravariantAction<in TDecendent>(TDecendent a);

        private static void Covariant()
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
        #endregion

        #region 命名参数与可选参数
        // 具有命名参数和可选参数的方法
        public static void Search(string name, int age = 21, string city = "Pueblo")
        {
            Console.WriteLine("Name = {0} - Age = {1} - City = {2}", name, age, city);
        }

        private static void RunNamedAndOptional()
        {
            // 标准调用
            Search("Sue", 22, "New York");

            // 省略 city 参数
            Search("Mark", 23);

            // 显式命名 city 参数
            Search("Lucy", city: "Cairo");

            // 以相反顺序使用命名参数
            Search("Pedro", age: 45, city: "Saigon");
        }
        #endregion

        #region Python
        private static void RunPythonScripts()
        {
            Console.WriteLine("Loading helloworld.py...");

            ScriptRuntime py = Python.CreateRuntime();
            dynamic helloworld = py.UseFile("PythonScripts\\helloworld.py");

            Console.WriteLine("helloworld.py loaded!");

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(helloworld.welcome("Employee #{0}"), i);
                Console.WriteLine(helloworld.welcome1("Employee #{0}"), i);
            }
        }
        #endregion

        #region 继承重写隐藏
        public class MyBase
        {
            public virtual string Meth1()
            {
                return "MyBase-Meth1";
            }
            public virtual string Meth2()
            {
                return "MyBase-Meth2";
            }
            public virtual string Meth3()
            {
                return "MyBase-Meth3";
            }
        }

        public class MyDerived : MyBase
        {
            // 使用 override 关键字重写虚方法 Meth1：
            public override string Meth1()
            {
                return "MyDerived-Meth1";
            }
            // 使用 new 关键字显式隐藏
            // 虚方法 Meth2：
            public new string Meth2()
            {
                return "MyDerived-Meth2";
            }
            // 由于下面声明中没有指定任何关键字，
            // 因此将发出一个警告来提醒程序员
            // 该方法隐藏了继承的成员 MyBase.Meth3()：
            public string Meth3()
            {
                return "MyDerived-Meth3";
            }

            public static void DerivedOverideAndHide()
            {
                MyDerived mD = new MyDerived();
                MyBase mB = (MyBase)mD;

                //overide重写后转回 还是重写后的继承方法。
                System.Console.WriteLine(mB.Meth1());

                // 使用 new 关键字显式隐藏
                System.Console.WriteLine(mB.Meth2());

                // 该方法隐藏了继承的成员 MyBase.Meth3()：
                System.Console.WriteLine(mB.Meth3());
            }
        }
        #endregion

    }
}
