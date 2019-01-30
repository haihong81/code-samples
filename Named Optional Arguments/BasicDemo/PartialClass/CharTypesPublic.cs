namespace Samples
{
    // 使用 partial 关键字可以在其他 .cs 文件中定义
    // 此类的附加方法、字段和属性。
    // 此文件包含 CharValues 定义的公共方法。
    internal partial class CharValues
    {
        public static int CountAlphabeticChars(string str)
        {
            int count = 0;
            foreach (char ch in str)
            {
                // IsAlphabetic 是在 CharTypesPrivate.cs 中定义的
                if (IsAlphabetic(ch))
                    count++;
            }
            return count;
        }

        public static int CountNumericChars(string str)
        {
            int count = 0;
            foreach (char ch in str)
            {
                // IsNumeric 是在 CharTypesPrivate.cs 中定义的
                if (IsNumeric(ch))
                    count++;
            }
            return count;
        }
    }
}