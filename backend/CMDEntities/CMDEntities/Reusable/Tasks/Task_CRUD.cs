using CMDEntities.Reusable.Super;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDEntities.Reusable.Tasks
{
    class Task_CRUD : super_CRUD<Task>
    {
        public Task_CRUD()
        {
            query_GetAll = "SELECT * FROM [IQS].[dbo].[Task]";
            query_GetByID = "SELECT * FROM [IQS].[dbo].[Task] WHERE TaskKey = ";
            sp_update = "udp_Task_ups";
        }
        public override Task entityFromTableRow(DataRow row)
        {
            Task entity = new Task();
            entity.id = long.Parse(row["TaskKey"].ToString());
            entity.CreatedDate = row["CreatedDate"].ToString() == "" ? (DateTime?)null : DateTime.Parse(row[1].ToString());
            return entity;
        }

        public override void addParameters(Task entity, ref Data_Base_MNG.SQL DM)
        {
            DM.Load_SP_Parameters("@TaskKey", entity.id);
        }
    }
    class ToDo_CRUD : super_CRUD<ToDo>
    {
        public ToDo_CRUD()
        {
            query_GetAll = "";
            query_GetByParent = "SELECT * FROM vw_todo WHERE TaskKey = @key";
            sp_update = "udp_ToDo_ups";
            query_DeleteByID = "DELETE FROM ToDo WHERE ToDoKey = ";
            query_GetByID = "SELECT * FROM vw_todo WHERE ToDoKey = ";
        }

        public override ToDo entityFromTableRow(DataRow row)
        {
            ToDo entity = new ToDo();
            entity.id = long.Parse(row["ToDoKey"].ToString());
            entity.TaskKey = long.Parse(row["TaskKey"].ToString());
            entity.CategoryKey = row["CategoryKey"].ToString() == "" ? (int?)null : int.Parse(row["CategoryKey"].ToString());
            entity.IsDone = bool.Parse(row["IsDone"].ToString());
            entity.Description = row["ToDo"].ToString();
            entity.DueDate = row["DueDate"].ToString() == "" ? (DateTime?)null : DateTime.Parse(row["DueDate"].ToString());
            entity.AssignedTo = long.Parse(row["AssignedTo"].ToString());
            entity.Status = row["Status"].ToString();
            entity.Category = row["Categorie"].ToString();
            entity.AssignedBy = long.Parse(row["AssignedBy"].ToString());
            entity.IsImportant = bool.Parse(row["IsImportant"].ToString());
            return entity;
        }

        public override void addParameters(ToDo entity, ref Data_Base_MNG.SQL DM)
        {
            if (entity.id == 0)
            {
                DM.Load_SP_Parameters("@CategoryKey", entity.CategoryKey);

            }
            DM.Load_SP_Parameters("@TaskKey", entity.TaskKey);
            DM.Load_SP_Parameters("@ToDoKey", entity.id);
            DM.Load_SP_Parameters("@IsDone", entity.IsDone);
            DM.Load_SP_Parameters("@ToDo", entity.Description);
            DM.Load_SP_Parameters("@DueDate", entity.DueDate);
            DM.Load_SP_Parameters("@AssignedTo", entity.AssignedTo);
            DM.Load_SP_Parameters("@Status", entity.Status);
            DM.Load_SP_Parameters("@AssignedBy", entity.AssignedBy);
            DM.Load_SP_Parameters("@IsImportant", entity.IsImportant);
        }
    }

    class ToDoCategorie_CRUD : super_CRUD<cat_ToDoCategorie>
    {
        public ToDoCategorie_CRUD()
        {
            query_GetAll = "SELECT ToDoCategoryKey, Value, Entity " +
                                 "FROM cat_ToDoCategorie";
            query_GetByParent = "";
        }
        public override cat_ToDoCategorie entityFromTableRow(DataRow row)
        {
            cat_ToDoCategorie entity = new cat_ToDoCategorie();
            entity.id = long.Parse(row[0].ToString());
            entity.Value = row[1].ToString();
            entity.Entity = row[2].ToString();
            return entity;
        }

        public List<cat_ToDoCategorie> readByEntity(string theEntityName)
        {
            ToDoPredefined_CRUD todoPredefined_CRUD = new ToDoPredefined_CRUD();
            List<cat_ToDoCategorie> allCategories = this.readAll();
            List<cat_ToDoCategorie> result = allCategories.FindAll(delegate (cat_ToDoCategorie categorie)
            {
                return categorie.Entity == theEntityName;
            });


            foreach (cat_ToDoCategorie categorie in result)
            {
                categorie.ToDoPredefined = todoPredefined_CRUD.readByParentID(categorie.id);
            }

            return result;
        }

        public override void addParameters(cat_ToDoCategorie entity, ref Data_Base_MNG.SQL DM)
        {
            throw new NotImplementedException();
        }
    }

    class ToDoPredefined_CRUD : super_CRUD<cat_PredefinedToDo>
    {
        public ToDoPredefined_CRUD()
        {
            query_GetAll = "";
            query_GetByParent = "SELECT PredefinedToDoKey, ToDoCategoryKey, Value " +
                                "FROM cat_PredefinedToDo " +
                                "WHERE (ToDoCategoryKey = @key)";
        }

        public override cat_PredefinedToDo entityFromTableRow(DataRow row)
        {
            cat_PredefinedToDo entity = new cat_PredefinedToDo();
            entity.id = long.Parse(row[0].ToString());
            entity.ToDoCategoryKey = int.Parse(row[1].ToString());
            entity.Value = row[2].ToString();
            return entity;
        }

        public override void addParameters(cat_PredefinedToDo entity, ref Data_Base_MNG.SQL DM)
        {
            throw new NotImplementedException();
        }
    }
}
