using System;

namespace Company.WebAPI.Models.Common.Products
{
    [Serializable]
    public class Product
    {
        /// <summary>
        /// 商品代碼
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 商品名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 商品原價
        /// </summary>
        public decimal OriginalPrice { get; set; }

        /// <summary>
        /// 商品售價
        /// </summary>
        public decimal SalePrice { get; set; }
    }
}
