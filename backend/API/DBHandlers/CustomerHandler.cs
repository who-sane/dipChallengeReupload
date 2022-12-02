using System.Data.SqlClient;
using Classes;

namespace API
{
    public class CustomerHandler : DatabaseHandler
    {
        public IEnumerable<Customer> GetCustomer()
        {
            List<Customer> customers = new List<Customer>();
            using(SqlConnection dbConnection = new SqlConnection(GetConnectionString())) 
            {
                dbConnection.Open();
                using(SqlCommand command = new SqlCommand("SELECT * FROM Customer", dbConnection))
                {
                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customers.Add(new Customer(){
                                CustID = reader.GetString(0),
                                FullName = reader.GetString(1),
                                Country = reader.GetString(2),
                                City = reader.GetString(3),
                                State = reader.GetString(4),
                                PostCode = reader.GetInt32(5),
                                SegID = reader.GetInt32(6),
                                Region = reader.GetString(7),
                            });
                        }
                    }
                }
                dbConnection.Close();
            }
            return customers;
        }

        
    }
}