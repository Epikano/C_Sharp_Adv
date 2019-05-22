using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Реализация_класса_Stack
{
    class Program
    {
        static void Main(string[] args)
        {
            MyStack<Person> persons = new MyStack<Person>();
            persons.Push(new Person() { Name = "Tom" });
            persons.Push(new Person() { Name = "Bill" });
            persons.Push(new Person() { Name = "John" });
            persons.Push(new Person() { Name = "Maxim" });
            persons.Push(new Person() { Name = "Nikita" });
            persons.Push(new Person() { Name = "Dima" });

            Console.WriteLine("Стек состоит из {0} человек: ", persons.Count());
            int u = 1;
            foreach (Person p in persons)
            {
                Console.WriteLine($"{u}.{p.Name}");
                u++;
            }
            Console.WriteLine("--------------------------------------------------------------");

            Person p1 = persons.Pop();
            Console.WriteLine($"Первый и изъятый элемент из стека: {p1.Name}");

            Console.WriteLine("\nТеперь стек состоит из {0} человек: ", persons.Count());
            u = 1;
            foreach (Person p in persons)
            {
                Console.WriteLine($"{u}.{p.Name}");
                u++;
            }
            Console.WriteLine("--------------------------------------------------------------");

            Person p2 = persons.Peek();
            Console.WriteLine($"Первый элемент текущего стека без его изъятия: {p2.Name}");
            Console.WriteLine("--------------------------------------------------------------");

            persons.Push(new Person() { Name = "Alexander" });
            Console.WriteLine("Был добавлен новый элемент \"Alexander\" в стек на первое место.");

            Console.WriteLine("\nТеперь стек состоит из {0} человек: ", persons.Count());
            u = 1;
            foreach (Person p in persons)
            {
                Console.WriteLine($"{u}.{p.Name}");
                u++;
            }
            Console.WriteLine("--------------------------------------------------------------");

            Console.WriteLine("\nВозможность доставать объект из стека по индексу:\n");
            Console.WriteLine($"Первый человек в очереди: {persons[0].Name}");
            Console.WriteLine($"Третий человек в очереди: {persons[2].Name}");
            Console.WriteLine("-------------------------------------------------------");

            Console.ReadLine();
        }
    }
}
