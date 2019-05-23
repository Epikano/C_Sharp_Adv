using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_1_Nikita
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Укажите длину стороны матрицы:");
            int x = Convert.ToInt32(Console.ReadLine());

            Matrix<int> square = new Matrix<int>(x);

            for (int i = 0; i < square.Side; i++)
            {
                for (int j = 0; j < square.Side; j++)
                {
                    int random = new Random(Guid.NewGuid().GetHashCode()).Next(0, 10);
                    square.matrix[i, j] = random;
                }
            }
            square.Show();

            int y = square.Take(height: 1, width: 2);
            Console.WriteLine($"Вывод значения в указанном месте матрицы: {y}");
            Console.WriteLine("-------------------------------------------");

            square.Insert(height: 1, width: 2, element: 1);
            Console.WriteLine("\n  <Новая матрица, с учетом метода Insert>");
            square.Show();

            Console.WriteLine("\nРеализация интерфейса IEnumerable с \nпоследующим выводом переборного массива:");
            foreach (int i in square)
                Console.Write(i + " ");
            Console.WriteLine("\n-------------------------------------------");
    
            Console.ReadLine();
        }
    }
}
