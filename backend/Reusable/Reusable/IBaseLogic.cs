using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reusable
{
    public interface IBaseLogic<Entity> where Entity : BaseEntity
    {
        int? byUserId { get; set; }
        CommonResponse Add(Entity entity);
        CommonResponse GetAll();
        CommonResponse GetByID(int ID);
        CommonResponse Remove(int id);
        CommonResponse Activate(int id);
        CommonResponse Update(Entity entity);
        CommonResponse AddToParent<ParentType>(int parentID, Entity entity) where ParentType : BaseEntity;
        CommonResponse GetAllByParent<ParentType>(int parentID) where ParentType : BaseEntity;
        CommonResponse CreateInstance();
    }
}
