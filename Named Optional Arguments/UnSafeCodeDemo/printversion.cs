using System;
using System.Runtime.InteropServices;

namespace Samples
{
    public class Win32Imports
    {
        [DllImport("version.dll")]
        public static extern bool GetFileVersionInfo(string sFileName,
            int handle, int size, byte[] infoBuffer);

        [DllImport("version.dll")]
        public static extern int GetFileVersionInfoSize(string sFileName,
            out int handle);

        // 自动将第三个参数“out string pValue”从 Ansi
        // 封送处理为 Unicode：
        [DllImport("version.dll")]
        public static extern unsafe bool VerQueryValue(byte[] pBlock,
            string pSubBlock, out string pValue, out uint len);

        // 此 VerQueryValue 重载被标记为“unsafe”，因为
        // 它使用 short*：
        [DllImport("version.dll")]
        public static extern unsafe bool VerQueryValue(byte[] pBlock,
            string pSubBlock, out short* pValue, out uint len);
    }

    public partial class UnsafeCode
    {
        // Main 被标记为“unsafe”，因为它使用指针：
        public static unsafe void PrintVersion()
        {

            var assemblyname = "Named Optional Arguments.exe";
            try
            {
                int handle = 0;
                // 确定有多少版本信息：
                int size =
                    Win32Imports.GetFileVersionInfoSize(assemblyname,
                    out handle);

                if (size == 0) return;

                byte[] buffer = new byte[size];

                if (!Win32Imports.GetFileVersionInfo(assemblyname, handle, size, buffer))
                {
                    Console.WriteLine("Failed to query file version information.");
                    return;
                }

                short* subBlock = null;
                uint len = 0;
                // 从版本信息获取区域设置信息：
                if (!Win32Imports.VerQueryValue(buffer, @"\VarFileInfo\Translation", out subBlock, out len))
                {
                    Console.WriteLine("Failed to query version information.");
                    return;
                }

                string spv = @"\StringFileInfo\" + subBlock[0].ToString("X4") + subBlock[1].ToString("X4") + @"\ProductVersion";
                //string spv = @"\StringFileInfo\" + subBlock[0].ToString("X4") + @"\ProductVersion";

                byte* pVersion = null;
                // 获取此程序的 ProductVersion 值:
                string versionInfo="";

                if (!Win32Imports.VerQueryValue(buffer, spv, out versionInfo, out len))
                {
                    Console.WriteLine("Failed to query version information.");
                    return;
                }

                Console.WriteLine("ProductVersion == {0}", versionInfo);
            }
            catch (Exception e)
            {
                Console.WriteLine("Caught unexpected exception " + e.Message);
            }

            return;
        }
    }
}