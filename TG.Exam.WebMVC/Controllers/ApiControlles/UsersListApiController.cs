using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ModelUser = TG.Exam.WebMVC.Models;

namespace TG.Exam.WebMVC.Controllers
{
    public class UsersListApiController : ApiController
    {
        public IEnumerable<ModelUser.UserDataModel> Get()
        {
            var users = ModelUser.User.GetAll();
            users.ForEach(p => p.Age += 10);

            var data = users
                .Select(p => new ModelUser.UserDataModel()
                {
                    UserData = p,
                    FetchMethod = "Async"
                })
                .ToArray();

            return data;
        }
    }
}
