using System;
using System.Data.SqlClient;

namespace MovieTickets
{
    class Program
    {
        static void Main(string[] args)
        {
            string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=aspnet-razorCore-53bc9b9d-9d6a-45d4-8429-2a2761773502;
                                Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;
                                   ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            SqlConnection conn = new SqlConnection(connString);
            string sql = @"SELECT Genre, Count(Genre) AS 'Number of Tickets' 
                             from Movies inner join Tickets 
                                on Movies.MovieId = Tickets.MovieId 
                                   Group By Genre ";

            try
            {
                conn.Open();
                Console.WriteLine("The database Connection is: {0}", conn.State);

                //Create the SQL command
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.Write("\nGenre: "+reader.GetValue(0)+ "\t\t Tickets: " + reader.GetValue(1));
                }


            }
            catch(SqlException e)
            {
                Console.WriteLine("\n\nThere was an error while communicating with the database, Error: {0}",e.Message);
            }
            finally
            {
                conn.Close();
                Console.WriteLine("\n\n\nThe database Connection is: {0}", conn.State);
            }

        }
    }
}
