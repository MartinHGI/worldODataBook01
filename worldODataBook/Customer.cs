using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace worldODataBook
{
    internal class Customer
    {
        private int id;
        private string name;
        private string surname;
        private string email;

        public int ID { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public string Email { get => email; set => email = value; }

        public Customer(int id, string name, string surname, string email)
        {
            this.ID = id;
            this.name = name;
            this.surname = surname;
            this.email = email;
        }

        public Customer(string name, string surname, string email)
        {
            this.ID = 0;
            this.name = name;
            this.surname = surname;
            this.email = email;
        }

        public override string ToString()
        {
            return $"{ID}. {name} {surname} {email}";
        }
    }
}
