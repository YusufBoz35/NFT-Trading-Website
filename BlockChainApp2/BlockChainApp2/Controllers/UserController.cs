using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BlockChainApp2.Models;
using BlockChainApp2.Models.EntityFramework;

namespace BlockChainApp2.Controllers
{
    public class UserController : Controller
    {
        BlockChainEntities BlockChain = new BlockChainEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(login login)
        {
            var modelNick = BlockChain.logins.FirstOrDefault((u => u.user_nick == login.user_nick && u.account_password == login.account_password));
            if (modelNick != null)
            {
                FormsAuthentication.SetAuthCookie(modelNick.user_nick, false);
                Session.Add("user", modelNick);
                return View("AdminDashboard", modelNick);
            }
            else
            {
                ViewBag.Message = "Kullanıcı Adı veya Şifre Hatalı !";
                return View();
            }

        }

        public ActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Register(login login)
        {
            if (login.account_mail == null || login.account_password == null || login.user_nick == null || login.user.user_pnumber == null || login.user.user_name == null || login.user.user_surname == null)
            {
                ViewBag.Message = "Lütfen boş alan bırakmayınız !";
                return View();
            }
            else
            {

                BlockChain.logins.Add(login);
                BlockChain.SaveChanges();
                ViewBag.Message = "Kayıt oldunuz, Lütfen giriş yapınız !";
                return View();
            }
            
        }

        [Authorize]
        public ActionResult AdminDashboard()
        {
            return View();
        }
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.RemoveAll();
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public ActionResult UserWallet(string id)
        {
            var model = BlockChain.wallets.Find(id);
            if (model == null)
            {
                ViewBag.Message = "Kullanıcı Bulunamadı !";
                return View();
            }
            return View(model);
        }
    }
}