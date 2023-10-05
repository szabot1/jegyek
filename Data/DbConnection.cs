using MySqlConnector;

namespace jegyek.Data;

public class DbConnection
{
    private static readonly string connectionString = "server=localhost;database=fz_jegyek;user=fz_jegyek;password=asd123";

    public static MySqlConnection GetConnection()
    {
        return new MySqlConnection(connectionString);
    }

    private DbConnection() { }
}