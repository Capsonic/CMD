using CMDEntities.Reusable.Super;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Base_MNG;
using System.Data;

namespace CMDEntities
{
    class Dashboard_CRUD : super_CRUD<Dashboard>
    {
        public Dashboard_CRUD()
        {
            sp_update = "udp_Dashboard_ups";
            query_GetByID = "SELECT * from vw_dashboard WHERE DashboardKey = ";
            query_GetAll = "SELECT * FROM vw_dashboard";
            query_SetActive = "UPDATE Dashboard SET sys_active = @bActive WHERE DashboardKey = @key";
            query_SetLock = "UPDATE Dashboard SET Entity_IsLocked = @IsLocked, Entity_Status = @Status WHERE DashboardKey = @key";
        }
        public override void addParameters(Dashboard entity, ref SQL DM)
        {
            DM.Load_SP_Parameters("@DashboardKey", entity.id);
            DM.Load_SP_Parameters("@Name", entity.Name);
            DM.Load_SP_Parameters("@Description", entity.Description);
        }

        public override Dashboard entityFromTableRow(DataRow row)
        {
            Dashboard entity = new Dashboard();

            entity.id = long.Parse(row["DashboardKey"].ToString());
            entity.Name = row["Name"].ToString();
            entity.Description = row["Description"].ToString();
            return entity;
        }
    }
}
