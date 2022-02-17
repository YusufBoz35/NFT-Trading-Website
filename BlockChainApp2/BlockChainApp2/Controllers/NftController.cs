using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlockChainApp2.Models;
using BlockChainApp2.Models.EntityFramework;
using BlockChainApp2.ViewModels;

namespace BlockChainApp2.Controllers
{
    public class NftController : Controller
    {
        BlockChainEntities BlockChain = new BlockChainEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details(string id)
        {
            var model = BlockChain.nfts.Find(id);
            if (model == null)
            {
                ViewBag.Message = "Nft Bulunamadı!";
                return View();
            }
            return View(model);
        }
        [Authorize]
        public ActionResult List()
        {
            var model = BlockChain.nfts.ToList();
            return View(model);
        }
        [Authorize]
        public ActionResult Edit(string id)
        {
            var model = BlockChain.nfts.Find(id);
            if (model == null)
            {
                ViewBag.Message = "Nft Bulunamadı!";
                return View();
            }
            return View(model);

        }
        [Authorize]
        public ActionResult Add()
        {
            var model = new NftFormViewModel()
            {
                Categories = BlockChain.categories.ToList()
            };
            return View(model);
        }

        [Authorize]
        public ActionResult Save(NftFormViewModel model)
        {
            BlockChain.nfts.Add(model.Nft);

            blockchain_func islem = new blockchain_func();
            DateTime localDate = DateTime.Now;
            string b_data = model.Nft.nft_nick + "-" + model.Nft.user_nick;
            string deger = islem.CalculateHash(localDate, "", b_data);

            var Blockchain = new blockchain();
            Blockchain.nft_nick = model.Nft.nft_nick;
            Blockchain.chain_safe = true;
            BlockChain.blockchains.Add(Blockchain);

            var Block = new block();
            Block.index_i = 0;
            Block.time_stamp = localDate;
            Block.previous_hash = "";
            Block.hash = deger;
            Block.data = b_data;
            Block.nft_nick = model.Nft.nft_nick;
            BlockChain.blocks.Add(Block);

            BlockChain.SaveChanges();
            ViewBag.Message("Kayıt Eklendi");
            return RedirectToAction("User", "AdminDasboard");
        }

        public ActionResult Buy(nft model)
        {
            //var user = (BlockChainApp2.Models.EntityFramework.login)Session["user"];
            //if (user == null)
            //{
            //    ViewBag.Message = "Lütfen giriş yapın !";
            //    return View();
            //}
            //else
            //{
            //    model.user_nick = user.user_nick;
            //    BlockChain.NFTKimeAit(model.nft_nick);
            //}
            return View();
        }
    }
}