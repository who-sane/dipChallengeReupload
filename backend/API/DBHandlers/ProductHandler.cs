using System.Data.SqlClient;
using Classes;

namespace API
{
    public class ProductHandler : DatabaseHandler
    {
        public IEnumerable<Product> GetProduct()
        {
            List<Product> products = new List<Product>();
            using(SqlConnection dbConnection = new SqlConnection(GetConnectionString())) 
            {
                dbConnection.Open();
                using(SqlCommand command = new SqlCommand("SELECT * FROM Product", dbConnection))
                {
                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product(){
                                ProdID = reader.GetString(0),
                                Description = reader.GetString(1),
                                UnitPrice = reader.GetInt32(2),
                                CatID = reader.GetInt32(3)
                            });
                        }
                    }
                }
                dbConnection.Close();
            }
            return products;
        }

    }
}