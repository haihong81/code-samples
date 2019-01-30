namespace Samples
{
    #region RunSingleExplicit


    internal interface IDimensions
    {
        float Length();
        float Width();
    }

    internal class SingleInterfaceDemoBox : IDimensions
    {
        private float lengthInches;
        private float widthInches;

        public SingleInterfaceDemoBox(float length, float width)
        {
            lengthInches = length;
            widthInches = width;
        }
        // 显式接口成员实现：
        public float Length()
        {
            return lengthInches;
        }
        // 显式接口成员实现：
        float IDimensions.Width()
        {
            return widthInches;
        }

        public static void Show()
        {
            // 声明类实例“myBox”：
            SingleInterfaceDemoBox myBox = new SingleInterfaceDemoBox(30.0f, 20.0f);
            // 声明接口实例“myDimensions”：
            IDimensions myDimensions = (IDimensions)myBox;
            // 打印出盒子的尺寸：
            /* 下列注释行将产生编译 
               错误，因为这些行尝试从类实例访问显式实现的
               接口成员：                    */
            //System.Console.WriteLine("Length: {0}", myBox.Length());
            //System.Console.WriteLine("Width: {0}", myBox.Width());
            /* 从接口的实例调用方法，
               以打印出盒子的尺寸：                         */
            System.Console.WriteLine("Length: {0}", myDimensions.Length());
            System.Console.WriteLine("Width: {0}", myDimensions.Width());
        }
    }

    #endregion


    /*******************************************************************************************************/

    #region MultiExplicit


    // 声明英制单位接口：
    internal interface IEnglishDimensions
    {
        float Length();
        float Width();
    }

    // 声明公制单位接口：
    internal interface IMetricDimensions
    {
        float Length();
        float Width();
    }

    // 声明实现以下两个接口的“Box”类：
    // IEnglishDimensions 和 IMetricDimensions：
    internal partial class MultiInterfaceDemoBox : IEnglishDimensions, IMetricDimensions
    {
        private float lengthInches;
        private float widthInches;
        public MultiInterfaceDemoBox(float length, float width)
        {
            lengthInches = length;
            widthInches = width;
        }
        // 显式实现 IEnglishDimensions 的成员：
        float IEnglishDimensions.Length()
        {
            return lengthInches;
        }
        float IEnglishDimensions.Width()
        {
            return widthInches;
        }
        // 显式实现 IMetricDimensions 的成员：
        float IMetricDimensions.Length()
        {
            return lengthInches * 2.54f;
        }
        float IMetricDimensions.Width()
        {
            return widthInches * 2.54f;
        }
        public static void Show()
        {
            // 声明类实例“myBox”：
            MultiInterfaceDemoBox myBox = new MultiInterfaceDemoBox(30.0f, 20.0f);
            // 声明英制单位接口的实例：
            IEnglishDimensions eDimensions = (IEnglishDimensions)myBox;
            // 声明公制单位接口的实例：
            IMetricDimensions mDimensions = (IMetricDimensions)myBox;
            // 以英制单位打印尺寸：
            System.Console.WriteLine("IEnglishDimensions Length(in): {0}", eDimensions.Length());
            System.Console.WriteLine("IEnglishDimensions Width (in): {0}", eDimensions.Width());
            // 以公制单位打印尺寸：
            System.Console.WriteLine("IMetricDimensions Length(cm): {0}", mDimensions.Length());
            System.Console.WriteLine("IMetricDimensions Width (cm): {0}", mDimensions.Width());
        }
    }


    #endregion

}
