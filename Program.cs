using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FiveThreads__1
{
    class Program
    {
        
        static void Main(string[] args)
        {
            while (true) 
            {
                Console.Clear();
                Console.WriteLine("Enter number for selection:");
                Console.WriteLine("1 - Insecure code: no critical sections!!!!");
                Console.WriteLine("2 - Secure code: with critical sections!!!!");
                Console.WriteLine("3 - Secure code: lock critical sections!!!!");
                int _enter = 0;
                try
                {
                    Console.WriteLine("Enter key:");
                    _enter = Int32.Parse(Console.ReadLine());
                }
                catch(FormatException e) 
                {
                    Console.WriteLine(e.Message);
                }
                if (_enter > 0 && _enter < 4) 
                {
                    switch (_enter)
                    {
                        case 1:
                            {
                                var insecure = new Protected();
                                insecure.Test();
                                Console.ReadKey();
                                break;
                            }
                        case 2:
                            {
                                var secure = new UnProtected();
                                secure.Test();
                                Console.ReadKey();
                        break;
                            }
                        case 3:
                            {
                                var secure = new UnProtected();
                                secure.TestLock();
                                Console.ReadKey();
                                break;
                            }
                        default:
                            break;
                    }
                }
                Console.WriteLine();
            }
        }
    }
}




/*
 * Спровоцировать и пронаблюдать проблемы, возникающие в многопоточных 
 * программах при отсутствии синхронизации потоков или неправильном 
 * её выполнении. 
 * 1. Создать программу из пяти потоков — основного, А, B, C и D:
 * Основной поток запускает остальные четыре и ожидает их завершения.
 * Поток A добавляет в список S числа 1, 2, 3 и т. д. Поток B извлекает 
 * из списка S последний элемент, возводит его в квадрат и помещает 
 * в список R. Если в списке S нет элементов, поток B ожидает 
 * одну секунду функцией Sleep(). 
 * Поток C извлекает из списка S последний элемент, делит его на 3 
 * и помещает в список R. Если в списке S нет элементов, поток C 
 * ожидает одну секунду.
 * Поток D извлекает из списка R последний элемент и печатает его. 
 * Если в списке R нет элементов, поток D печатает сообщение 
 * об этом и ожидает одну секунду.
 * Синхронизацию потоков производить на данном этапе не нужно. 
 * Запустите программу несколько раз, проанализируйте проблему 
 * (Стабильной работы программы не ожидается.)
 */


/*
* 2. Обеспечить корректную синхронизацию потоков.
* Каждое обращение к спискам S и R из любого потока защитить 
* критической областью (одна КО для списка S, другая — для списка R). 
* 3. Спровоцировать взаимоблокировку вследствие состязания. 
* 3.1 Изменить алгоритм потока B на следующий:
* Вход в КО для списка S, вход в КО для списка R;
* Извлечение элемента из S, вычисление, добавление элемента в R; 
* Выход из КО для списка R, выход из КО для списка S.
* В целях наглядности можно между обращениями к КО в обоих 
* потоках вставить задержки функцией Sleep(). Так можно 
* проверить сценарии с разным порядком входа и выхода из КО.
* 
* Ожидается «зависание» потоков A, B и С, что будет проявляться 
* в постоянно пустом списке R 
*/
