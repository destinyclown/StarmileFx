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

        Task<ResponseResult<bool>> AddProduct(Product product);

        Task<ResponseResult<bool>> ModifyProduct(Product product);

        Task<ResponseResult<Product>> GetProduct(int Id);

        Task<ResponseResult<bool>> BatchDeleteProduct(int[] Ids);

        Task<ResponseResult<bool>> DeleteProduct(int Id);

    }
}
