using BlockChainApp2.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlockChainApp2.ViewModels
{
    public class NftFormViewModel
    {
        public IEnumerable<category> Categories { get; set; }
        public nft Nft { get; set; }
    }
}