using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functions
{
    public class DigitCount
    {
        // NumberOfDigits 静态方法计算
        // 传递的字符串中数字字符的数目：
        public static int NumberOfDigits(string theString)
        {
            int count = 0;
            for (int i = 0; i < theString.Length; i++)
            {
                if (Char.IsDigit(theString[i]))
                {
                    count++;
                }
            }
            return count;
        }
    }

    public class Factorial
    {
        // “Calc”静态方法为传入的指定整数
        // 计算阶乘值：
        public static int Calc(int i)
        {
            return ((i <= 1) ? 1 : (i * Calc(i - 1)));
        }
    }
}
