using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAOTretak;

namespace worldODataBook
{
    internal class AuthorDAO
    {
        public void Delete(int iid)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM Author WHERE id = @id", conn))
            {
                command.Parameters.Add(new SqlParameter("@id", iid));
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Author> GetAll()
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
        public Author? GetByID(int id)
        {
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

        public void RemoveAll()
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM Author", conn))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
