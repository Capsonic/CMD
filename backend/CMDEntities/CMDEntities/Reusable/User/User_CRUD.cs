using CMDEntities.Reusable.Super;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDEntities.Reusable
{
    class User_CRUD : super_CRUD<User>
    {
        public User_CRUD()
        {
            sp_update = "udp_User_ups";
            query_GetAll = "SELECT * FROM vw_user";
            query_GetByID = "SELECT * FROM vw_user WHERE UserKey = ";
        }

        public override User entityFromTableRow(System.Data.DataRow row)
        {
            User entity = new User();
            entity.id = long.Parse(row["UserKey"].ToString());
            entity.Value = row["Value"].ToString();
            entity.UserName = row["UserName"].ToString();
            entity.Role = row["Role"].ToString();
            entity.Email = row["Email"].ToString();
            entity.Phone1 = row["Phone1"].ToString();
            entity.Phone2 = row["Phone2"].ToString();
            entity.Identicon = Encoding.ASCII.GetBytes(row["Identicon"].ToString());
            entity.Identicon64 = row["Identicon64"].ToString();

            return entity;
        }

        public override void addParameters(User entity, ref Data_Base_MNG.SQL DM)
        {
            if (entity.id == 0)
            {

            }
            DM.Load_SP_Parameters("@UserKey", entity.id);
            DM.Load_SP_Parameters("@Value", entity.Value);
            DM.Load_SP_Parameters("@UserName", entity.UserName);
            DM.Load_SP_Parameters("@Role", entity.Role);
            DM.Load_SP_Parameters("@Email", entity.Email);
            DM.Load_SP_Parameters("@Phone1", entity.Phone1);
            DM.Load_SP_Parameters("@Phone2", entity.Phone2);
            DM.Load_SP_Parameters("@Identicon", entity.Identicon);
            DM.Load_SP_Parameters("@Identicon64", entity.Identicon64);
        }

        public User readByName(string sUserName)
        {
            ErrorOccur = false;
            if (sUserName == "")
            {
                ErrorOccur = true;
                ErrorMessage = "UserName missing.";
                return null;
            }
            DM = connectionManager.getDataManager();
            string query = "SELECT * FROM [IQS].[dbo].[User] where UserName = '" + sUserName + "'";
            DataTable table = new DataTable();
            table = DM.Execute_Query(query);
            if (DM.ErrorOccur)
            {
                throw new Exception(DM.Error_Mjs);
            }
            if (table.Rows.Count > 0)
            {
                return entityFromTableRow(table.Rows[0]);
            }
            else
            {
                ErrorOccur = true;
                ErrorMessage = "Could not find user.";
                return null;
            }
        }
    }
}
