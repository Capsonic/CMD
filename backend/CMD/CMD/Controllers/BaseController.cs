using CMD.Auth;
using CMDLogic.Logic;
using Reusable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CMD.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public abstract class BaseController<Entity> : ApiController where Entity: BaseEntity
    {
        IBaseLogic<Entity> _logic;

        public BaseController(IBaseLogic<Entity> logic)
        {
            _logic = logic;
        }

        // GET: api/Base
        public CommonResponse Get()
        {
            LoggedUser loggedUser = new LoggedUser((ClaimsIdentity)User.Identity);
            _logic.byUserId = loggedUser.UserID;

            return _logic.GetAll();
        }

        // GET: api/Base/5
        public CommonResponse Get(int id)
        {
            LoggedUser loggedUser = new LoggedUser((ClaimsIdentity)User.Identity);
            _logic.byUserId = loggedUser.UserID;
            _logic.byUserId = 2;

            return _logic.GetByID(id);
        }

        // POST: api/Base
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Base/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Base/5
        public CommonResponse Delete(int id)
        {
            LoggedUser loggedUser = new LoggedUser((ClaimsIdentity)User.Identity);
            _logic.byUserId = loggedUser.UserID;
            
            return _logic.Remove(id);
        }
    }
}
