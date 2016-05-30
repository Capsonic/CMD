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
    class Gant_CRUD : super_CRUD<Gant>
    {
        public Gant_CRUD()
        {
            sp_update = "udp_Gant_ups";
            query_GetByID = "SELECT * from vw_gant WHERE GantKey = ";
            query_GetAll = "SELECT * FROM vw_gant";
            query_GetSingleByParent = "SELECT * from vw_gant WHERE InitiativeKey = ";
            query_SetActive = "UPDATE Gant SET sys_active = @bActive WHERE GantKey = @key";
            query_SetLock = "UPDATE Gant SET Entity_IsLocked = @IsLocked, Entity_Status = @Status WHERE GantKey = @key";
        }
        public override void addParameters(Gant entity, ref SQL DM)
        {
            DM.Load_SP_Parameters("@GantKey", entity.id);
            DM.Load_SP_Parameters("@InitiativeKey", entity.InitiativeKey);
            DM.Load_SP_Parameters("@GantData", entity.GantData);
        }

        public override Gant entityFromTableRow(DataRow row)
        {
            Gant entity = new Gant();

            entity.id = long.Parse(row["GantKey"].ToString());
            entity.InitiativeKey = long.Parse(row["InitiativeKey"].ToString());
            entity.GantData = row["GantData"].ToString();
            return entity;
        }
    }
}
