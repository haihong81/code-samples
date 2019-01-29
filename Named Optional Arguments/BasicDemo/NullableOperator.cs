using System;

namespace Samples
{
    internal class NullableOperator
    {
        public static void DisplayValueDemo()
        {
            DisplayValue(1);
            DisplayValue(null);
        }

        private static void DisplayValue(int? num)
        {
            if (num.HasValue == true)
            {
                Console.WriteLine("num = " + num);
            }
            else
            {
                Console.WriteLine("num = null");
            }

            // 如果 num.HasValue 为 false，则 num.Value 引发 InvalidOperationException
            try
            {
                Console.WriteLine("value = {0}", num.Value);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void BoxingDemo()
        {
            int? a;
            object oa;

            // 为 a 赋值 Nullable<int> (value = default(int), hasValue = false)。
            a = null;

            // 为 oa 赋值 null（因为 x==null），而不是装箱的“int?”。
            oa = a;

            Console.WriteLine("Testing 'a' and 'boxed a' for null...");
            // 可以将可以为 null 的变量与 null 进行比较。
            if (a == null)
            {
                Console.WriteLine("  a == null");
            }

            // 可以将装箱的可以为 null 的变量与 null 进行比较，
            // 因为对 HasValue==false 的可以为 null 的变量进行装箱
            // 将把引用设置为 null。
            if (oa == null)
            {
                Console.WriteLine("  oa == null");
            }

            Console.WriteLine("Unboxing a nullable type...");
            int? b = 10;
            object ob = b;

            // 装箱的可以为 null 的类型可以取消装箱
            int? unBoxedB = (int?)ob;
            Console.WriteLine("  b={0}, unBoxedB={0}", b, unBoxedB);

            // 如果是取消装箱为可以为 null 的类型，则可以对设置为 null 的可以为 null 的类型
            // 进行取消装箱。
            int? unBoxedA = (int?)oa;
            if (oa == null && unBoxedA == null)
            {
                Console.WriteLine("  a and unBoxedA are null");
            }

            Console.WriteLine("Attempting to unbox into non-nullable type...");
            // 如果是取消装箱为不可以为 null 的类型，则对设置为 null 的可以为 null 的类型
            //取消装箱将引发异常。
            try
            {
                int unBoxedA2 = (int)oa;
            }
            catch (Exception e)
            {
                Console.WriteLine("  {0}", e.Message);
            }
        }

        public static void OperatorDemo()
        {
            // ?? 运算符示例。
            int? x = null;

            // y = x，只有当 x 为 null 时，y = -1。
            int y = x ?? -1;
            Console.WriteLine("y == " + y);

            // 将方法的返回值赋给 i，
            // 仅当返回值为 null 时，
            // 将默认的 int 值赋给 i。
            int i = GetNullableInt() ?? default(int);
            Console.WriteLine("i == " + i);

            // ?? 也适用于引用类型。
            string s = GetStringValue();
            // 显示 s 的内容，仅当 s 为 null 时，
            // 显示“未指定”。
            Console.WriteLine("s = {0}", s ?? "null");
        }

        private static int? GetNullableInt()
        {
            return null;
        }

        private static string GetStringValue()
        {
            return null;
        }
    }
}