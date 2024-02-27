using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace worldODataBook
{
    internal class Author
    {
        private int id;
        private string name;
        private string surname;
        private string birth_date;

        public int ID { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public string Birth_date { get => birth_date; set => birth_date = value; }

        public Author(int id, string name, string surname, string birth_date)
        {
            this.ID = id;
            this.name = name;
            this.surname = surname;
            this.birth_date = birth_date;
        }

        public Author(string name, string surname, string birth_date)
        {
            this.ID = 0;
            this.name = name;
            this.surname = surname;
            this.birth_date = birth_date;
        }

        public override string ToString()
        {
            return $"{ID}. {name} {surname} {birth_date}";
        }




        

   

    }
}
