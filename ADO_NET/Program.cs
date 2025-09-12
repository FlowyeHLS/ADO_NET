using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace ADO_NET
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ADO_NET");
        }
    }
}
/*
CREATE TABLE [dbo].[Movies]
(
	[movie_id] INT NOT NULL , 
    [movie_name] NVARCHAR(256) NOT NULL, 
    [relese_date] DATE NOT NULL, 
    [director] INT NOT NULL  
    CONSTRAINT [FK_Movies_Directors] FOREIGN KEY ([director]) REFERENCES [Directors]([director_id])
)

 */