using StarmileFx.Models;
using StarmileFx.Models.Web;
using StarmileFx.Models.Youngo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarmileFx.Web.Server.IService
{
    public interface IYoungoServer
    {
        Task<ResponseResult<List<ProductWeb>>> GetProductList(ProductSearch search);
    }
}
