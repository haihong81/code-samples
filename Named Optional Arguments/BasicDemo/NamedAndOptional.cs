using System;

namespace Samples
{
    public class NamedAndOptional
    {
        // 具有命名参数和可选参数的方法
        public static void Search(string name, int age = 21, string city = "Pueblo")
        {
            Console.WriteLine("Name = {0} - Age = {1} - City = {2}", name, age, city);
        }

        public static void Show()
        {
            // 标准调用
            Search("Sue", 22, "New York");

            // 省略 city 参数
            Search("Mark", 23);

            // 显式命名 city 参数
            Search("Lucy", city: "Cairo");

            // 以相反顺序使用命名参数
            Search("Pedro", age: 45, city: "Saigon");
        }
    }
}