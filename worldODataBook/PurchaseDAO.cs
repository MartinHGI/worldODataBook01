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
    internal class PurchaseDAO
    {
        public void Delete(int iid)// Metoda pro smazání záznamu 
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM Purchase WHERE id = @id", conn))
            {
                command.Parameters.Add(new SqlParameter("@id", iid));
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Purchase> GetAll()// Metoda pro získání/ výpis všech záznamů
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT * FROM Purchase", conn))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Purchase purchase = new Purchase(
                        Convert.ToInt32(reader[0].ToString()),
                        Convert.ToInt32(reader[0].ToString()),
                        Convert.ToInt32(reader[0].ToString()),
                        reader[1].ToString()
                        

                    );
                    yield return purchase;
                }
                reader.Close();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Purchase? GetByID(int id)
        {
            Purchase? purchase = null;
            SqlConnection connection = DatabaseSingleton.GetInstance();
            // 1. declare command object with parameter
            using (SqlCommand command = new SqlCommand("SELECT * FROM Purchase WHERE id = @Id", connection))
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
                    purchase = new Purchase(
                        Convert.ToInt32(reader[0].ToString()),
                        Convert.ToInt32(reader[0].ToString()),
                        Convert.ToInt32(reader[0].ToString()),
                        reader[2].ToString()
                        );
                }
                reader.Close();
            }

            return purchase;

        }
        // Metoda pro uložení nového nakupu do databáze nebo aktualizaci
        public void Save(Purchase purchase)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            SqlCommand command = null;

            if (purchase.ID < 1)
            {
                using (command = new SqlCommand("INSERT INTO Knihy (book_id, customer_id,  purchase_date ) VALUES (@book_id, @customer_id , @purchase_date)", conn))
                {
                    command.Parameters.Add(new SqlParameter("@book_id", purchase.Book_id));
                    command.Parameters.Add(new SqlParameter("@customer_id", purchase.Customer_id));
                    command.Parameters.Add(new SqlParameter("@purchase_date", purchase.Purchase_date));
                    command.ExecuteNonQuery();
                    //zjistim id posledniho vlozeneho zaznamu
                    command.CommandText = "Select @@Identity";
                    purchase.ID = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            else
            {
                using (command = new SqlCommand("UPDATE Purchase SET book_id= @book_id, customer_id = @customer_id, purchase_date = @purchase_date " +
                    "WHERE id = @id", conn))
                {
                    command.Parameters.Add(new SqlParameter("@id", purchase.ID));
                    command.Parameters.Add(new SqlParameter("@book_id", purchase.Book_id));
                    command.Parameters.Add(new SqlParameter("@customer_id", purchase.Customer_id));
                    command.Parameters.Add(new SqlParameter("@purchase_date", purchase.Purchase_date));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void RemoveAll()
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM Purchase", conn))
            {
                command.ExecuteNonQuery();
            }
        }

        public void Import()// Metoda pro import záznamů autorů z XML
        {
            XmlDocument x = new XmlDocument();
            x.Load("data.xml");
            XmlNodeList PurchaseNodes = x.SelectNodes("/data/Purchase");
            foreach (XmlNode an in PurchaseNodes)
            {
                int book_id = int.Parse(an.SelectSingleNode("book_id").InnerText);
                int customer_id = int.Parse(an.SelectSingleNode("customer_id").InnerText);
                string purchase_date = an.SelectSingleNode("purchase_date").InnerText;

                Purchase a = new Purchase(book_id, customer_id, purchase_date);
                Save(a);
            }
        }
    }
}
