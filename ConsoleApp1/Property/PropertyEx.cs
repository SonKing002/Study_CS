using System;

namespace PropertyEx
{
    public class BirthdayInfo
    {
        private string name;
        private DateTime birthday;

        public string Name
        { 
            get 
            {
                return name; 
            }
            set
            {
                name = value; 
            }
        }
        public DateTime Birthday
        {
            get
            {
                return birthday;
            }
            set
            {
                birthday = value;
            }
        }
        public int Age
        { 
            get
            {
                return new DateTime(DateTime.Now.Subtract(birthday).Ticks).Year; //public Subtract 함수 tick - this.tick
            }
        }
    }

    public class PropertyEx
    {
        static void Main(string[] args)
        { 
            BirthdayInfo birth = new BirthdayInfo();
            birth.Name = "서현";
            birth.Birthday = new DateTime(1991, 6, 28);

            Console.WriteLine($"Name : {birth.Name}");
            Console.WriteLine($"Brithday : {birth.Birthday}");
            Console.WriteLine($"Age : {birth.Age}");
        }
    }
}

