using CMDEntities.Reusable.Super;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDEntities.Reusable.Token
{
    class Token_CRUD : super_CRUD<Token>
    {
        public Token_CRUD()
        {
            query_GetAll = "SELECT [TokenKey], [Token], [Subject], [SubjectKey], [DeadDate], [sys_active] " +
                            "FROM [IQS].[dbo].[Token]";
            sp_update = "udp_Token_ups";
        }
        public override Token entityFromTableRow(DataRow row)
        {
            Token entity = new Token();
            entity.id = long.Parse(row[0].ToString());
            entity.TokenGenerated = row[1].ToString();
            entity.Subject = row[2].ToString();
            entity.SubjectKey = long.Parse(row[3].ToString());
            entity.DeadDate = row[4].ToString() == "" ? (DateTime?)null : DateTime.Parse(row[4].ToString());
            return entity;
        }

        public override void addParameters(Token entity, ref Data_Base_MNG.SQL DM)
        {
            if (entity.id == 0)
            {

            }
            DM.Load_SP_Parameters("@TokenKey", entity.id);
            DM.Load_SP_Parameters("@Token", entity.TokenGenerated);
            DM.Load_SP_Parameters("@Subject", entity.Subject);
            DM.Load_SP_Parameters("@SubjectKey", entity.SubjectKey);
            DM.Load_SP_Parameters("@DeadDate", entity.DeadDate);
        }

        //public Token readByRFQ(RFQHeader rfq)
        //{
        //    Token token = new Token();

        //    string query = "SELECT [TokenKey], [Token], [Subject], [SubjectKey], [DeadDate] " +
        //                    "FROM [IQS].[dbo].[Token] WHERE Subject = 'RFQ' AND SubjectKey = @key";
        //    DataTable table = new DataTable();
        //    SqlConnection sqlConnection = connectionManager.getConnection();
        //    if (sqlConnection != null)
        //    {
        //        SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
        //        sqlCommand.Parameters.AddWithValue("@key", rfq.id);
        //        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
        //        sqlDataAdapter.Fill(table);

        //        if (table.Rows.Count > 0)
        //        {
        //            sqlConnection.Dispose();
        //            return entityFromTableRow(table.Rows[0]);
        //        }
        //    }
        //    return null;
        //}
        public Token readByToken(string sToken)
        {
            string query = "SELECT [TokenKey], [Token], [Subject], [SubjectKey], [DeadDate] " +
                            "FROM [IQS].[dbo].[Token] WHERE [Token] = '" + sToken + "'";
            DataTable table = new DataTable();
            SqlConnection sqlConnection = connectionManager.getConnection();
            if (sqlConnection != null)
            {
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(table);

                if (table.Rows.Count > 0)
                {
                    sqlConnection.Dispose();
                    return entityFromTableRow(table.Rows[0]);
                }
            }
            return null;
        }
    }
}
