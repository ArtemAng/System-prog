using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FiveThreads__1
{
    class Protected
    {
        IList<double> _S = new List<double>();
        IList<double> _R = new List<double>();

        public IList<double> S { get => _S; set => _S = value; }
        public IList<double> R { get => _R; set => _R = value; }

        public void Test()
        {
            var A = new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    S.Add(i);
                    Console.WriteLine("Thread A: Добавлен в S: " + i);
                }
            });
            A.Start();

            var B = new Thread(() =>
            {
                if (S.Count == 0)
                {
                    Console.WriteLine("В списке нет объектов.");
                    Console.WriteLine("Thread B:Sleeped 1000");
                    Thread.Sleep(1000);
                }
                var lastElem = S.Last();
                if (lastElem != 0)
                {
                    R.Add(lastElem * lastElem);
                }
                Console.WriteLine("Thread B:Добавлен в R, последний элемент S " + lastElem * lastElem);
            });
            B.Start();

            var C = new Thread(() =>
            {
                while (S.Count == 0)
                {
                    Console.WriteLine("Thread C: В S  нет элементов");
                    Thread.Sleep(1000);
                    Console.WriteLine("Thread C: Sleeped 1000");
                }
                while (R.Count() == 0)
                {
                    Console.WriteLine("Thread D: B R нет элементов!!!");
                    Console.WriteLine("Thread C: Sleeped 1000");
                }
                var lastElement = R.Last();
                if (lastElement != 0)
                {
                    R.Add(lastElement / 3);
                }
                Console.WriteLine("Thread C: Добавлен в R, последний элемент S " + lastElement / 3);
            });
            C.Start();

            var D = new Thread(() =>
            {
                if (R.Count == 0)
                {
                    Console.WriteLine("Thread D: В R нет элементов!!!");
                    Console.WriteLine("Thread D: Sleeped 1000 ");
                    Thread.Sleep(1000);
                }

                Console.WriteLine("Thread D: print last element R: " + R.Last());
            });
            D.Start();
        }
    }
}
