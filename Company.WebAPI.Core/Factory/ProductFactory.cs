using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Features.Indexed;
using Company.WebAPI.Core.Module.Products;

namespace Company.WebAPI.Core.Factory
{
    public class ProductFactory : IFactory<IProductModule>
    {
        protected IProductModule _defaultModules;
        protected IIndex<string, IProductModule> _productModules;

        public ProductFactory(IProductModule defaultModules, IIndex<string, IProductModule> productModules)
        {
            _defaultModules = defaultModules;
            _productModules = productModules;
        }

        public IProductModule Instance(string version)
        {
            IProductModule module;

            if (_productModules.TryGetValue(version, out module))
                return module;

            return _defaultModules;
        }
    }
}
