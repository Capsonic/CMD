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
    class Metric_CRUD : super_CRUD<Metric>
    {
        public Metric_CRUD()
        {
            sp_update = "udp_Metric_ups";
        }

        public override void addParameters(Metric entity, ref SQL DM)
        {
            DM.Load_SP_Parameters("@MetricKey", entity.id);
            DM.Load_SP_Parameters("@Description", entity.Description);
            DM.Load_SP_Parameters("@CurrentValue", entity.CurrentValue);
            DM.Load_SP_Parameters("@GoalValue", entity.GoalValue);
            DM.Load_SP_Parameters("@FormatKey", entity.FormatKey);
            DM.Load_SP_Parameters("@BasisKey", entity.BasisKey);
            DM.Load_SP_Parameters("@ComparatorMethodKey", entity.ComparatorMethodKey);
        }

        public override Metric entityFromTableRow(DataRow row)
        {
            Metric entity = new Metric();

            entity.id = long.Parse(row["MetricKey"].ToString());
            entity.Description = row["Description"].ToString();
            entity.CurrentValue = row["CurrentValue"].ToString() == "" ? (decimal?)null : decimal.Parse(row["ProgressValue"].ToString());
            entity.GoalValue = row["GoalValue"].ToString() == "" ? (decimal?)null : decimal.Parse(row["GoalValue"].ToString());
            entity.FormatKey = row["FormatKey"].ToString() == "" ? (long?)null : long.Parse(row["FormatKey"].ToString());
            entity.FormatKey = row["BasisKey"].ToString() == "" ? (long?)null : long.Parse(row["BasisKey"].ToString());
            entity.FormatKey = row["ComparatorMethodKey"].ToString() == "" ? (long?)null : long.Parse(row["ComparatorMethod"].ToString());
            return entity;
        }
    }

    class cross_Objective_Metric_CRUD : superJunction_CRUD<Metric, cross_Objective_Metric>
    {
        public cross_Objective_Metric_CRUD()
        {
            sp_update = "udp_cross_Objective_Metric_ups";
            query_GetByParent = "SELECT * FROM vw_metric"
                                + " WHERE ObjectiveKey = @parentKey"
                                + " AND User_ID = @userKey";
        }

        public override void addParameters(cross_Objective_Metric entity, ref SQL DM)
        {
            DM.Load_SP_Parameters("@ID", entity.id);
            DM.Load_SP_Parameters("@ObjectiveKey", entity.ObjectiveKey);
            DM.Load_SP_Parameters("@MetricKey", entity.MetricKey);
        }

        public override Metric entityFromTableRow(DataRow row)
        {
            Metric_CRUD metric_CRUD = new Metric_CRUD();
            return metric_CRUD.entityFromTableRow(row);
        }
    }
}
