using System;

namespace Samples
{
    internal class PartialClassesDemo
    {
        public static void Run()
        {
            string str = "abc123";
            // CharValues 是一个分部类 -- 该分部类有两个方法
            // 是在 CharTypesPublic.cs 中定义的，另有两个方法是在
            // CharTypesPrivate.cs 中定义的。
            int aCount = CharValues.CountAlphabeticChars(str);
            int nCount = CharValues.CountNumericChars(str);

            Console.Write("The input argument contains {0} alphabetic and {1} numeric characters", aCount, nCount);
        }
    }
}