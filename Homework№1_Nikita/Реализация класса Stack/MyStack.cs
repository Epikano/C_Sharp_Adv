using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Реализация_класса_Stack
{
    class MyStack<T> : IEnumerable<T>
    {
        private static int counter = 0;
        T[] persons = new T[counter];

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        public IEnumerator<T> GetEnumerator()
        {
            foreach (T i in persons)
                yield return i;
        }

        public T this[int index]
        {
            get
            {
                return persons[index];
            }
            set
            {
                persons[index] = value;
            }
        }

        public void Push(T p)
        {
            counter++;

            T[] temp = new T[counter];
            for (int i = 0; i < persons.Length; i++)
            {
                temp[i] = persons[i];
            }

            for (int i = counter - 1; i > 0; i--)
            {
                temp[i] = temp[i - 1];
            }
            temp[0] = p;

            persons = (T[])temp.Clone();
        }

        public T Pop()
        {
            T temp = persons[0];
            for (int i = 0; i < counter - 1; i++)
            {
                persons[i] = persons[i + 1];
            }

            counter--;

            T[] temp2 = new T[counter];
            for (int i = 0; i < persons.Length - 1; i++)
            {
                temp2[i] = persons[i];
            }
            persons = (T[])temp2.Clone();

            return temp;
        }

        public T Peek()
        {
            return persons[0];
        }

        public int Count()
        {
            return persons.Length;
        }
    }
}
