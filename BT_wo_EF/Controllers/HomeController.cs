using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BT_wo_EF.Models;
namespace BT_wo_EF.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            login obj = new login();
            return View(obj);
        }
        [HttpPost]
        public ActionResult Index(login objuserlogin)
        {
            var display = Userloginvalues().Where(m => m.UserName == objuserlogin.UserName && m.UserPassword == objuserlogin.UserPassword).FirstOrDefault();
            if (display != null)
            {
                ViewBag.Status = "CORRECT UserNAme and Password";
            }
            else
            {
                ViewBag.Status = "INCORRECT UserName or Password";
            }
            return View(objuserlogin);
        }
        public List<login> Userloginvalues()
        {
            List<login> objModel = new List<login>();
            objModel.Add(new login { UserName = "nive", UserPassword = "1234$" });
            objModel.Add(new login { UserName = "user2", UserPassword = "password2" });
            objModel.Add(new login { UserName = "user3", UserPassword = "password3" });
            objModel.Add(new login { UserName = "user4", UserPassword = "password4" });
            objModel.Add(new login { UserName = "user5", UserPassword = "password5" });
            return objModel;
        }
    }
}