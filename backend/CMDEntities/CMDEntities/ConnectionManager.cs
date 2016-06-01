using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDEntities.Reusable
{
    class ConnectionManager
    {
        private static string user = "CMD_APP";
        private static string server = @".\SQLEXPRESS";
        private static string pass = "capsonic";
        private static string database = "CMD";

        // private Data_Mng.SQL_DTMG DMG;
        private Data_Base_MNG.SQL DMG;

        public Data_Base_MNG.SQL getDataManager()
        {
            try
            {
                DMG = new Data_Base_MNG.SQL(server, database, user, pass);
                return DMG;
            }
            catch (Exception e)
            {   //TODO: log e in a future
                return null;
            }
        }

        public SqlConnection getConnection()
        {
            string sql_con = "Data Source=" + server + ";Initial Catalog=" + database + ";Persist Security Info=True;User ID=" + user + ";Password=" + pass;
            SqlConnection sqlConnection = new SqlConnection(sql_con);
            return sqlConnection;
        }
    }
}
