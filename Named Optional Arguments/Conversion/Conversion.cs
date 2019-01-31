using System;

namespace Samples
{
    internal struct RomanNumer
    {
        public RomanNumer(int value)
        {
            this.value = value;
        }

        // 声明从 int 到 RomanNumeral 的转换。请注意
        // operator 关键字的使用。这是名为
        // RomanNumeral 的转换运算符：
        public static implicit operator RomanNumer(int value)
        {
            // 请注意，由于 RomanNumeral 声明为结构，
            // 因此对该结构调用 new 只是调用构造函数
            // 而不是在堆上分配对象：
            return new RomanNumer(value);
        }

        // 声明从 RomanNumeral 到 int 的显式转换：
        public static explicit operator int(RomanNumer roman)
        {
            return roman.value;
        }

        // 声明从 RomanNumeral 到
        // string 的隐式转换：
        public static implicit operator string(RomanNumer roman)
        {
            return ("Conversion not yet implemented");
        }

        private int value;
    }

    internal partial class ConversionDemo
    {
        public static void RunConversion()
        {
            RomanNumer numeral;

            numeral = 10;

            // 调用从 numeral 到 int 的显式转换。由于是显式转换，
            // 因此必须使用强制转换：
            Console.WriteLine((int)numeral);

            // 调用到 string 的隐式转换。由于没有
            // 强制转换，到 string 的隐式转换是可以考虑的
            // 唯一转换：
            Console.WriteLine(numeral);

            // 调用从 numeral 到 int 的显式转换，
            // 然后调用从 int 到 short 的显式转换：
            short s = (short)numeral;

            Console.WriteLine(s);
        }
    }
}