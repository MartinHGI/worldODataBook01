using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAOTretak;

namespace worldODataBook
{
    internal class CustomerDAO
    {
        public void Delete(int iid)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM Customer WHERE id = @id", conn))
            {
                command.Parameters.Add(new SqlParameter("@id", iid));
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Customer> GetAll()
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT * FROM Customer", conn))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Customer customer = new Customer(
                        Convert.ToInt32(reader[0].ToString()),
                        reader[1].ToString(),
                        reader[2].ToString(),
                        reader[3].ToString()

                    );
                    yield return customer;
                }
                reader.Close();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customer? GetByID(int id)
        {
            Customer? customer = null;
            SqlConnection connection = DatabaseSingleton.GetInstance();
            // 1. declare command object with parameter
            using (SqlCommand command = new SqlCommand("SELECT * FROM Customer WHERE id = @Id", connection))
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
                    customer = new Customer(
                        Convert.ToInt32(reader[0].ToString()),
                        reader[1].ToString(),
                        reader[2].ToString(),
                        reader[3].ToString()
                        );
                }
                reader.Close();
            }

            return customer;

        }

        public void Save(Customer customer)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            SqlCommand command = null;

            if (customer.ID < 1)
            {
                using (command = new SqlCommand("INSERT INTO Customer (name, surname, email ) VALUES (@name, @surname, @email)", conn))
                {
                    command.Parameters.Add(new SqlParameter("@name", customer.Name));
                    command.Parameters.Add(new SqlParameter("@surname", customer.Surname));
                    command.Parameters.Add(new SqlParameter("@email", customer.Email));
                    command.ExecuteNonQuery();
                    //zjistim id posledniho vlozeneho zaznamu
                    command.CommandText = "Select @@Identity";
                    customer.ID = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            else
            {
                using (command = new SqlCommand("UPDATE Customer SET name = @name, surname = @surname, email = @email " +
                    "WHERE id = @id", conn))
                {
                    command.Parameters.Add(new SqlParameter("@id", customer.ID));
                    command.Parameters.Add(new SqlParameter("@name", customer.Name));
                    command.Parameters.Add(new SqlParameter("@surname", customer.Surname));
                    command.Parameters.Add(new SqlParameter("@email", customer.Email));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void RemoveAll()
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM Customer", conn))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
