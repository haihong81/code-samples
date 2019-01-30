using System;
using System.IO;

// 将大文件当作字节数组
// 访问的类。
public class FileByteArray
{
    private Stream stream;      // 包含用于访问

    // 该文件的基础流。
    // 创建封装特定文件的新 FileByteArray。
    public FileByteArray(string fileName)
    {
        stream = new FileStream(fileName, FileMode.Open);
    }

    // 关闭流。这应是
    // 结束前的最后一个操作。
    public void Close()
    {
        stream.Close();
        stream = null;
    }

    // 提供对文件的读/写访问的索引器。
    public byte this[long index]   // long 是 64 位整数
    {
        // 在偏移量 index 处读取一个字节，然后将其返回。
        get
        {
            byte[] buffer = new byte[1];
            stream.Seek(index, SeekOrigin.Begin);
            stream.Read(buffer, 0, 1);
            return buffer[0];
        }
        // 在偏移量 index 处写入一个字节，然后将其返回。
        set
        {
            byte[] buffer = new byte[1] { value };
            stream.Seek(index, SeekOrigin.Begin);
            stream.Write(buffer, 0, 1);
        }
    }

    // 获取文件的总长度。
    public long Length
    {
        get
        {
            return stream.Seek(0, SeekOrigin.End);
        }
    }
}

// 演示 FileByteArray 类。
// 反转文件中的字节。
public class IndexDemo
{
    public static void Reverse()
    {
        string filepath = "TEST.TXT";

        if (!File.Exists(filepath))
        {
            File.Create(filepath).Close();
            File.AppendAllText(filepath, "File Content To Display!");
        }

        Console.WriteLine(File.ReadAllText(filepath));

        FileByteArray file = new FileByteArray(filepath);
        long len = file.Length;

        // 交换文件中的字节以对其进行反转。
        for (long i = 0; i < len / 2; ++i)
        {
            byte t;

            // 请注意，为“file”变量建立索引会调用
            //  FileByteStream 类上的索引器，该索引器在文件中读取
            // 和写入字节。
            t = file[i];
            file[i] = file[len - i - 1];
            file[len - i - 1] = t;
        }
        file.Close();
        Console.WriteLine(File.ReadAllText(filepath));
    }
}