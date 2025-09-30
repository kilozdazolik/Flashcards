using Microsoft.Data.SqlClient;

string connectionString = "Server=localhost;Database=Flashcards;Trusted_Connection=True;TrustServerCertificate=True;";

using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        connection.Open();
        Console.WriteLine("Connection successful!");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Connection failed:");
        Console.WriteLine(ex.Message);
    }
}