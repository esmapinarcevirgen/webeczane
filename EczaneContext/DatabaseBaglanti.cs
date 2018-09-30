using Npgsql;

namespace webeczane.Models
{
    public static class DatabaseBaglanti
    {
        public static NpgsqlConnection Connection()=> new NpgsqlConnection("User ID=postgres;Password=123;Host=localhost;Port=5432;Database=dbeczane;");
    }
}