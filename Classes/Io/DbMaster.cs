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

    
    public class DbMaster
    {
        public DbMaster(string ConenctionString)
        {
            //Initalize and open SQL connection
            conn = new SqlConnection(ConenctionString);
            conn.Open();
        }

        protected readonly SqlConnection conn;

        public string ValidateSql(string input)
        {
            if (input.IndexOfAny(new char[] { ';', '\'', '*', '/', '-', '_', '"' }) > -1) throw new BadSqlException(string.Format("Illegal user input: {0}", input));
            else return input;
        }
         
        public int SqlQuery(string query)
        {
            //retrieve the SQL Server instance version
            SqlCommand cmd = new SqlCommand(query, conn);
            //execute the SQLCommand
            int ra = cmd.ExecuteNonQuery();
            cmd.Dispose();
            return ra;
        }

        public async Task<int> SqlQueryAsync(string query)
        {
            //retrieve the SQL Server instance version
            SqlCommand cmd = new SqlCommand(query, conn);
            //execute the SQLCommand
            return await cmd.ExecuteNonQueryAsync();
        }

        public bool CheckId(string id)
        {

            //retrieve the SQL Server instance version
            string query = string.Format(@"select Id from [dbo].[CustomCountdowns] where Id = N'{0}';", id);
            SqlCommand cmd = new SqlCommand(query, conn);

            //execute the SQLCommand
            SqlDataReader dr = cmd.ExecuteReader();

            cmd.Dispose();

            bool r = dr.HasRows;

            //Close DataReader
            dr.Close();

            return !r;
        }

        public CountdownLocale GetUserLocale(HttpRequest Request)
        {
            return Request.Cookies["locale"] switch 
            {
                "custom" => throw new ArgumentOutOfRangeException("custom locale can only be used with (HttpRequest, String) overload"),
                "work" => throw new ArgumentOutOfRangeException("custom locale can only be used with (HttpRequest, String) overload"),
                _ => GetLocale(Request.Cookies["locale"])
            };
        }

        public CountdownLocale GetUserLocale(HttpRequest Request, string UserID)
        {
            return Request.Cookies["locale"] switch
            {
                "custom" => GetCustomLocale(UserID),
                "work" => GetCustomLocale(UserID),
                _ => GetLocale(Request.Cookies["locale"])
            };
        }

        /*
         * Gets a countdown locale from a school lookup name
         * */
        public CountdownLocale GetLocale(string school)
        {
            CountdownLocale locale = new CountdownLocale
            {
                Municipality = "Error"
            };

            //Get locale from SQL
            try
            {

                //retrieve the SQL Server instance version
                string query = string.Format(@"select * from [dbo].[DefaultLocales] where LookupName = N'{0}';", ValidateSql(school));
                SqlCommand cmd = new SqlCommand(query, conn);
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

        public CountdownLocale GetCustomLocale(string UserId)
        {
            CountdownLocale locale = new CountdownLocale
            {
                Municipality = "Error"
            };
            
            //Get locale from SQL
            try
            {
                //retrieve the SQL Server instance version
                string query = string.Format(@"select * from [dbo].[Customlocales] where UserId = N'{0}';", UserId);

                SqlCommand cmd = new SqlCommand(query, conn);

                //execute the SQLCommand
                SqlDataReader dr = cmd.ExecuteReader();

                //check if there are records
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        locale = new CountdownLocale
                        {
                            Data = dr.GetString(2),
                            IsWork = dr.GetBoolean(3)
                        };
                    }
                }
                else throw new InvalidLocaleException(string.Format("Invalid userid {0}", UserId));

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


        public List<SimpleMunicipality> GetAllLocales()
        {
            List<SimpleMunicipality> ReturnList = new List<SimpleMunicipality>();
            //Get locale from SQL

            //retrieve the SQL Server instance version
            string query = string.Format(@"select LookupName, School, Municipality from [dbo].[DefaultLocales] ORDER BY Municipality, School ASC;");

            SqlCommand cmd = new SqlCommand(query, conn);

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
        public CustomCountdown GetCustomCountdown(string id)
        {
            CustomCountdown returner = new CustomCountdown();


            //retrieve the SQL Server instance version
            string query = string.Format(@"select Id, CountdownType, CountdownTime, CountdownText, CountdownEndText, BackgroundPath, UseCCCText, UseLocalTime, CssAppend, HtmlAppend, Owner from [dbo].[CustomCountdowns] where Id = N'{0}';", ValidateSql(id));

            SqlCommand cmd = new SqlCommand(query, conn);

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

        public Dictionary<string, string> GetDictionaryFromSql(string sql)
        {
            Dictionary<string, string> ReturnList = new Dictionary<string, string>();

            //retrieve the SQL Server instance version

            SqlCommand cmd = new SqlCommand(sql, conn);

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

        public async Task<List<CustomCountdown>> GetAllUserCountdownDataJsonAsync(string user)
        {

            //retrieve the SQL Server instance version

            string query = string.Format(@"select Id, CountdownType, CountdownTime, CountdownText, CountdownEndText, BackgroundPath, UseCCCText, UseLocalTime, CssAppend, HtmlAppend, Owner from [dbo].[CustomCountdowns] where Owner = N'{0}';", user);

            SqlCommand cmd = new SqlCommand(query, conn);

            //execute the SQLCommand
            SqlDataReader dr = await cmd.ExecuteReaderAsync();

            List<CustomCountdown> customCountdowns = new List<CustomCountdown>();

            //check if there are records
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    customCountdowns.Add(new CustomCountdown
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
                    });
                }
            }
            dr.Close();
            cmd.Dispose();
            return customCountdowns;
        }
    }
}
