using ConsoleApp7;

class Program
{
    static void Main(string[] args)
    {
        Database db = new Database();

        DatabaseConnectionParameters dbParams = new DatabaseConnectionParameters(
            "myServer", "myDatabase", "myUser", "myPassword"
        );

        string filePath = "connection_params.STD";
        db.SaveConnectionString(filePath, dbParams);

        db.LoadConnectionString(filePath);
        Console.WriteLine("Connection String: " + db.GetConnectionString());
    }
}
