using DAOTretak;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace worldODataBook
{
    internal class BookDAO
    {
        public void Delete(int iid)// Metoda pro smazání záznamu 
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM Book WHERE id = @id", conn))
            {
                command.Parameters.Add(new SqlParameter("@id", iid));
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Book> GetAll()// Metoda pro získání/ výpis všech záznamů
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT * FROM Book", conn))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Book book = new Book(
                        Convert.ToInt32(reader[0].ToString()),
                        Convert.ToInt32(reader[1].ToString()),
                        Convert.ToInt32(reader[2].ToString()),
                        reader[3].ToString(),
                        reader[4].ToString(),
                        reader[5].ToString()

                    );
                    yield return book;
                }
                reader.Close();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Book? GetByID(int id)
        {
            Book? book = null;
            SqlConnection connection = DatabaseSingleton.GetInstance();
            // 1. declare command object with parameter
            using (SqlCommand command = new SqlCommand("SELECT * FROM Book WHERE id = @Id", connection))
            {
                // 2. define parameters used in command 
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@Id";
                param.Value = id;

                // 3. add new parameter to command object
                command.Parameters.Add(param);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    book = new Book(
                        Convert.ToInt32(reader[0].ToString()),
                        Convert.ToInt32(reader[1].ToString()),
                        Convert.ToInt32(reader[2].ToString()),
                        reader[3].ToString(),
                        reader[4].ToString(),
                        reader[5].ToString()
                        );
                }
                reader.Close();
            }

            return book;

        }
        // Metoda pro uložení nového book do databáze nebo aktualizaci
        public void Save(Book book)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            SqlCommand command = null;

            if (book.ID < 1)
            {
                using (command = new SqlCommand("INSERT INTO Knihy (genre_id, author_id, name,  release_date, available ) VALUES (@genre_id, @author_id ,@name, @release_date, @available)", conn))
                {
                    command.Parameters.Add(new SqlParameter("@genre_id", book.Genre_id));
                    command.Parameters.Add(new SqlParameter("@author_id", book.Author_id));
                    command.Parameters.Add(new SqlParameter("@name", book.Name));
                    command.Parameters.Add(new SqlParameter("@release_date", book.Release_date));
                    command.Parameters.Add(new SqlParameter("@available", book.Available));
                    command.ExecuteNonQuery();
                    //zjistim id posledniho vlozeneho zaznamu
                    command.CommandText = "Select @@Identity";
                    book.ID = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            else
            {
                using (command = new SqlCommand("UPDATE Book SET genre_id= @genre_id, author_id = @author_id, name = @name, release_date = @release_date,  available = @available" +
                    "WHERE id = @id", conn))
                {
                    command.Parameters.Add(new SqlParameter("@id", book.ID));
                    command.Parameters.Add(new SqlParameter("@genre_id", book.Genre_id));
                    command.Parameters.Add(new SqlParameter("@author_id", book.Author_id));
                    command.Parameters.Add(new SqlParameter("@name", book.Name));
                    command.Parameters.Add(new SqlParameter("@release_date", book.Release_date));
                    command.Parameters.Add(new SqlParameter("@available", book.Available));

                    command.ExecuteNonQuery();
                }
            }
        }

        public void RemoveAll()
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM Book", conn))
            {
                command.ExecuteNonQuery();
            }
        }

        public void Import()// Metoda pro import záznamů autorů z XML
        {
            XmlDocument x = new XmlDocument();
            x.Load("data.xml");
            XmlNodeList BookNodes = x.SelectNodes("/data/Book");
            foreach (XmlNode an in BookNodes)
            {
                int genre_id = int.Parse(an.SelectSingleNode("genre_id").InnerText) ;
                int author_id = int.Parse(an.SelectSingleNode("author_id").InnerText);
                string name = an.SelectSingleNode("name").InnerText;
                string release_date = an.SelectSingleNode("release_date").InnerText;
                string available = an.SelectSingleNode("available").InnerText;

                Book a = new Book(genre_id, author_id, name, release_date, available);
                Save(a);
            }
        }
    }
}
