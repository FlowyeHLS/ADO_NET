using System.Runtime.InteropServices;

namespace LINQ
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AllocConsole();
            //1создаем источник данных
            int[] arr = { 3, 5, 8, 13, 21, 34, 55 };
            //2)определяем query expression
            IEnumerable<int> FibonacciQuery =
                from i in arr
                where i > 20
                orderby i descending
                select i;
            //3)выполнение запроса 
            foreach (int i in FibonacciQuery)
                Console.Write($"{i}\t");
            Console.WriteLine();

            ///////////////////////////////////////////////////////////////////////////
            //Immidiate queries: 
            Console.WriteLine((from i in arr select i).Count());
            Console.WriteLine((from i in arr select i).Sum());
            //List<int> i_list = (from i in arr select i).To
        }
        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();
        [DllImport("kernel32.dll")]
        public static extern bool FreeConsole();
    }
}
