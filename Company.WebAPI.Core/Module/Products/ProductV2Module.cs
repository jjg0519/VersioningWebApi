using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.WebAPI.Models.Common.Products;

namespace Company.WebAPI.Core.Module.Products
{
    public class ProductV2Module : IProductModule
    {
        private List<Product> _products;
        
        public ProductV2Module()
        {
            _products = new List<Product>()
            {
                new Product()
                {
                    Id = 101,
                    Name = "Magic Mouse",
                    OriginalPrice = 2400,
                    SalePrice = 2450
                },
                new Product()
                {
                    Id = 102,
                    Name = "Macbook pro",
                    OriginalPrice = 30450,
                    SalePrice = 30490
                }
            };
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public Product Get(int id)
        {
            return _products.Find(p => p.Id == id);
        }
    }
}
