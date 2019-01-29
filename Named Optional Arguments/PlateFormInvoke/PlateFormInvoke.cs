using System;
using System.Runtime.InteropServices;

namespace Samples
{
    internal class PlatformInvokeDemo
    {
        [DllImport("msvcrt.dll")]
        public static extern int puts([MarshalAs(UnmanagedType.LPStr)]string m);

        [DllImport("msvcrt.dll")]
        internal static extern int _flushall();

        public static void Run()
        {
            /*托管调试助手 "PInvokeStackImbalance":
             * “对 PInvoke 函数“Samples!Samples.PlatformInvokeDemo::puts”的调用导致堆栈不对称。
             * 原因可能是托管的 PInvoke 签名与非托管的目标签名不匹配。
             * 请检查 PInvoke 签名的调用约定和参数与非托管的目标签名是否匹配。”*/
            puts("Hello World!");
            _flushall();
        }
    }

    internal class PlatformInvokeDemo1
    {
        [DllImport("msvcrt.dll")]
        public static extern int puts(string c);

        [DllImport("msvcrt.dll")]
        internal static extern int _flushall();

        public static void Run()
        {
            puts("Test");
            _flushall();
        }
    }

    internal class PlatformInvokeDemo2
    {
        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr CreateFontIndirect(
            [In, MarshalAs(UnmanagedType.LPStruct)]
        LOGFONT lplf   // 特征
            );

        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(
            IntPtr handle
            );

        public static void Run()
        {
            LOGFONT lf = new LOGFONT();
            lf.lfHeight = 9;
            lf.lfFaceName = "Arial";
            IntPtr handle = CreateFontIndirect(lf);

            if (IntPtr.Zero == handle)
            {
                Console.WriteLine("Can't creates a logical font.");
            }
            else
            {
                if (IntPtr.Size == 4)
                    Console.WriteLine("{0:X}", handle.ToInt32());
                else
                    Console.WriteLine("{0:X}", handle.ToInt64());

                // 删除所创建的逻辑字体。
                if (!DeleteObject(handle))
                    Console.WriteLine("Can't delete the logical font");
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public class LOGFONT
    {
        public const int LF_FACESIZE = 32;
        public int lfHeight;
        public int lfWidth;
        public int lfEscapement;
        public int lfOrientation;
        public int lfWeight;
        public byte lfItalic;
        public byte lfUnderline;
        public byte lfStrikeOut;
        public byte lfCharSet;
        public byte lfOutPrecision;
        public byte lfClipPrecision;
        public byte lfQuality;
        public byte lfPitchAndFamily;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = LF_FACESIZE)]
        public string lfFaceName;
    }
}