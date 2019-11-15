using FerieCountdown.Classes.Countdowns;
using FerieCountdown.Classes.Exceptions;
using FerieCountdown.Classes.Locale;
using FerieCountdown.Models;
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

        public static string ValidateSql(string input)
        {
            if (input.IndexOfAny(new char[] { ';', '\'', '*', '/', '-', '_' }) > -1) throw new BadSqlException(string.Format("Illegal user input: {0}", input));
            else return input;
        }
         
        public static void SqlQuery(string query)
        {
            SqlConnection conn = new SqlConnection(ConnString);
            //retrieve the SQL Server instance version
            SqlCommand cmd = new SqlCommand(query, conn);
            //open connection
            conn.Open();
            //execute the SQLCommand
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Close();
            cmd.Dispose();
        }

        public static bool CheckId(string id)
        {
            SqlConnection conn = new SqlConnection(ConnString);
            //retrieve the SQL Server instance version
            string query = string.Format(@"select Id from [dbo].[CustomCountdowns] where Id = N'{0}';", id);

            SqlCommand cmd = new SqlCommand(query, conn);

            //open connection
            conn.Open();

            //execute the SQLCommand
            SqlDataReader dr = cmd.ExecuteReader();

            //check if there are records
            if (dr.HasRows) return false;
            else return true;
        }

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
                string query = string.Format(@"select * from [dbo].[DefaultLocales] where LookupName = N'{0}';", ValidateSql(school));

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
                            Data = dr.GetString(2)
                        };
                    }
                }
                else throw new InvalidLocaleException(string.Format("Invalid default countdownlocale {0}", school));
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
            dr.Close();
            cmd.Dispose();
            return ReturnList;
        }

        //Custom countdown methods
        public static CustomCountdown GetCustomCountdown(string id)
        {
            // TODO fix this
            CustomCountdown returner = new CustomCountdown();

            SqlConnection conn = new SqlConnection(ConnString);
            //retrieve the SQL Server instance version
            string query = string.Format(@"select Id, CountdownType, CountdownTime, CountdownText, CountdownEndText, BackgroundPath, UseCCCText, UseLocalTime, CssAppend, HtmlAppend, Owner from [dbo].[CustomCountdowns] where Id = N'{0}';", ValidateSql(id));

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
                    returner = new CustomCountdown
                    {
                        Id = dr.GetString(0),
                        CountdownType = dr.GetString(1),
                        CountdownTime = dr.GetDateTime(2), 
                        CountdownText = dr.GetString(3),
                        CountdownEndText = dr.GetString(4),
                        Background = new CountdownBackground 
                        { 
                            Path = dr.GetString(5),
                            UseCCC = dr.GetBoolean(6),
                            Css = dr.GetString(8),
                            Html = dr.GetString(9)
                        },
                        UseLocalTime = dr.GetBoolean(7),
                        Owner = dr.GetString(10)
                    };
                }
            }
            else
            {
                Console.WriteLine("No data found.");
            }
            dr.Close();
            cmd.Dispose();

            return returner;
        }

        public static Dictionary<string, string> GetDictionaryFromSql(string sql)
        {
            Dictionary<string, string> ReturnList = new Dictionary<string, string>();
            SqlConnection conn = new SqlConnection(ConnString);
            //retrieve the SQL Server instance version

            SqlCommand cmd = new SqlCommand(sql, conn);

            //open connection
            conn.Open();

            //execute the SQLCommand
            SqlDataReader dr = cmd.ExecuteReader();

            //check if there are records
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ReturnList.Add(dr.GetString(0), dr.GetString(1));
                }
            }
            dr.Close();
            cmd.Dispose();
            return ReturnList;
        }
    }
}
