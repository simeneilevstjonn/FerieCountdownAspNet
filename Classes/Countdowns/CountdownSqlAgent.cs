using FerieCountdown.Classes.Io;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerieCountdown.Classes.Countdowns
{
    public static class CountdownSqlAgent
    {
        public static string CreateCustomCountdown(string Owner, string Type, DateTime Time, string Background, string Text, string FinishedText, string HTML, string CSS, bool UseCccText, bool UseLocalTime)
        {

            int UseCcc = 0;
            if (UseCccText) UseCcc = 1;

            int UseLocal = 0;
            if (UseLocalTime) UseLocal = 1;

            string id = GenerateId();

            string query = string.Format("INSERT INTO [dbo].[CustomCountdowns] ([Id], [CountdownType], [CountdownTime], [CountdownText], [CountdownEndText], [BackgroundPath], [UseCCCText], [UseLocalTime], [CssAppend], [HtmlAppend], [Owner]) VALUES (N'{0}', N'{1}', N'{2}', N'{3}', N'{4}', N'{5}', {6}, {7}, N'{8}', N'{9}', N'{10}');",
                id, DbMaster.ValidateSql(Type), Time.ToString("u").Replace(' ', 'T'), DbMaster.ValidateSql(Text), DbMaster.ValidateSql(FinishedText), Background, UseCcc, UseLocal, CSS, HTML, Owner);
            //There is no Sql Validation on background, css, html or owner. Don't accept user input for these

            DbMaster.SqlQuery(query);
            return id;
        }



        static string GenerateId()
        {
            Random random = new Random();
            int rnd0 = random.Next(111111, int.MaxValue);
            int rnd1 = random.Next(111111, int.MaxValue);
            string intscomb = string.Format("{0}{1}", rnd0, rnd1);
            try
            {
                long longnum = long.Parse(intscomb);
                string base36 = Base36Encode(longnum);
                if (base36.Length < 10) return GenerateId();
                else if (base36.Length > 10) base36 = base36.Remove(10);

                if (DbMaster.CheckId(base36)) return base36;
                else return GenerateId();
            }
            catch (OverflowException)
            {
                return GenerateId();
            }
           
        }

        static readonly string CharList = "0123456789abcdefghijklmnopqrstuvwxyz";
        static string Base36Encode(long input)
        {
            if (input < 0) throw new ArgumentOutOfRangeException("input", input, "input cannot be negative");

            char[] clistarr = CharList.ToCharArray();
            var result = new Stack<char>();
            while (input != 0)
            {
                result.Push(clistarr[input % 36]);
                input /= 36;
            }
            return new string(result.ToArray());
        }
    }
}
