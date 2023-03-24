namespace CRUDapi.TestService;
using MySql.Data.MySqlClient;
using Dapper;


    public class Practice
    {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Tel { get; set; }
    public DateTime CreateTime { get; set; }
    public DateTime EditTime { get; set; }
}

