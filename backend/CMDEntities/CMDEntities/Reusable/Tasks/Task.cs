using CMDEntities.Reusable.Super;
using CMDEntities.Reusable.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDEntities.Reusable.Tasks
{
    class Task : IEntity
    {
        public DateTime? CreatedDate { get; set; }

        //FROM ToDo
        public List<ToDo> ToDo { get; set; }
    }

    class ToDo : IEntity
    {
        public long TaskKey { get; set; }
        public int? CategoryKey { get; set; }
        public bool IsDone { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public long AssignedTo { get; set; }
        public string Status { get; set; }
        public long AssignedBy { get; set; }
        public bool IsImportant { get; set; }

        //FROM cat_ToDoCategorie
        public string Category { get; set; }
    }

    class cat_ToDoCategorie : IEntity
    {
        public string Value { get; set; }
        public string Entity { get; set; }

        //FROM cat_PredefinedToDo
        public List<cat_PredefinedToDo> ToDoPredefined { get; set; }
    }

    class cat_PredefinedToDo : IEntity
    {
        public int ToDoCategoryKey { get; set; }
        public string Value { get; set; }
    }

    class TaskCatalogs
    {
        public List<User> User { get; set; }
    }
}
