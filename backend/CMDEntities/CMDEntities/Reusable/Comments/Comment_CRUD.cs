using CMDEntities.Reusable.Super;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDEntities.Reusable.Comments
{
    class Comment_CRUD : super_CRUD<Comment>
    {
        public Comment_CRUD()
        {
            query_GetAll = "";
            query_GetByParent = "SELECT * FROM vw_comment WHERE ParentKey = @key";
            sp_update = "udp_Comment_ups";
        }

        public override Comment entityFromTableRow(DataRow row)
        {
            Comment entity = new Comment();
            entity.id = long.Parse(row["CommentKey"].ToString());
            entity.ParentKey = row["ParentKey"].ToString() == "" ? (long?)null : long.Parse(row["ParentKey"].ToString());
            entity.CommentText = row["Comment"].ToString();
            entity.CommentDate = DateTime.Parse(row["CommentDate"].ToString());
            entity.CommentByUser = long.Parse(row["CommentByUser"].ToString());
            entity.User = row["User"].ToString();
            entity.Identicon64 = row["Identicon64"].ToString();
            return entity;
        }

        public List<Comment> readRecursively(long? theParentKey)
        {
            List<Comment> recordset = new List<Comment>();

            if (theParentKey == null)
            {
                return recordset;
            }

            DataTable table = new DataTable();
            SqlConnection sqlConnection = connectionManager.getConnection();
            if (sqlConnection != null)
            {
                SqlCommand sqlCommand = new SqlCommand(query_GetByParent, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@key", theParentKey);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(table);

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Comment entity = entityFromTableRow(table.Rows[i]);
                    entity.Comments = readRecursively(entity.id);
                    recordset.Add(entity);
                }
            }
            return recordset;
        }


        public override void addParameters(Comment entity, ref Data_Base_MNG.SQL DM)
        {
            DM.Load_SP_Parameters("@CommentKey", entity.id);
            DM.Load_SP_Parameters("@ParentKey", entity.ParentKey);
            DM.Load_SP_Parameters("@Comment", entity.CommentText);
            DM.Load_SP_Parameters("@CommentByUser", entity.CommentByUser);
        }
    }
}
