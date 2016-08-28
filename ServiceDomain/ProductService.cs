using StoreService.ServiceObjectAccess;
using StoreService.BussinessDomain;
using System;
using System.ServiceModel;

namespace StoreService.ServiceDomain
{
    public class ProductService : IProductService
    {
        IProductLogic plogic;
        public ProductService()
        {
            plogic = new ProductLogic();
        }
        public Product GetProduct(int id)
        {
            return plogic.GetProduct(id);
        }

        public bool UpdateProduct(Product product, ref string message)
        {
            return plogic.UpdateProduct(product, ref message);
        }
    }
}
