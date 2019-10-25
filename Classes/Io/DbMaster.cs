using FerieCountdown.Classes.Locale;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerieCountdown.Classes.Io
{

    
    public static class DbMaster
    {
        public static string ConnString { get; set; }

        public static CountdownLocale GetUserLocale()
        {
            //TODO implement checking of user's preferred locale
            return GetLocale(1);
        }

        /*
         * Gets a countdown locale from an ID
         * */
        public static CountdownLocale GetLocale(int id)
        {
            CountdownLocale locale = new CountdownLocale
            { 
                Id = 500, 
                Municipality = "Error"
            };

            //Get locale from SQL
            try
            {
                SqlConnection conn = new SqlConnection(ConnString);
                //retrieve the SQL Server instance version
                string query = string.Format(@"select * from [dbo].[DefaultLocales] where Id = {0};", id);

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
                locale.Data = JsonConvert.SerializeObject(ex);
            }

            return locale;
        }
        /*
         * Gets a countdown locale from a school name
         * */
        public static CountdownLocale GetLocale(string school)
        {
            CountdownLocale locale = new CountdownLocale
            {
                Id = 500,
                Municipality = "Error"
            };

            //Get locale from SQL
            try
            {
                SqlConnection conn = new SqlConnection(ConnString);
                //retrieve the SQL Server instance version
                string query = string.Format(@"select * from [dbo].[DefaultLocales] where School = {0};", school);

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
                locale.Data = JsonConvert.SerializeObject(ex);
            }

            return locale;
        }
    }
}
