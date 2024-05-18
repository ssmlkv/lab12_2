using lab10;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab12__2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyHashTable<Carriage> hashtable = null;
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("1. Создать и заполнить хеш-таблицу");
                Console.WriteLine("2. Поиск элемента в хеш-таблице по ключу");
                Console.WriteLine("3. Удаление элемента из хеш-таблицы по ключу");
                Console.WriteLine("4. Вывести содержимое хеш-таблицы");
                Console.WriteLine("5. Показать, что будет при добавлении элемента, если таблица уже заполнена");
                Console.WriteLine("6. Выход");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("\nВведите количество элементов для добавления в хэш-таблицу:");
                        int numberOfElements;
                        while (!int.TryParse(Console.ReadLine(), out numberOfElements) || numberOfElements <= 0)
                        {
                            Console.WriteLine("\nПожалуйста, введите корректное положительное целое число.");
                        }

                        hashtable = new MyHashTable<Carriage>();

                        for (int i = 0; i < numberOfElements; i++)
                        {
                            Carriage newCarriage = new Carriage();
                            newCarriage.RandomInit();
                            hashtable.AddItem(newCarriage);
                        }
                        break;

                    case "2":
                        if (hashtable == null || hashtable.Count == 0)
                        {
                            Console.WriteLine("\nХэш-таблица не создана. Сначала создайте и заполните таблицу.");
                            break;
                        }

                        Console.WriteLine("\nВведите информацию об объекте для поиска:");

                        Console.Write("\nВведите id: ");
                        int id;
                        while (!int.TryParse(Console.ReadLine(), out id))
                        {
                            Console.WriteLine("\nНекорректный ввод. Пожалуйста, введите целое число.");
                            Console.Write("\nВведите id: ");
                        }

                        Console.Write("\nВведите номер вагона: ");
                        int number;
                        while (!int.TryParse(Console.ReadLine(), out number))
                        {
                            Console.WriteLine("\nНекорректный ввод. Пожалуйста, введите целое число.");
                            Console.Write("\nВведите номер вагона: ");
                        }

                        Console.Write("\nВведите максимальную скорость: ");
                        int maxSpeed;
                        while (!int.TryParse(Console.ReadLine(), out maxSpeed))
                        {
                            Console.WriteLine("\nНекорректный ввод. Пожалуйста, введите целое число.");
                            Console.Write("\nВведите максимальную скорость: ");
                        }

                        Carriage carriageToFind = new Carriage(new IdNumber(id), number, maxSpeed);

                        bool contains = hashtable.Contains(carriageToFind);

                        Console.WriteLine($"\nОбъект {(contains ? "найден" : "не найден")} в хеш-таблице.");
                        break;

                    case "3":
                        if (hashtable == null || hashtable.Count == 0)
                        {
                            Console.WriteLine("\nХэш-таблица не создана или пуста. Сначала создайте и заполните таблицу.");
                            break;
                        }

                        Console.WriteLine("\nВведите информацию об объекте для удаления:");

                        Console.Write("\nВведите id: ");
                        int id1;
                        while (!int.TryParse(Console.ReadLine(), out id1))
                        {
                            Console.WriteLine("\nНекорректный ввод. Пожалуйста, введите целое число.");
                            Console.Write("\nВведите id: ");
                        }

                        Console.Write("\nВведите номер вагона: ");
                        int number1;
                        while (!int.TryParse(Console.ReadLine(), out number1))
                        {
                            Console.WriteLine("\nНекорректный ввод. Пожалуйста, введите целое число.");
                            Console.Write("\nВведите номер вагона: ");
                        }

                        Console.Write("\nВведите максимальную скорость: ");
                        int maxSpeed1;
                        while (!int.TryParse(Console.ReadLine(), out maxSpeed1))
                        {
                            Console.WriteLine("\nНекорректный ввод. Пожалуйста, введите целое число.");
                            Console.Write("\nВведите максимальную скорость: ");
                        }

                        Carriage carriageToRemove = new Carriage(new IdNumber(id1), number1, maxSpeed1);

                        bool removed = hashtable.RemoveData(carriageToRemove);

                        // Если элемент успешно удален, обновляем индексы в таблице
                        if (removed)
                        {
                            hashtable.UpdateIndexesAfterRemoval();
                        }

                        Console.WriteLine($"\nЭлемент {(removed ? "удален" : "не найден в хеш-таблице")}.");
                        break;

                    case "4":
                        if (hashtable == null || hashtable.Count == 0)
                        {
                            Console.WriteLine("\nХэш-таблица не создана. Сначала создайте и заполните таблицу.");
                            break;
                        }

                        Console.WriteLine("\nХэш-таблица:");
                        hashtable.Print();
                        break;

                    case "5":
                        Console.WriteLine("\nДобавление элемента в хеш-таблицу, когда она уже заполнена:");

                        // Создаем новый элемент для добавления
                        Carriage newCarriage2 = new Carriage();
                        newCarriage2.RandomInit();

                        // Пытаемся добавить элемент в таблицу
                        try
                        {
                            hashtable.AddItem(newCarriage2);
                            Console.WriteLine("\nЭлемент успешно добавлен в хеш-таблицу.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"\nОшибка при добавлении элемента в хеш-таблицу: {ex.Message}");
                        }
                        break;

                    case "6":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("\nНекорректный выбор. Попробуйте снова.");
                        break;
                }
            }
        }
    }
}
