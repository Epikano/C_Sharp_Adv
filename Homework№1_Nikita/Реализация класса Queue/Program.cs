using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Реализация_класса_Queue
{
    class Program
    {
        static void Main(string[] args)
        {
            MyQueue<Person> persons = new MyQueue<Person>();

            persons.Enqueue(new Person() { Name = "Tom" });
            persons.Enqueue(new Person() { Name = "Bill" });
            persons.Enqueue(new Person() { Name = "John" });
            persons.Enqueue(new Person() { Name = "Maxim" });
            persons.Enqueue(new Person() { Name = "Nikita" });
            persons.Enqueue(new Person() { Name = "Dima" });

            Console.WriteLine("Очередь состоит из {0} человек: ", persons.Count());
            int u = 1;
            foreach (Person p in persons)
            {
                Console.WriteLine($"{u}.{p.Name}");
                u++;
            }
            Console.WriteLine("-------------------------------------------------------");

            Person px = persons.Dequeue();
            Console.WriteLine($"Первый и изъятый элемент из очереди: {px.Name}");

            Console.WriteLine("\nТеперь очередь состоит из {0} человек: ", persons.Count());
            u = 1;
            foreach (Person p in persons)
            {
                Console.WriteLine($"{u}.{p.Name}");
                u++;
            }
            Console.WriteLine("-------------------------------------------------------");

            Person py = persons.Peek();
            Console.WriteLine($"Первый элемент текущей очереди без его изъятия: {py.Name}");
            Console.WriteLine("-------------------------------------------------------");

            persons.Enqueue(new Person() { Name = "Alexander" });
            Console.WriteLine("Был добавлен новый элемент \"Alexander\" в конец очереди.");

            Console.WriteLine("\nТеперь очередь состоит из {0} человек: ", persons.Count());
            u = 1;
            foreach (Person p in persons)
            {
                Console.WriteLine($"{u}.{p.Name}");
                u++;
            }
            Console.WriteLine("-------------------------------------------------------");

            Console.WriteLine("\nВозможность доставать объект из очереди по индексу:\n");
            Console.WriteLine($"Первый человек в очереди: {persons[0].Name}");
            Console.WriteLine($"Третий человек в очереди: {persons[2].Name}");
            Console.WriteLine("-------------------------------------------------------");

            Console.ReadLine();
        }
    }
}
