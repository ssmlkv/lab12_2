using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace lab10
{
    public class IdNumber
    {
        protected int id; // поле id
        public int Id // свойство для поля id
        {
            get => id;
            set
            {
                if (value < 0)
                    throw new Exception("ID должно быть больше нуля");
                else
                    id = value;
            }
        }
        // конструктор без параметров
        public IdNumber()
        {
            Id = 1;
        }

        // конструктор с параметром
        public IdNumber(int id)
        {
            Id = id;
        }
        // перегруженный метод ToString
        public override string ToString()
        {
            return Id.ToString();
        }

        // перегруженный метод Equals()
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj is IdNumber i)
                return Id == i.Id;
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class Carriage : IInit, IComparable, ICloneable
    {
        protected string[] cargoTransporteds =
        {
            "Метал",
            "Золото",
            "Алмаз",
            "Серебро"
        };

        public IdNumber id;
        protected int number;
        protected int maxSpeed;
        protected Random rnd = new Random();


        public int Number //номер вагона
        {
            get => number;
            set
            {
                if (value < 0)
                    number = 0;
                else
                    number = value;
            }
        }
        public int MaxSpeed//макс скорость
        {
            get => maxSpeed;
            set
            {
                if (value < 0)
                    maxSpeed = 0;
                else
                    maxSpeed = value;
            }
        }

        public Carriage()// конструктор без параметров
        {
            number = 0;
            maxSpeed = 0;
            id = new IdNumber(0); // Инициализация объекта id
        }

        public Carriage(IdNumber id,int number, int maxSpeed)//конструктор с параметрами
        {
            Number = number;
            MaxSpeed = maxSpeed;
            this.id = id; // Инициализация объекта id
        }

        public virtual void ShowVirtual()
        {
            Console.WriteLine($"{id}, Номер вагона: {Number} Максимальная скорость {MaxSpeed}");
        }

        public override string ToString()
        {
            return $"{id}, Номер вагона: {Number} Максимальная скорость {MaxSpeed}";
        }
        public void Show()
        {
            Console.WriteLine($"{id}, Номер вагона: {Number} Максимальная скорость {MaxSpeed}");
        }

        public virtual void Init()// для ввода информации об объектах класса с клавиатуры
        {
            Console.Write("Введите id: ");
            try
            {
                id.Id = int.Parse(Console.ReadLine());
            }
            catch
            {
                id.Id = 1;
            }
            Console.WriteLine("Введите номер вагона");
            try
            {
                Number = int.Parse(Console.ReadLine());
            }
            catch
            {
                Number = 0; 
            }
            Console.WriteLine("Введите Максимальную скорость");
            try
            {
                MaxSpeed = int.Parse(Console.ReadLine());
            }
            catch
            {
                MaxSpeed = 0;
            }
        }

        public virtual void RandomInit()//для формирования объектов класса с помощью ДСЧ
        {
            id.Id = rnd.Next(1, 100);
            Number = rnd.Next(1,100);
            MaxSpeed = rnd.Next(1,200);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is Carriage w)
                return id.Equals(w.id) && Number == w.Number && MaxSpeed == w.MaxSpeed;
            return false;
        }
        public override int GetHashCode()
        {
            return id.GetHashCode() + number.GetHashCode() + maxSpeed.GetHashCode();
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return -1;
            if (obj is not Carriage) return -1;
            Carriage w = new Carriage();
            return w.Number.CompareTo(Number);
        }

        public object Clone()
        {
            return new Carriage(id,number, MaxSpeed);
        }
        public object ShallowCopy()
        {
            return this.MemberwiseClone();
        }
    }
}
