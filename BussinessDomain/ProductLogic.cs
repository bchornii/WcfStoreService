using StoreService.BussinesObjectAccess;
using StoreService.DataDomain;
using StoreService.ServiceObjectAccess;
using StoreService.ServiceExtensions;
using System;
using System.ServiceModel;

namespace StoreService.BussinessDomain
{
    public interface IProductLogic
    {
        Product GetProduct(int id);
        bool UpdateProduct(Product product, ref string message);
    }
    public class ProductLogic : IProductLogic
    {
        IProductRepository productRepository;
        public ProductLogic()
        {
            productRepository = new ProductRepository();
        }
        public Product GetProduct(int id)
        {
            ProductBDO product = null;
            try
            {
                product = productRepository.GetProduct(id);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                string reason = "GetProduct Exception";
                throw new FaultException<ProductFault>(new ProductFault(msg), reason);
            }
            
            if (product == null)
            {
                string msg = string.Format("No product found for id {0}", id);
                string reason = "GetProduct empty product";
                if (id == 999)
                {
                    throw new Exception(msg);
                }
                else
                {
                    throw new FaultException<ProductFault>(new ProductFault(msg), reason);
                }
            }
            return product.ToClientProduct();
        }

        public bool UpdateProduct(Product product, ref string message)
        {            
            // Check parameters from client
            if (product.UnitPrice <= 0)
            {
                message = "Price cannot be < 0";
                return false;
            }
            else if (string.IsNullOrEmpty(product.ProductName))
            {
                message = "Product name cannot be empty";
                return false;
            }
            else if (string.IsNullOrEmpty(product.QuantityPerUnit))
            {
                message = "Quantity cannot be empty";
                return false;
            }
            else
            {
                ProductBDO productInDB = GetProduct(product.ProductId).ToServiceProduct();
                // invalid product to update
                if (productInDB == null)
                {
                    message = "cannot get product for this ID";
                    return false;
                }
                // a product can't be discontinued
                // if there are non-fulfilled orders
                if (product.Discontinued == true && productInDB.UnitsOnOrder > 0)
                {
                    message = "cannot discontinue this product";
                    return false;
                }
                else
                {
                    return productRepository.UpdateProduct(product.ToServiceProduct(), ref message);
                }
            }            
        }
    }
}
