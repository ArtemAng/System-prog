using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FiveThreads__1
{
    class UnProtected
    {
        IList<double> _S = new List<double>();
        IList<double> _R = new List<double>();
        private static object _tlocker = new object();
        private static object _tlocker2 = new object();


        public IList<double> S { get => _S; set => _S = value; }
        public IList<double> R { get => _R; set => _R = value; }


        public void Test()
        {
            var A = new Thread(() =>
            {
                lock (_tlocker)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        S.Add(i);
                        Console.WriteLine("Thread A: добавлен в S: " + i);
                    }
                }
            });
            A.Start();

            var B = new Thread(() =>
            {
                double last = 0;
                Thread.Sleep(2000);
                lock (_tlocker)
                {
                    while (S.Count == 0)
                    {
                        Console.WriteLine("Thread B: Нет эллементов в S");
                        Console.WriteLine("Thread B: Sleeped 1000");
                        Thread.Sleep(1000);
                    }
                    last = S.Last() * S.Last();
                }

                lock (_tlocker2)
                {
                    while (R.Count() == 0)
                    {
                        Console.WriteLine("Thread B: Нет элементов в R");
                        Thread.Sleep(1000);
                        Console.WriteLine("Thread B: Sleeped 1000");
                    }
                    R.Add(last);
                    Console.WriteLine("Thread B: Добавлен в R Последний в S " + last);
                }
            });
            B.Start();

            var C = new Thread(() =>
            {
                double last = 0;
                lock (_tlocker)
                {
                    if (S.Count == 0)
                    {
                        Console.WriteLine("Thread C:Нет элементов в S");
                        Thread.Sleep(2000);
                        Console.WriteLine("Thread C: Sleep(1000)");
                    }
                    last = S.Last();
                }

                lock (_tlocker2)
                {
                    if (R.Count() == 0)
                    {
                        Console.WriteLine("Thread C:Нет элементов в R!!!");
                        Thread.Sleep(1000);
                        Console.WriteLine("Thread C: Sleep(1000)");
                    }
                    R.Add(last / 3);
                    Console.WriteLine("Thread C: Add in ListR last element ListS " + last / 3);
                }
            });
            C.Start();
            C.Join();

            var D = new Thread(() =>
            {
                lock (_tlocker2)
                {
                    while (R.Count == 0)
                    {
                        Console.WriteLine("Thread D: Нет элементов в R!!!");
                        Console.WriteLine("Thread D: Sleeped 1000");
                        Thread.Sleep(1000);
                    }
                    Console.WriteLine("Thread D: Вывести последний элемент R: " + R.Last());
                }
            });
            D.Start();
        }

        public void TestLock() 
        {
            var A = new Thread(() =>
            {
                lock (_tlocker)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        S.Add(i);
                        Console.WriteLine("Thread A: Add in ListS: " + i);
                    }
                }
                Console.WriteLine("Thread A fread lock();");
            });
            A.Start();

            var B = new Thread(() =>
            {
                double last = 0;
                Thread.Sleep(2000);
                lock (_tlocker)
                {
                    while (S.Count == 0)
                    {
                        Console.WriteLine("Thread B:Нет элементов S!!!");
                        Console.WriteLine("Thread B: Sleeped 1000");
                        Thread.Sleep(1000);
                    }
                    last = S.Last() * S.Last();
                }

                lock (_tlocker2)
                {
                    while (R.Count() == 0)
                    {
                        Console.WriteLine("Thread B: Нет элементов в R!!!");
                        Thread.Sleep(1000);
                        Console.WriteLine("Thread B: Sleeped1000");
                    }
                    R.Add(last);
                    Console.WriteLine("Thread B:Добавлен в R" + last);
                }
            });
            B.Start();

            var C = new Thread(() =>
            {
                double last = 0;
                lock (_tlocker)
                {
                    if (S.Count == 0)
                    {
                        Console.WriteLine("Thread C: Нет элементов в S!!!");
                        Thread.Sleep(2000);
                        Console.WriteLine("Thread C: Sleeped 2000");
                    }
                    last = S.Last();
                }

                lock (_tlocker2)
                {
                    if (R.Count() == 0)
                    {
                        Console.WriteLine("Thread C: Нет элементов в R!!!");
                        Thread.Sleep(1000);
                        Console.WriteLine("Thread C: Sleeped 000");
                    }
                    R.Add(last / 3);
                    Console.WriteLine("Thread C: Add in ListR last element ListS " + last / 3);
                }
            });
            C.Start();

            var D = new Thread(() =>
            {
                lock (_tlocker2)
                {
                    while (R.Count == 0)
                    {
                        Console.WriteLine("Thread D: No element in ListR!!!");
                        Console.WriteLine("Thread D: Sleep(1000)");
                        Thread.Sleep(1000);
                    }
                    Console.WriteLine("Thread D: print last element ListR: " + R.Last());
                }
            });
            D.Start();
            Console.ReadKey();
            D.Abort();
        }
    }
}





