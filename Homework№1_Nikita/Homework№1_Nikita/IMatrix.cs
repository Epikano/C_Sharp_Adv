using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_1_Nikita
{
    interface IMatrix<T> : IEnumerable<T>
    {
        int Side { get; }

        void Show();
        void Insert(int height, int width, T element);
        T Take(int height, int width);
    }
}
