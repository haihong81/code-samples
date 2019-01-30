using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Samples
{
    internal class FileReader
    {
        private const uint GENERIC_READ = 0x80000000;
        private const uint OPEN_EXISTING = 3;
        private IntPtr handle;

        [DllImport("kernel32", SetLastError = true)]
        private static extern unsafe IntPtr CreateFile(
            string FileName,                // 文件名
            uint DesiredAccess,             // 访问模式
            uint ShareMode,                 // 共享模式
            uint SecurityAttributes,        // 安全特性
            uint CreationDisposition,       // 如何创建
            uint FlagsAndAttributes,        // 文件特性
            int hTemplateFile               // 模板文件的句柄
            );

        [DllImport("kernel32", SetLastError = true)]
        private static extern unsafe bool ReadFile(
            IntPtr hFile,                   // 文件句柄
            void* pBuffer,              // 数据缓冲区
            int NumberOfBytesToRead,    // 要读取的字节数
            int* pNumberOfBytesRead,        // 已读取的字节数
            int Overlapped              // 重叠缓冲区
            );

        [DllImport("kernel32", SetLastError = true)]
        private static extern unsafe bool CloseHandle(
            IntPtr hObject   // 对象句柄
            );

        public bool Open(string FileName)
        {
            // 打开现有文件进行读取

            handle = CreateFile(
                FileName,
                GENERIC_READ,
                0,
                0,
                OPEN_EXISTING,
                0,
                0);

            if (handle != IntPtr.Zero)
                return true;
            else
                return false;
        }

        public unsafe int Read(byte[] buffer, int index, int count)
        {
            int n = 0;
            fixed (byte* p = buffer)
            {
                if (!ReadFile(handle, p + index, count, &n, 0))
                    return 0;
            }
            return n;
        }

        public bool Close()
        {
            // 关闭文件句柄
            return CloseHandle(handle);
        }
    }

    public partial class UnsafeCode
    {
        public static void ReadFile()
        {
            string filepath = "TEST.TXT";
            File.Create(filepath).Close();
            File.AppendAllText(filepath, "File Content To Display!");
            bool getfile = false;
            while (!getfile)
            {
                if (!System.IO.File.Exists(filepath))
                {
                    Console.WriteLine("File " + filepath + " not found.");
                }
                else
                {
                    getfile = true;
                }
            }

            byte[] buffer = new byte[128];
            FileReader fr = new FileReader();

            if (fr.Open(filepath))
            {
                // 假定正在读取 ASCII 文件
                ASCIIEncoding Encoding = new ASCIIEncoding();

                int bytesRead;
                do
                {
                    bytesRead = fr.Read(buffer, 0, buffer.Length);
                    string content = Encoding.GetString(buffer, 0, bytesRead);
                    Console.Write("{0}", content);
                }
                while (bytesRead > 0);

                fr.Close();
            }
            else
            {
                Console.WriteLine("Failed to open requested file");
            }
        }
    }
}