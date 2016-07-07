using CMDLogic.Logic;
using Reusable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            return _logic.GetAll();
        }

        // GET: api/Base/5
        public string Get(int id)
        {
            return "value";
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
        public void Delete(int id)
        {
        }
    }
}
