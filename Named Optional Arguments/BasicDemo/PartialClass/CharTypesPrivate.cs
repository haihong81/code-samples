namespace Samples
{
    // 使用 partial 关键字可以在其他 .cs 文件中定义
    // 此类的附加方法、字段和属性。
    // 此文件包含 CharValues 定义的私有方法。
    internal partial class CharValues
    {
        private static bool IsAlphabetic(char ch)
        {
            if (ch >= 'a' && ch <= 'z')
                return true;
            if (ch >= 'A' && ch <= 'Z')
                return true;
            return false;
        }

        private static bool IsNumeric(char ch)
        {
            if (ch >= '0' && ch <= '9')
                return true;
            return false;
        }
    }
}