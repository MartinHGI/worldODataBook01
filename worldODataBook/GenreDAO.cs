﻿using DAOTretak;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
namespace worldODataBook
{
    internal class GenreDAO
    {
        public void Delete(int iid)// Metoda pro smazání záznamu 
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM Genre WHERE id = @id", conn))
            {
                command.Parameters.Add(new SqlParameter("@id", iid));
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Genre> GetAll()// Metoda pro získání/ výpis všech záznamů
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT * FROM Genre", conn))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Genre genre = new Genre(
                        Convert.ToInt32(reader[0].ToString()),
                        reader[1].ToString()

                    );
                    yield return genre;
                }
                reader.Close();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Genre? GetByID(int id)
        {
            Genre? genre = null;
            SqlConnection connection = DatabaseSingleton.GetInstance();
            // 1. declare command object with parameter
            using (SqlCommand command = new SqlCommand("SELECT * FROM Genre WHERE id = @Id", connection))
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
                    genre = new Genre(
                        Convert.ToInt32(reader[0].ToString()),
                        reader[1].ToString()
                        );
                }
                reader.Close();
            }

            return genre;

        }
        // Metoda pro uložení nového žanru do databáze nebo aktualizaci
        public void Save(Genre genre)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            SqlCommand command = null;

            if (genre.ID < 1)
            {
                using (command = new SqlCommand("INSERT INTO Genre (name) VALUES (@name)", conn))
                {
                    command.Parameters.Add(new SqlParameter("@name", genre.Name));
                    command.ExecuteNonQuery();
                    //zjistim id posledniho vlozeneho zaznamu
                    command.CommandText = "Select @@Identity";
                    genre.ID = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            else
            {
                using (command = new SqlCommand("UPDATE Genre SET name = @name" +
                    "WHERE id = @id", conn))
                {
                    command.Parameters.Add(new SqlParameter("@id", genre.ID));
                    command.Parameters.Add(new SqlParameter("@name", genre.Name));

                    command.ExecuteNonQuery();
                }
            }
        }

        public void RemoveAll()
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM Genre", conn))
            {
                command.ExecuteNonQuery();
            }
        }

        public void Import()// Metoda pro import záznamů autorů z XML
        {
            XmlDocument x = new XmlDocument();
            x.Load("data.xml");
            XmlNodeList GenreNodes = x.SelectNodes("/data/Genre");
            foreach (XmlNode an in GenreNodes)
            {
                string name = an.SelectSingleNode("name").InnerText;

                Genre a = new Genre(name);
                Save(a);
            }
        }
    }
}
