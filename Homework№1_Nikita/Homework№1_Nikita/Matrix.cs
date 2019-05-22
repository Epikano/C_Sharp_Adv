using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_1_Nikita
{
    class Matrix<T> : IMatrix<T>
    {
        public int Side { get; }

        public T[,] matrix;
        public Matrix(int side)
        {
            Side = side;
            matrix = new T[Side, Side];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in matrix)
            {
                yield return item;
            }
        }

        public void Show()
        {
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Вывод матрицы:");
            for (int i = 0; i < Side; i++)
            {
                for (int j = 0; j < Side; j++)
                {
                    Console.Write(matrix[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("-------------------------------------------");
        }

        public void Insert(int height, int width, T element)
        {
            matrix[height, width] = element;
        }

        public T Take(int height, int width)
        {
            if (height < Side && width < Side)
            {
                return matrix[height, width];
            }
            else
            {
                throw new Exception($"Индексы выходят за границу допустимых значений. Допустимый диапазон: от 0 до {Side - 1}");
            }
        }
    }
}
