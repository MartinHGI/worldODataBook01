using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace worldODataBook
{
    internal class Book
    {
        private int id;
        private int genre_id;
        private int author_id;
        private string name;
        private string release_date;
        private string available;

        public int ID { get => id; set => id = value; }
        public int Genre_id { get => genre_id; set => genre_id = value; }
        public int Author_id { get => author_id; set => author_id = value; }
        public string Name { get => name; set => name = value; }
        public string Release_date { get => release_date; set => release_date = value; }
        public string Available { get => available; set => available = value;}

        public Book(int id, int genre_id, int author_id, string name, string release_date, string available)
        {
            this.ID = id;
            this.genre_id = genre_id;
            this.author_id = author_id;
            this.name = name;
            this.release_date = release_date;
            this.available = available;
        }

        public Book(int genre_id, int author_id,  string name, string release_date, string available)
        {
            this.ID = 0;
            this.genre_id = genre_id;
            this.author_id = author_id;
            this.name = name;
            this.release_date = release_date;
            this.available = available;
        }

        public override string ToString()
        {
            return $"{ID}.{genre_id} {author_id} {name}  {release_date} {available}";
        }
    }
}
