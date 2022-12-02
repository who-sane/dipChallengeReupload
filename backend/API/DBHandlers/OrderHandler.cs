using System.Data.SqlClient;
using Classes;

namespace API
{
    public class OrderHandler : DatabaseHandler
    {
        public IEnumerable<Order> GetOrders()
        {
            List<Order> orders = new List<Order>();
            using(SqlConnection dbConnection = new SqlConnection(GetConnectionString())) 
            {
                dbConnection.Open();
                using(SqlCommand command = new SqlCommand("SELECT * FROM [Order];", dbConnection))
                {
                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string ProdID = reader.GetString(4);
                            Product prod = new Product();
                            prod.ProdID = ProdID;
                            using(SqlConnection prodTableConn = new SqlConnection(GetConnectionString()))
                            {
                                prodTableConn.Open();
                                using(SqlCommand command2 = new SqlCommand($"SELECT * FROM Product WHERE ProdID = '{ProdID}';", prodTableConn))
                                {
                                    using(SqlDataReader reader2 = command2.ExecuteReader())
                                    {
                                        while (reader2.Read())
                                        {
                                        prod.Description = reader2.GetString(1);
                                        prod.UnitPrice = reader2.GetInt32(2);
                                        prod.CatID = reader2.GetInt32(3);
                                        }                                        
                                    }
                                }
                            }


                            orders.Add(new Order(){
                                Prod = prod,
                                OrderDate = reader.GetString(0),
                                Quantity = reader.GetInt32(1),
                                ShipDate = reader.GetString(2),
                                ShipMode = reader.GetString(3),
                                CustID = reader.GetString(5)
                            });
                        }
                    }
                }
                dbConnection.Close();
            }
            return orders;
        }

        public float findTotal(Order order){
            return order.findTotal(order.Quantity, (float)order.Prod.UnitPrice);
        }

        public float getGST(Order order){
            return order.getGST(order.Quantity, (float)order.Prod.UnitPrice);
        }
      public int AddOrder(Order order)
        {
            using(SqlConnection dbConnection = new SqlConnection(GetConnectionString())) 
            {
                dbConnection.Open();
                using(SqlCommand command = new SqlCommand("INSERT INTO [ORDER] VALUES (@Order,@Qty,@ShipDate,@ShipMode, @ProdID,@CustID)", dbConnection))
                {
                    command.Parameters.AddWithValue("@Order", order.OrderDate);
                    command.Parameters.AddWithValue("@ProdID", order.Prod.ProdID);
                    command.Parameters.AddWithValue("@CustID", order.CustID);
                    command.Parameters.AddWithValue("@Qty", order.Quantity);
                    command.Parameters.AddWithValue("@ShipDate", order.ShipDate);
                    command.Parameters.AddWithValue("@ShipMode", order.ShipMode);

                    int tblRow = command.ExecuteNonQuery();
                    return tblRow;
                }
                dbConnection.Close();
            }
            
        }
        public int Delete(Order order)
        {
            using(SqlConnection dbConnection = new SqlConnection(GetConnectionString())) 
            {
                dbConnection.Open();
                using(SqlCommand command = new SqlCommand("DELETE FROM [ORDER] WHERE OrderDate = @Order AND ProdID = @ProdID AND CustID = @CustID", dbConnection))
                {
                    command.Parameters.AddWithValue("@Order", order.OrderDate);
                    command.Parameters.AddWithValue("@ProdID", order.Prod.ProdID);
                    command.Parameters.AddWithValue("@CustID", order.CustID);

                    int tblRow = command.ExecuteNonQuery();
                    return tblRow;
                }
                dbConnection.Close();
            }
            
        }

  
    }
}