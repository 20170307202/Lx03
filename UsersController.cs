using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public ActionResult Get()
        {
            return Json(DAL.UserInfo.Instance.GetCount());
        }

        // GET api/<controller>/5
        [HttpGet("{username}")]
        public ActionResult getUser(string username)
        {
            var result = DAL.UserInfo.Instance.GetModel(username);
            if (result != null)
                return Json(Result.OK(result));
            else
                return Json(Result.Err("用户名不存在"));
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult Post([FromBody]Model.UserInfo users)
        {
            try
            {
                int n = DAL.UserInfo.Instance.Add(users);
                return Json(Result.OK("添加成功"));
            }
            catch(Exception ex)
            {
                if (ex.Message.ToLower().Contains("primary"))
                    return Json(Result.Err("用户名已存在"));
                else if(ex.Message.ToLower().Contains("null"))
                    return Json(Result.Err("用户名、密码、身份不能为空"));
                else
                    return Json(Result.Err(ex.Message));
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
