using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerieCountdownWithAuth.Classes
{

    
    public static class DbMaster
    {
        public static string ConnString { get; set; }
        public static CountdownLocale GetUserLocale()
        {

            // TODO: Implement method for checking what users prefferred locale is. 

            CountdownLocale locale = new CountdownLocale();
            //Get locale from SQL
            try
            {
                SqlConnection conn = new SqlConnection(ConnString);
                //retrieve the SQL Server instance version
                string query = @"select * from [dbo].[DefaultLocales] where Id = 1;";

                SqlCommand cmd = new SqlCommand(query, conn);

                //open connection
                conn.Open();

                //execute the SQLCommand
                SqlDataReader dr = cmd.ExecuteReader();

                //check if there are records
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        locale = new CountdownLocale
                        {
                            Id = dr.GetInt32(0),
                            Municipality = dr.GetString(1),
                            School = dr.GetString(2),
                            Data = dr.GetString(3),
                        };
                    }
                }
                else
                {
                    Console.WriteLine("No data found.");
                }
                dr.Close();
                cmd.Dispose();

            }
            catch (Exception ex)
            {
                //display error message
                Console.WriteLine("Exception: " + ex.Message);
            }

            return locale;
        }
    }
}
