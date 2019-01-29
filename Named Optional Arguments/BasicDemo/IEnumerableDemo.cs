using System;
using System.Collections;
using System.Collections.Generic;

namespace Samples
{
    public class IEnumerableDemo
    {
        public static void Yield()
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

        public static void EnumerateTokens()
        {
            // 通过将字符串分解为标记来测试标记:
            Tokens f = new Tokens("This is a well-done program.",
               new char[] { ' ', '-' });
            foreach (string item in f)
            {
                Console.WriteLine(item);
            }
        }
    }

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
    // 声明 Tokens 类:
    public class Tokens : IEnumerable
    {
        private string[] elements;

        public Tokens(string source, char[] delimiters)
        {
            // 将字符串分析为标记:
            elements = source.Split(delimiters);
        }

        // IEnumerable 接口实现:
        // 声明 IEnumerable 所需的
        // GetEnumerator() 方法
        public IEnumerator GetEnumerator()
        {
            return new TokenEnumerator(this);
        }

        // 内部类实现 IEnumerator 接口:
        private class TokenEnumerator : IEnumerator
        {
            private int position = -1;
            private Tokens t;

            public TokenEnumerator(Tokens t)
            {
                this.t = t;
            }

            // 声明 IEnumerator 所需的 MoveNext 方法:
            public bool MoveNext()
            {
                if (position < t.elements.Length - 1)
                {
                    position++;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            // 声明 IEnumerator 所需的 Reset 方法:
            public void Reset()
            {
                position = -1;
            }

            // 声明 IEnumerator 所需的 Current 属性:
            public object Current
            {
                get
                {
                    return t.elements[position];
                }
            }
        }
    }
}