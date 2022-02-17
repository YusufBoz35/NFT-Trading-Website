using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlockChainApp2.Models;
using BlockChainApp2.Models.EntityFramework;

namespace BlockChainApp2.Controllers
{
    public class HomeController : Controller
    {
        BlockChainEntities BlockChain = new BlockChainEntities();
        public ActionResult Index()
        {
            Console.Write("asd");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Nft()
        {
            //var modelNick = BlockChain.logins.FirstOrDefault((u => u.user_nick == login.user_nick && u.account_password == login.account_password));
            var nfts = BlockChain.nfts.Where(n => n.nft_sale == true);
            var model = BlockChain.nfts.ToList();
            return View(nfts);
        }
    }
}