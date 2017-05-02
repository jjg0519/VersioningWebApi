using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.WebAPI.Models.Common.Products;

namespace Company.WebAPI.Core.Module.Products
{
    public interface IProductModule
    {
        /// <summary>
        /// 取得全部商品
        /// </summary>
        /// <returns></returns>
        List<Product> GetAll();
        /// <summary>
        /// 取得單一商品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Product Get(int id);
    }
}
