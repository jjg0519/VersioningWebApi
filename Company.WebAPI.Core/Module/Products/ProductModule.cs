using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.WebAPI.Models.Common.Products;

namespace Company.WebAPI.Core.Module.Products
{
    public class ProductModule : IProductModule
    {
        private List<Product> _products;

        public ProductModule()
        {
            _products = new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Name = "Book",
                    OriginalPrice = 400,
                    SalePrice = 450
                },
                new Product()
                {
                    Id = 2,
                    Name = "Usb type-c",
                    OriginalPrice = 450,
                    SalePrice = 490
                },
                new Product()
                {
                    Id = 3,
                    Name = "Cup",
                    OriginalPrice = 210,
                    SalePrice = 230
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
