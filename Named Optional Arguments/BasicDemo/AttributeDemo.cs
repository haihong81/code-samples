using System;
using System.Collections;
using System.Reflection;



namespace TraceFunction
{
    using System.Diagnostics;

    public class Trace
    {
        [Conditional("DEBUG")]
        public static void Message(string traceMessage)
        {
            Console.WriteLine("[TRACE] - " + traceMessage);
        }
    }
}

namespace Samples
{
    using TraceFunction;

    public partial class AttributeDemo
    {
        public static void ShowConditional()
        {
            Trace.Message("Main Starting");
            Console.WriteLine("Main Method Running");
            Trace.Message("Main Ending");
        }
    }
}

/*******************************************************************************************************/

/*
Attributes for : Account
Author : Joe Programmer

Member AddOrder is tested!
Member ToString is NOT tested!
Member Equals is NOT tested!
Member GetHashCode is NOT tested!
Member GetType is NOT tested!

Attributes for : Order
Author : Jane Programmer Version : 2
Is Tested

Member ToString is NOT tested!
Member Equals is NOT tested!
Member GetHashCode is NOT tested!
Member GetType is NOT tested!
*/

namespace Samples
{
    public partial class AttributeDemo
    {
        private static bool IsMemberTested(MemberInfo member)
        {
            foreach (object attribute in member.GetCustomAttributes(true))
            {
                if (attribute is IsTestedAttribute)
                {
                    return true;
                }
            }
            return false;
        }

        private static void DumpAttributes(MemberInfo member)
        {
            Console.WriteLine("Attributes for : " + member.Name);
            foreach (object attribute in member.GetCustomAttributes(true))
            {
                Console.WriteLine(attribute);
            }
        }

        public static void Customerize()
        {
            // 显示 Account 类的特性
            DumpAttributes(typeof(Account));

            // 显示已测试成员的列表
            foreach (MethodInfo method in (typeof(Account)).GetMethods())
            {
                if (IsMemberTested(method))
                {
                    Console.WriteLine("Member {0} is tested!", method.Name);
                }
                else
                {
                    Console.WriteLine("Member {0} is NOT tested!", method.Name);
                }
            }
            Console.WriteLine();

            // 显示 Order 类的特性
            DumpAttributes(typeof(Order));

            // 显示 Order 类的方法的特性
            foreach (MethodInfo method in (typeof(Order)).GetMethods())
            {
                if (IsMemberTested(method))
                {
                    Console.WriteLine("Member {0} is tested!", method.Name);
                }
                else
                {
                    Console.WriteLine("Member {0} is NOT tested!", method.Name);
                }
            }
            Console.WriteLine();
        }
    }

    // IsTested 类是用户定义的自定义特性类。
    // 它可以应用于任何声明，包括
    //  - 类型(结构、类、枚举、委托)
    //  - 成员(方法、字段、事、属性、索引器)
    // 使用它时不带参数。
    public class IsTestedAttribute : Attribute
    {
        public override string ToString()
        {
            return "Is Tested";
        }
    }

    // AuthorAttribute 类是用户定义的特性类。
    // 它只能应用于类和结构声明。
    // 它采用一个未命名的字符串参数(作者的姓名)。
    // 它有一个可选的命名参数 Version，其类型为 int。
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class AuthorAttribute : Attribute
    {
        private string name;
        private int version;

        // 此构造函数为特性类指定未命名的参数。
        public AuthorAttribute(string name)
        {
            this.name = name;
            this.version = 0;
        }

        // 此属性为只读(它没有 set 访问器)
        // 因此，不能将其用作此特性的命名参数。
        public string Name
        {
            get
            {
                return name;
            }
        }

        // 此属性可读写(它有 set 访问器)
        // 因此，将此类用作属性类时，
        // 可以将其用作命名参数。
        public int Version
        {
            get
            {
                return version;
            }
            set
            {
                version = value;
            }
        }

        public override string ToString()
        {
            string value = "Author : " + Name;
            if (version != 0)
            {
                value += " Version : " + Version.ToString();
            }
            return value;
        }
    }

    // 此处，将用户定义的自定义特性 AuthorAttribute 附加
    // 到 Account 类。创建属性时，会将未命名的
    // 字符串参数传递到 AuthorAttribute 类的构造函数。
    [Author("Joe Programmer")]
    internal class Account
    {
        // 将自定义特性 IsTestedAttribute 附加到此方法。
        [IsTested]
        public void AddOrder(Order orderToAdd)
        {
            orders.Add(orderToAdd);
        }

        private ArrayList orders = new ArrayList();
    }

    // 将自定义特性 AuthorAttribute 和 IsTestedAttribute 附加
    // 到此类。
    // 请注意 AuthorAttribute 的命名参数“Version”的用法。
    [Author("Jane Programmer", Version = 2), IsTested]
    internal class Order
    {
        // 在此处添加资料...
    }
}