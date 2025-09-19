//ADO_NET
//ADO (ActiveX Data Object) JSON, XML, MS SQL Server, MySQL, Oracle....
//#define SCALAR_CHECK
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//ADO.NET:
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace ADO_NET
{
    internal class Program
    {
        static string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Movies;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        

        static void Main(string[] args)
        {
            //1) Создаем подключение к Базе данных на Сервере:
            //Console.WriteLine(connectionString);
            //connection = new SqlConnection();
            //connection.ConnectionString = connectionString;

            Connector database = new Connector(connectionString);
            database.Connection();


            //Select("SELECT * FROM Directors");
            //Select("SELECT * FROM Movies");
            Connector.Select("*", "Directors");
            Connector.Select("movie_name,release_date,first_name+last_name AS Режиссер", "Movies,Directors", "director=director_id");


#if SCALAR_CHECK
			connection.Open();
			string cmd = "SELECT COUNT(*) FROM Directors";
			SqlCommand command = new SqlCommand(cmd, connection);
			Console.WriteLine($"Количество режиссереов:\t{command.ExecuteScalar()}");

			command.CommandText = "SELECT COUNT(*) FROM Movies";
			Console.WriteLine($"Количество киношек:\t{command.ExecuteScalar()}");

			command.CommandText = "SELECT last_name FROM Directors WHERE first_name=N'James'";
			Console.WriteLine(command.ExecuteScalar());

			connection.Close();

			Console.WriteLine(Scalar("SELECT last_name FROM Directors WHERE first_name=N'James'"));
			Console.WriteLine(Scalar("SELECT COUNT(*) FROM Movies")); 
#endif

            Connector.InsertDirector();
            //InsertMovie();
        }

    }
}
/*
CREATE TABLE [dbo].[Movies] (
    [movie_id]    INT            NOT NULL,
    [movie_name]  NVARCHAR (256) NOT NULL,
    [relese_date] DATE           NOT NULL,
    [director]    INT            NOT NULL,
    CONSTRAINT [FK_Movies_Directors] FOREIGN KEY ([director]) REFERENCES [dbo].[Directors] ([director_id]), 
    CONSTRAINT [PK_Movies] PRIMARY KEY ([movie_id])
);


 */
