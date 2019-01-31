using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samples
{
    class TheClass
    {
        public int x;
    }

    struct TheStruct
    {
        public int x;
    }

    class ClassAndStructDemo
    {
        public static void structtaker(TheStruct s)
        {
            s.x = 5;
        }
        public static void classtaker(TheClass c)
        {
            c.x = 5;
        }
        public static void Show()
        {
            TheStruct a = new TheStruct();
            TheClass b = new TheClass();
            a.x = 1;
            b.x = 1;
            structtaker(a);
            classtaker(b);
            Console.WriteLine("Struct never change,Class changed");
            Console.WriteLine("Struct a.x = {0}", a.x);
            Console.WriteLine("Class b.x = {0}", b.x);

        }

        public static void GetSetMethod()
        {
            SimpleStruct ss = new SimpleStruct();
            ss.X = 5;
            ss.DisplayX();
        }
    }

    struct SimpleStruct
    {
        private int xval;
        public int X
        {
            get
            {
                return xval;
            }
            set
            {
                if (value < 100)
                    xval = value;
            }
        }
        public void DisplayX()
        {
            Console.WriteLine("The stored value is: {0}", xval);
        }
    }

}
