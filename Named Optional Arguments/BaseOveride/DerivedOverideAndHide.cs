namespace Samples
{
    internal partial class Program
    {
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
    }
}