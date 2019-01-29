using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;

namespace Samples
{
    public class PythonScript
    {
        #region Python
        public static void Run()
        {
            Console.WriteLine("Loading helloworld.py...");

            ScriptRuntime py = Python.CreateRuntime();
            dynamic helloworld = py.UseFile("PythonDemo\\helloworld.py");

            Console.WriteLine("helloworld.py loaded!");

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(helloworld.welcome("Employee #{0}"), i);
                Console.WriteLine(helloworld.welcome1("Employee #{0}"), i);
            }
        }

        #endregion
    }

}

