using CMDEntities.Reusable.Super;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDEntities.Reusable.Comments
{
    class Comment : IEntity
    {
        public long? ParentKey { get; set; }
        public string CommentText { get; set; }
        public DateTime? CommentDate { get; set; }
        public long CommentByUser { get; set; }

        public List<Comment> Comments { get; set; }

        //FROM user table
        public string User { get; set; }
        public string Identicon64 { get; set; }
    }
}
