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
    // Třída pro práci s databází autorů.
    internal class AuthorDAO
    {
        public void Delete(int iid) // Metoda pro smazání záznamu autora 
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM Author WHERE id = @id", conn))
            {
                command.Parameters.Add(new SqlParameter("@id", iid));
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Author> GetAll()// Metoda pro získání/ výpis všech autorů 
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT * FROM Author", conn))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Author author = new Author(
                        Convert.ToInt32(reader[0].ToString()),
                        reader[1].ToString(),
                        reader[2].ToString(),
                        reader[3].ToString()

                    );
                    yield return author;
                }
                reader.Close();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Author? GetByID(int id)// Metoda pro získání autora z databáze podle jeho id
            Author? author = null;
            SqlConnection connection = DatabaseSingleton.GetInstance();
            // 1. declare command object with parameter
            using (SqlCommand command = new SqlCommand("SELECT * FROM Author WHERE id = @Id", connection))
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
                    author = new Author(
                        Convert.ToInt32(reader[0].ToString()),
                        reader[1].ToString(),
                        reader[2].ToString(),
                        reader[3].ToString()
                        );
                }
                reader.Close();
            }

            return author;

        }
// Metoda pro uložení nového autora do databáze nebo aktualizaci
        public void Save(Author author)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            SqlCommand command = null;

            if (author.ID < 1)
            {
                using (command = new SqlCommand("INSERT INTO Author (name, surname, birth_date ) VALUES (@name, @surname, @birth_date)", conn))
                {
                    command.Parameters.Add(new SqlParameter("@name", author.Name));
                    command.Parameters.Add(new SqlParameter("@surname", author.Surname));
                    command.Parameters.Add(new SqlParameter("@birth_date", author.Birth_date));
                    try
                    {
                        command.ExecuteNonQuery();
                        command.CommandText = "Select @@Identity";
                        author.ID = Convert.ToInt32(command.ExecuteScalar());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Wrong input");
                    }
                }
            }
            else
            {
                using (command = new SqlCommand("UPDATE Author SET name = @name, surname = @surname, birth_date = @birth_date " +
                    "WHERE id = @id", conn))
                {
                    command.Parameters.Add(new SqlParameter("@id", author.ID));
                    command.Parameters.Add(new SqlParameter("@name", author.Name));
                    command.Parameters.Add(new SqlParameter("@surname", author.Surname));
                    command.Parameters.Add(new SqlParameter("@birth_date", author.Birth_date));
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine("Wrong input");
                    }
                }
            }
        }

       

        public void RemoveAll()// Metoda pro odstranění všech záznamů
{
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM Author", conn))
            {
                command.ExecuteNonQuery();
            }
        }


        public void Import()// Metoda pro import záznamů autorů z XML
{
            XmlDocument x = new XmlDocument();
            x.Load("data.xml");
            XmlNodeList AuthorNodes = x.SelectNodes("/data/Author");
            foreach (XmlNode an in AuthorNodes)
            {
                string name = an.SelectSingleNode("name").InnerText;
                string surname = an.SelectSingleNode("surname").InnerText;
                string birth_date = an.SelectSingleNode("birth_date").InnerText;

                Author a = new Author(name, surname, birth_date);
                Save(a);
            }
        }
    }
}
