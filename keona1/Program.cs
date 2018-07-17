using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keona1
{
    class Program
    {
        static void Main(string[] args)
        {            
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Keona;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand("GetPersons", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Date",
                     SqlDbType.Date).Value = DateTime.Now.Date;
                    var da = new SqlDataAdapter(cmd);
                    var ds = new DataSet();
                    da.Fill(ds);                    
                    var PersonList = ds.Tables[0].AsEnumerable().Select(dataRow => new Person { PersonId = dataRow.Field<int>("PersonId"), NameFirst = dataRow.Field<string>("NameFirst"), NameLast = dataRow.Field<string>("NameLast") }).ToList();

                    foreach (Person person in PersonList)
                    {
                        Console.Write(person.PersonId);
                        Console.Write(" ");
                        Console.Write(person.NameFirst);
                        Console.WriteLine(person.NameLast);
                    };

                    Console.ReadLine();
                }
                    
                
            }
        }
    }
}
