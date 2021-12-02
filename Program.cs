using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab_22
{
    /*
     * Сформировать массив случайных целых чисел (размер  задается пользователем). 
     * Вычислить сумму чисел массива и максимальное число в массиве.  
     * Реализовать  решение  задачи  с  использованием  механизма  задач продолжения.
     */
    class Program
    {
        static int n;
        static int[] masInt;

        static void RandomGeneral()
        {
            masInt = new int[n];
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                masInt[i] = random.Next(-n, n);
            }
        }
        static int SummMass(Task task)
        {
            int res = 0;
            //Цикл сделан для того чтобы сделать задержку на каждом этапе перебора
            for (int i = 0; i < n; i++)
            {
                res += masInt[i];
                Thread.Sleep(10); //Для задержки выполнения операции
            }
            return res;
        }

        // Для примера сделал без вывода с целью определения, что раньше закончится (ради эксперимента)
        //В Main закоментировал строки изначального кода с выводом результата по этому методу
        static void MaxNum(Task task)
        {
            int res = 0;
            //Цикл сделан для того чтобы сделать задержку на каждом этапе перебора
            for (int i = 0; i < n; i++)
            {
                if (res < masInt[i])
                {
                    res = masInt[i];
                }
                Thread.Sleep(20); //Для задержки выполнения операции
            }
            Console.WriteLine("Максимальное число последовательности {0}", res);
            // return res;
        }
        static void Main(string[] args)
        {
            n = InPutData.EnterDataInt("Введите размер массива случайных чисел: ");

            Action action = new Action(RandomGeneral);
            Task task = new Task(action);

            Func<Task, int> func1 = new Func<Task, int>(SummMass);
            // Func<Task, int> func2 = new Func<Task, int>(MaxNum);
            Action<Task> action1 = new Action<Task>(MaxNum);

            //т.к. оба метода должны запускатся только после генерации массива случайных чиесел
            Task<int> task1 = task.ContinueWith<int>(func1);
            //Task<int> task2 = task.ContinueWith<int>(func2);
            Task task2 = task.ContinueWith(action1);

            task.Start();

            //Console.WriteLine("Максимальное число последовательности {0}, сумма элементов последовательностей {1}", task2.Result, task1.Result);
            Console.WriteLine("Cумма элементов последовательностей {0}", task1.Result);

            //для проверки
            //foreach (var item in masInt)
            //{
            //    Console.WriteLine(item);
            //}
            Console.ReadKey();
        }
    }

    //Не стал использовать ссылки на предыдущие задачи, на данный этап просто скопировал. В реале использовал ссылку на подобный код ввода
    public static class InPutData
    {
        static public string EnterDataStr(string str)
        {
            string res = "";
            Console.Write(str);
            res = Console.ReadLine();
            return res;
        }
        static public int EnterDataInt(string str)
        {
            int res = 0;
            Console.Write(str);
            try
            {
                res = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! {0}", ex.Message);
                res = EnterDataInt("Попробуйте ввсти еще раз: ");
            }
            return res;
        }
        static public double EnterDataDouble(string str)
        {
            double res = 0;
            Console.Write(str);
            try
            {
                res = Convert.ToDouble(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! {0}", ex.Message);
                res = EnterDataDouble("Попробуйте ввсти еще раз: ");
            }
            return res;
        }
    }
}
