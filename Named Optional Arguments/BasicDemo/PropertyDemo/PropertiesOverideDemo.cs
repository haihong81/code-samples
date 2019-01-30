namespace Samples
{
    public partial class PropertiesDemo
    {
        public static void PropertiesOverideDemo()
        {
            Shape[] shapes =
               {
            new Square(5, "Square #1"),
            new Circle(3, "Circle #1"),
            new Rectangle( 4, 5, "Rectangle #1")
         };

            System.Console.WriteLine("Shapes Collection");
            foreach (Shape s in shapes)
            {
                System.Console.WriteLine(s);
            }
        }
    }

    public abstract class Shape
    {
        private string myId;

        public Shape(string s)
        {
            Id = s;   // 调用 Id 属性的 set 访问器
        }

        public string Id
        {
            get
            {
                return myId;
            }

            set
            {
                myId = value;
            }
        }

        // Area 为只读属性 - 只需要 get 访问器：
        public abstract double Area
        {
            get;
        }

        public override string ToString()
        {
            return Id + " Area = " + string.Format("{0:F2}", Area);
        }
    }

    public class Square : Shape
    {
        private int mySide;

        public Square(int side, string id) : base(id)
        {
            mySide = side;
        }

        public override double Area
        {
            get
            {
                // 已知边长，返回正方形的面积：
                return mySide * mySide;
            }
        }
    }

    public class Circle : Shape
    {
        private int myRadius;

        public Circle(int radius, string id) : base(id)
        {
            myRadius = radius;
        }

        public override double Area
        {
            get
            {
                // 已知半径，返回圆的面积：
                return myRadius * myRadius * System.Math.PI;
            }
        }
    }

    public class Rectangle : Shape
    {
        private int myWidth;
        private int myHeight;

        public Rectangle(int width, int height, string id) : base(id)
        {
            myWidth = width;
            myHeight = height;
        }

        public override double Area
        {
            get
            {
                // 已知宽度和高度，返回矩形的面积：
                return myWidth * myHeight;
            }
        }
    }
}