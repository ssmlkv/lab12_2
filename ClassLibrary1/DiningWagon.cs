using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace lab10
{
    public class DiningWagon : PassengerWagon, ICloneable
    {
        public string operatingMode;
        protected int quantitybeds;

        public string OperatingMode
        {
            get => operatingMode;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    operatingMode = value;
                else
                    operatingMode = "Unknown";
            }
        }

        public int Quantitybeds
        {
            get => quantitybeds;
            set
            {
                if (value < 0)
                    quantitybeds = 0;
                else
                    quantitybeds = value;
            }
        }
        

        public DiningWagon(): base()
        {
            NumberBeds = 0;
            OperatingMode = "12-23";
        }
        public DiningWagon(IdNumber id, int number, int maxSpeed, string operatingMode, int numberBeds, int numberSeats) : base(id, number, maxSpeed, numberBeds, numberSeats)
        {
            OperatingMode = operatingMode;
            NumberBeds = 0;
        }

        public void Show()
        {
            base.Show();
            Console.Write("Вагон-ресторан: ");
            Console.WriteLine($"Режим работы: {operatingMode}, Количество койко-мест: {NumberBeds}");
        }
        public virtual string Tostring()
        {
            Console.Write("Вагон-ресторан: ");
            return base.ToString() + $" Режим работы: {OperatingMode}, Количество койко-мест: {NumberBeds}";
        }

        public virtual void Init()
        {
            base.Init();
            Console.WriteLine("Введите режим работы");
            try
            {
                OperatingMode = Console.ReadLine();
            }
            catch
            {
                OperatingMode = "12:00-23:00";
            }
        }
        public override void RandomInit()
        {
            base.RandomInit();
            NumberBeds = 0;
            OperatingMode = "12-23";
        }
        public object Clone()
        {
            return new DiningWagon(this.id, this.Number, this.MaxSpeed, this.OperatingMode, this.NumberBeds, this.NumberSeats);
        }

        // метод Equals для тестирования
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is DiningWagon d)
                return base.Equals(obj) && Number == d.Number;
            return false;
        }

    }
}
