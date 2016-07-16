using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Reusable
{
    public class UserLogic : BaseLogic<User>
    {
        public UserLogic(DbContext context, IRepository<User> repository) : base(context, repository)
        {
        }

        protected override void loadNavigationProperties(DbContext context, IList<User> entities)
        {
            //Empty
        }



        //Specific for UserLogic
        public CommonResponse GetByName(string sName)
        {
            CommonResponse response = new CommonResponse();
            List<User> entities = new List<User>();
            try
            {
                User entity = repository.GetSingle(e => e.UserName == sName);
                if (entity != null)
                {
                    entities.Add(entity);
                    loadNavigationProperties(context, entities);
                }
                return response.Success(entity);

            }
            catch (Exception e)
            {
                return response.Error("ERROR: " + e.ToString());
            }
        }
    }
}
