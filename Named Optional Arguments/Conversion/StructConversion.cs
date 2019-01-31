using System;

namespace Samples
{
    internal struct RomanNumeral
    {
        public RomanNumeral(int value)
        {
            this.value = value;
        }

        public static implicit operator RomanNumeral(int value)
        {
            return new RomanNumeral(value);
        }

        public static implicit operator RomanNumeral(BinaryNumeral binary)
        {
            return new RomanNumeral((int)binary);
        }

        public static explicit operator int(RomanNumeral roman)
        {
            return roman.value;
        }

        public static implicit operator string(RomanNumeral roman)
        {
            return ("Conversion not yet implemented");
        }

        private int value;
    }

    internal struct BinaryNumeral
    {
        public BinaryNumeral(int value)
        {
            this.value = value;
        }

        public static implicit operator BinaryNumeral(int value)
        {
            return new BinaryNumeral(value);
        }

        public static implicit operator string(BinaryNumeral binary)
        {
            return ("Conversion not yet implemented");
        }

        public static explicit operator int(BinaryNumeral binary)
        {
            return (binary.value);
        }

        private int value;
    }

    internal partial class ConversionDemo
    {
        public static void RunStructConverssion()
        {
            RomanNumeral roman;
            roman = 10;
            BinaryNumeral binary;
            // 执行从 RomanNumeral 到
            // BinaryNumeral 的转换：
            binary = (BinaryNumeral)(int)roman;
            // 执行从 BinaryNumeral 到 RomanNumeral 的转换。
            // 不需要任何强制转换：
            roman = binary;
            Console.WriteLine((int)binary);
            Console.WriteLine(binary);
        }
    }
}