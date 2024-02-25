using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace worldODataBook
{
    internal class Genre
    {
        private int id;
        private string name;


        public int ID { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }


        public Genre(int id, string name)
        {
            this.ID = id;
            this.name = name;

        }

        public Genre(string name)
        {
            this.ID = 0;
            this.name = name;
        }

        public override string ToString()
        {
            return $"{ID}. {name} ";
        }
    }
}
