using System.Linq;
using System.Web.Mvc;
using ModelUser = TG.Exam.WebMVC.Models;


namespace TG.Exam.WebMVC.Controllers
{
    public class UsersListController : Controller
    {
        public ActionResult Index()
        {
            var model = ModelUser.User.GetAll()
                .Select(u => new ModelUser.UserDataModel()
                {
                    UserData = u,
                    FetchMethod = "Sync"
                })
                .ToArray();

            return View(model);

        }
    }
}
