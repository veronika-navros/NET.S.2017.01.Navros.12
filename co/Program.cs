using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace co
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<int> queue = new Queue<int>(new[] { 1, 2, 3, 4, 5, 6 });
            queue.Enqueue(3);

            string result = string.Empty;
            foreach (var el in queue)
            {
                result += el;
            }
            Console.Write(result);
            Console.ReadKey();
        }
    }
}
