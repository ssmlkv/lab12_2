using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab9
{
    public class Discipline : IInit
    {
        public string name;
        public int contactHours;
        public int selfHours;
        public static int count = 0;
        private static Random rnd = new Random();

        public string Name
        {
            get => name;
            set
            {
                name = value;
            }
        }

        public int ContactHours
        {
            get => contactHours;
            set
            {
                if (value < 0)
                    contactHours = 0;
                else
                    contactHours = value;
            }
        }
        public int SelfHours
        {
            get => selfHours;
            set
            {
                if (value < 0)
                    selfHours = 0;
                else
                    selfHours = value;
            }
        }

        public Discipline()
        {
            name = "Без имени";
            contactHours = 0;
            selfHours = 0;
            count++;
        }

        public Discipline(string name, int contactHours, int selfHours)
        {
            this.name = name;
            this.contactHours = contactHours;
            this.selfHours = selfHours;
            count++;
        }


        public Discipline(Discipline otherDiscipline)
        {
            this.name = otherDiscipline.name;
            this.contactHours = otherDiscipline.contactHours;
            this.selfHours = otherDiscipline.selfHours;
            count++;
        }

        public int UnitsCredit
        {
            get => (contactHours + selfHours) / 38;
            set
            {
                if (value < 0)
                    throw new Exception("Зачетные единицы не могут быть меньше 0");
                else UnitsCredit = value;
            }
        }

        public static int Сount { get; private set; }

        public void ToString()
        {
            Console.WriteLine($"Название дисциплины: {name}.\nЧасы аудиторной работы: {contactHours}.\nЧасы самостоятельной работы: {selfHours}.\nЗачетные единицы: {UnitsCredit}.");
        }

        public static int GetCount()
        {
            return Сount;
        }


        public static double operator !(Discipline discipline)
        {
            return (double)discipline.SelfHours / (discipline.ContactHours + discipline.SelfHours) * 100;
        }
        public static Discipline operator ++(Discipline discipline)
        {
            discipline.ContactHours += 2;
            discipline.SelfHours -= 2;
            return discipline;
        }
        public static explicit operator double(Discipline discipline)
        {
            return (double)discipline.ContactHours / (discipline.ContactHours + discipline.SelfHours);
        }
        public static implicit operator int(Discipline discipline)
        {
            return discipline.ContactHours / 2;
        }
        public static bool operator >=(Discipline discipline1, Discipline discipline2)
        {
            return (discipline1.ContactHours + discipline1.SelfHours) >= (discipline2.ContactHours + discipline2.SelfHours);
        }
        public static bool operator <=(Discipline discipline1, Discipline discipline2)
        {
            return (discipline1.ContactHours + discipline1.SelfHours) <= (discipline2.ContactHours + discipline2.SelfHours);
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Discipline other = (Discipline)obj;
            return Name == other.Name && ContactHours == other.ContactHours && SelfHours == other.SelfHours;
        }

        public void Init()
        {
            Console.Write("Введите аудиторные часы: ");
            try
            {
                ContactHours = int.Parse(Console.ReadLine());
            }
            catch
            {
                ContactHours = 5;
            }
            Console.Write("Введите самостоятельные часы: ");
            try
            {
                SelfHours = int.Parse(Console.ReadLine());
            }
            catch
            {
                SelfHours = 10;
            }
        }

        public void RandomInit()
        {
            SelfHours = rnd.Next(1,100);
            ContactHours = rnd.Next(1, 100);
        }
    }
}
