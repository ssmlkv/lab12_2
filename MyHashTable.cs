using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab10;
using lab12__2;

namespace lab12__2
{
    public class MyHashTable<T> where T : IInit, ICloneable, new()
    {
        public T[] table;
        int count = 0;
        double FillRatio; //коэф. заполняемости таблицы

        public int Capacity => table.Length; //емкость, кол-во выделенной памяти
        public int Count => count;


        public MyHashTable(int size = 10, double ratio = 0.72)
        {
            table = new T[size];
            this.FillRatio = ratio;
        }

        public bool Contains(T data)
        {
            return (FindItem(data) >= 0);
        }

        public bool RemoveData(T data)
        {
            bool remove = false; // удалён ли элемент
            int index = FindItem(data);

            while (index >= 0)
            {
                count--;
                remove = true;
                table[index] = default;
                index = FindItem(data);
            }
            return remove;
        }

        public void Print()
        {
            int i = 0;
            foreach (T item in table)
            {
                Console.WriteLine($"{i} : {item}");
                i++;
            }
        }

        public void AddItem(T item)
        {
            if ((double)Count / Capacity > FillRatio)
            {
                T[] temp = (T[])table.Clone();
                table = new T[temp.Length * 2];
                count = 0;
                for (int i = 0; i < temp.Length; i++)
                    AddData(temp[i]);
            }
            AddData(item);
        }

        int GetIndex(T data) => Math.Abs(data.GetHashCode()) % Capacity;

        public void AddData(T data)
        {
            if (data == null) return;
            int index = GetIndex(data);
            int current = index;

            if (Contains(data))
            {
                throw new Exception("Такой элемент уже есть");
            }

            if (table[index] != null)
            {
                while (current < table.Length && table[current] != null)
                    current++;
                if (current == table.Length) 
                {
                    current = 0;
                    while (current < index && table[current] != null)
                        current++;
                    if (current == index)
                        throw new Exception("В таблице нет свободных мест!");
                }
            }
            table[current] = data;
            count++;
        }

        public int FindItem(T data)
        {
            int index = GetIndex(data);

            if (table[index] != null && table[index].Equals(data))
            {
                return index;
            }
            if (table[index] == null)
            {
                for (int i = 1; i < table.Length; i++)
                {
                    int currentIndex = (index + i) % Capacity;

                    if (table[currentIndex] == null)
                    {
                        continue;
                    }

                    if (table[currentIndex].Equals(data))
                    {
                        return currentIndex;
                    }
                }
            }
            return -1; // не нашли
        }

        public void UpdateIndexesAfterRemoval()
        {
            T[] newTable = new T[table.Length];
            int newIndex = 0;

            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] != null)
                {
                    newTable[newIndex] = table[i];
                    newIndex++;
                }
            }

            table = newTable;
        }

    }
}
