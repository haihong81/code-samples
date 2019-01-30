using System;

#region DelegateDemo Compose

namespace Samples
{
    internal delegate void MyDelegate(string s);

    internal partial class DelegateDemo
    {
        public static void Hello(string s)
        {
            Console.WriteLine("  Hello, {0}!", s);
        }

        public static void Goodbye(string s)
        {
            Console.WriteLine("  Goodbye, {0}!", s);
        }

        public static void Compose()
        {
            MyDelegate a, b, c, d;

            // 创建引用 Hello 方法的
            // 委托对象 a：
            a = new MyDelegate(Hello);
            // 创建引用 Goodbye 方法的
            // 委托对象 b：
            b = new MyDelegate(Goodbye);
            // a 和 b 两个委托组成 c，
            // c 将按顺序调用这两个方法：
            c = a + b;
            // 从组合委托中移除 a 而保留 d，
            // 后者仅调用 Goodbye 方法：
            d = c - a;

            Console.WriteLine("Invoking delegate a:");
            a("A");
            Console.WriteLine("Invoking delegate b:");
            b("B");
            Console.WriteLine("Invoking delegate c (c=a+b):");
            c("C");
            Console.WriteLine("Invoking delegate d (d=c-a):");
            d("D");
        }
    }
}

#endregion

/*******************************************************************************************************/

#region DelegateDemo ProcessBookMethods
// 用于处理书店的一组类:
namespace Bookstore
{
    using System.Collections;

    // 描述图书列表中的一本书:
    public struct Book
    {
        public string Title;        // 书名。
        public string Author;       // 作者。
        public decimal Price;       // 价格。
        public bool Paperback;      // 是否为平装本？

        public Book(string title, string author, decimal price, bool paperBack)
        {
            Title = title;
            Author = author;
            Price = price;
            Paperback = paperBack;
        }
    }

    // 声明一个用于处理书的委托类型:
    public delegate void ProcessBookDelegate(Book book);

    // 维护一个图书数据库。
    public class BookDB
    {
        // 列出数据库中的所有图书:
        private ArrayList list = new ArrayList();

        // 向数据库中添加图书:
        public void AddBook(string title, string author, decimal price, bool paperBack)
        {
            list.Add(new Book(title, author, price, paperBack));
        }

        // 对每本平装图书调用传入委托来进行处理:
        public void ProcessPaperbackBooksDelegate(ProcessBookDelegate processBook) //方法1 使用自定义委托类型
        {
            foreach (Book b in list)
            {
                if (b.Paperback)
                    // 调用该委托：
                    processBook(b);
            }
        }

        // 对每本平装图书调用传入委托来进行处理:
        public void ProcessPaperbackBooksAction(Action<Book> processBook) //方法2 使用内置委托类型
        {
            foreach (Book b in list)
            {
                if (b.Paperback)
                    // 调用该委托：
                    processBook(b);
            }
        }
    }
}

// 使用 Bookstore 类：
namespace Samples
{
    using Bookstore;

    // 计算图书总价格和平均价格的类：
    internal class PriceTotaller
    {
        private int countBooks = 0;
        private decimal priceBooks = 0.0m;

        internal void AddBookToTotal(Book book)
        {
            countBooks += 1;
            priceBooks += book.Price;
        }

        internal decimal TotalPrice()
        {
            return priceBooks;
        }

        internal decimal TotalCount()
        {
            return countBooks;
        }

        internal decimal AveragePrice()
        {
            return priceBooks / countBooks;
        }
    }

    // 测尝试书数据库的类：
    internal partial class DelegateDemo
    {
        // 打印书名。
        private static void PrintTitle(Book b)
        {
            Console.WriteLine("   {0}", b.Title);
        }

        // 下面开始执行。
        public static void ProcessBookMethods()
        {
            BookDB bookDB = new BookDB();

            // 用一些书初始化数据库：
            AddBooks(bookDB);

            // 打印所有平装本的书名：
            Console.WriteLine("Paperback Book Titles:");
            // 创建一个与静态方法 Test.PrintTitle 关联的
            // 新委托对象：

            Console.WriteLine("方法1 使用自定义委托类型");
            bookDB.ProcessPaperbackBooksDelegate(new ProcessBookDelegate(PrintTitle));//方法1 使用自定义委托类型

            Console.WriteLine("方法2 使用内置委托类型");
            bookDB.ProcessPaperbackBooksAction(new Action<Book>(PrintTitle));//方法2 使用内置委托类型

            Console.WriteLine("方法3 使用Lambda");
            bookDB.ProcessPaperbackBooksAction(b => Console.WriteLine("   {0}", b.Title));//方法3 使用Lambda

            // 使用 PriceTotaller 对象
            // 获取平装本的平均价格：
            PriceTotaller totaller = new PriceTotaller();
            // 创建一个与对象 totaller 的非静态方法
            // AddBookToTotal 关联的新委托对象：

            Console.WriteLine("方法1 使用自定义委托类型");
            bookDB.ProcessPaperbackBooksDelegate(new ProcessBookDelegate(totaller.AddBookToTotal));//方法1 使用自定义委托类型

            Console.WriteLine("方法2 使用内置委托类型");
            bookDB.ProcessPaperbackBooksAction(new Action<Book>(totaller.AddBookToTotal));//方法2 使用内置委托类型

            Console.WriteLine("方法3 使用Lambda");
            bookDB.ProcessPaperbackBooksAction(new Action<Book>(b => totaller.AddBookToTotal(b)));//方法3 使用Lambda

            Console.WriteLine("Paperback Book TotalPrice: ${0:#.##}", totaller.TotalPrice());
            Console.WriteLine("Paperback Book TotalCount: ${0:#.##}", totaller.TotalCount());
            Console.WriteLine("Paperback Book Average Price: ${0:#.##}", totaller.AveragePrice());
        }

        // 用一些测尝试书初始化图书数据库：
        private static void AddBooks(BookDB bookDB)
        {
            bookDB.AddBook("The C Programming Language",
               "Brian W. Kernighan and Dennis M. Ritchie", 19.95m, true);
            bookDB.AddBook("The Unicode Standard 2.0",
               "The Unicode Consortium", 39.95m, true);
            bookDB.AddBook("The MS-DOS Encyclopedia",
               "Ray Duncan", 129.95m, false);
            bookDB.AddBook("Dogbert's Clues for the Clueless",
               "Scott Adams", 12.00m, true);
        }
    }
}
#endregion