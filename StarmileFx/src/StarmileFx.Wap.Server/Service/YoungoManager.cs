using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarmileFx.Models.Redis;
using StarmileFx.Wap.Server.IService;

namespace StarmileFx.Wap.Server.Service
{
    public class YoungoManager : IYoungoServer
    {
        public bool AddShopCart(int CustomerID, Dictionary<string, int> productList)
        {
            throw new NotImplementedException();
        }

        public bool AddShopCartProduct(int CustomerID, string ProductID)
        {
            throw new NotImplementedException();
        }

        public bool DeleteShopCart(int CustomerID)
        {
            throw new NotImplementedException();
        }

        public ShopCart GetShopCart(int CustomerID)
        {
            throw new NotImplementedException();
        }

        public bool ModifyShopCart(int CustomerID, Dictionary<string, int> productList)
        {
            throw new NotImplementedException();
        }
    }
}
