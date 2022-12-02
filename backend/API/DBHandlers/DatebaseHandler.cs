using System.Data.SqlClient;

namespace API
{
    // Directs the API to the remotely hosted Database on Somee
    public abstract class DatabaseHandler
    {
        public static string GetConnectionString()
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "dipChallenge2022.mssql.somee.com";
                builder.UserID = "whosaneDB_SQLLogin_1";
                builder.Password = "sn3x6tc3l6";
                builder.InitialCatalog = "dipChallenge2022";
                       return builder.ConnectionString;
            }
            catch (Exception e)
            {
                throw new Exception("failed to get conn string: " + e.Message);
            }
        }
    }
}