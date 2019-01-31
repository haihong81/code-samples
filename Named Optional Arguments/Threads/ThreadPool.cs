using System;
using System.Threading;

namespace Samples
{
    // Fibonacci 类为使用辅助
    // 线程执行长时间的 Fibonacci(N) 计算提供了一个接口。
    // N 是为 Fibonacci 构造函数提供的，此外还提供了
    // 操作完成时对象发出的事件信号。
    // 然后，可以使用 FibOfN 属性来检索结果。

    public class Fibonacci
    {
        public Fibonacci(int n, ManualResetEvent doneEvent)
        {
            _n = n;
            _doneEvent = doneEvent;
        }

        // 供线程池使用的包装方法。
        public void ThreadPoolCallback(Object threadContext)
        {
            int threadIndex = (int)threadContext;
            Console.WriteLine("thread {0} started...", threadIndex);
            _fibOfN = Calculate(_n);
            Console.WriteLine("thread {0} result calculated...", threadIndex);
            _doneEvent.Set();
        }

        // 计算第 N 个斐波纳契数的递归方法。
        public int Calculate(int n)
        {
            int pre1 = 1;
            int pre2 = 0;
            int result = 0;
            if (n <= 1)
            {
                return n;
            }
            else
            {
                //递归方法
                //return Calculate(n - 1) + Calculate(n - 2);

                //循环方法 更好
                for (int i = 1; i <= n; i++)
                {
                    result = pre1 + pre2;
                    pre1 = pre2;
                    pre2 = result;
                }
                return result;
            }
        }

        public int N { get { return _n; } }
        private int _n;

        public int FibOfN { get { return _fibOfN; } }
        private int _fibOfN;
        private ManualResetEvent _doneEvent;
    }

    public partial class ThreadDemo
    {
        public static void RunPool()
        {
            const int FibonacciCalculations = 10;

            // 每个 Fibonacci 对象使用一个事件
            ManualResetEvent[] doneEvents = new ManualResetEvent[FibonacciCalculations];
            Fibonacci[] fibArray = new Fibonacci[FibonacciCalculations];
            //Random r = new Random();

            // 使用 ThreadPool 配置和启动线程：
            Console.WriteLine("launching {0} tasks...", FibonacciCalculations);
            for (int i = 0; i < FibonacciCalculations; i++)
            {
                doneEvents[i] = new ManualResetEvent(false);
                Fibonacci f = new Fibonacci(i + 1, doneEvents[i]);
                fibArray[i] = f;
                ThreadPool.QueueUserWorkItem(f.ThreadPoolCallback, i);
            }

            // 等待池中的所有线程执行计算...
            WaitHandle.WaitAll(doneEvents);
            Console.WriteLine("Calculations complete.");

            //Console.WriteLine("1,1,2,3,5,8,13,21,34,55");
            // 显示结果...
            for (int i = 0; i < FibonacciCalculations; i++)
            {
                Fibonacci f = fibArray[i];
                Console.WriteLine("Fibonacci({0}) = {1}", f.N, f.FibOfN);
            }
        }
    }
}