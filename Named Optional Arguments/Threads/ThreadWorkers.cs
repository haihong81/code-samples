using System;
using System.Threading;

namespace Samples
{
    public class Worker
    {
        // 启动线程时调用此方法。
        public void DoWork()
        {
            while (!_shouldStop)
            {
                Console.WriteLine("worker thread: working...");
                Thread.Sleep(1);
            }
            Console.WriteLine("worker thread: terminating gracefully.");
        }

        public void RequestStop()
        {
            _shouldStop = true;
        }

        // Volatile 用于向编译器提示此数据
        // 成员将由多个线程访问。
        private volatile bool _shouldStop;
    }

    public partial class ThreadDemo
    {
        public static void RunWorkers()
        {
            // 创建线程对象。这不会启动该线程。
            Worker workerObject = new Worker();
            Thread workerThread = new Thread(workerObject.DoWork);
            // 启动辅助线程。
            workerThread.Start();
            Console.WriteLine("main thread: Starting worker thread...");

            // 循环直至辅助线程激活。
            while (!workerThread.IsAlive) ;

            // 为主线程设置 1 毫秒的休眠，
            // 以使辅助线程完成某项工作。
            Thread.Sleep(5);

            // 请求辅助线程自行停止：
            workerObject.RequestStop();

            // 使用 Join 方法阻塞当前线程，
            // 直至对象的线程终止。
            workerThread.Join();
            Console.WriteLine("main thread: Worker thread has terminated.");
        }
    }
}