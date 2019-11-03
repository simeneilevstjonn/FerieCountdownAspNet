using FerieCountdown.Classes.Locale;
using Microsoft.AspNetCore.Http;
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

        public static CountdownLocale GetUserLocale(HttpRequest Request)
        {
            return GetLocale(Request.Cookies["locale"]);
        }

        /*
         * Gets a countdown locale from a school lookup name
         * */
        public static CountdownLocale GetLocale(string school)
        {
            CountdownLocale locale = new CountdownLocale
            {
                Municipality = "Error"
            };

            //Get locale from SQL
            try
            {
                SqlConnection conn = new SqlConnection(ConnString);
                //retrieve the SQL Server instance version
                string query = string.Format(@"select * from [dbo].[DefaultLocales] where LookupName = N'{0}';", school);

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
                            Municipality = dr.GetString(0),
                            School = dr.GetString(1),
                            Data = dr.GetString(2),
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

        public static List<SimpleMunicipality> GetAllLocales()
        {
            List<SimpleMunicipality> ReturnList = new List<SimpleMunicipality>();
            //Get locale from SQL
            /*try
            {*/
                SqlConnection conn = new SqlConnection(ConnString);
                //retrieve the SQL Server instance version
                string query = string.Format(@"select LookupName, School, Municipality from [dbo].[DefaultLocales] ORDER BY Municipality, School ASC;");

                SqlCommand cmd = new SqlCommand(query, conn);

                //open connection
                conn.Open();

                //execute the SQLCommand
                SqlDataReader dr = cmd.ExecuteReader();

                SimpleMunicipality currentMunicipality = new SimpleMunicipality();
                string currentName = string.Empty;
                bool firstrun = true;

                //check if there are records
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (firstrun)
                        {
                            currentName = dr.GetString(2);
                            currentMunicipality.Name = currentName;
                            firstrun = false;
                        }
                    if (dr.GetString(2) != currentName)
                    {
                        ReturnList.Add(currentMunicipality);
                        currentMunicipality = new SimpleMunicipality();
                        currentName = dr.GetString(2);
                        currentMunicipality.Name = currentName;
                    }
                    currentMunicipality.Schools.Add(new SimpleLocale
                        {
                            LookupName = dr.GetString(0),
                            Name = dr.GetString(1)
                        });
                    }
                    ReturnList.Add(currentMunicipality);
                }
                else
                {
                    Console.WriteLine("No data found.");
                }
                dr.Close();
                cmd.Dispose();

            /*}
            catch (Exception ex)
            {
                //display error message
                Console.WriteLine("Exception: " + ex.Message);
            }*/
            return ReturnList;
        }
    }
}
