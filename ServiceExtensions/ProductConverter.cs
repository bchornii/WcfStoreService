using StoreService.BussinesObjectAccess;
using StoreService.ServiceObjectAccess;

namespace StoreService.ServiceExtensions
{
    public static class ProductConverter
    {
        public static Product ToClientProduct(this ProductBDO value)
        {
            return new Product
            {
                ProductId = value.ProductId,
                ProductName = value.ProductName,
                QuantityPerUnit = value.QuantityPerUnit,
                UnitPrice = value.UnitPrice,
                Discontinued = value.Discontinued
            };
        }

        public static ProductBDO ToServiceProduct(this Product value)
        {
            return new ProductBDO
            {
                ProductId = value.ProductId,
                ProductName = value.ProductName,
                QuantityPerUnit = value.QuantityPerUnit,
                UnitPrice = value.UnitPrice,
                Discontinued = value.Discontinued
            };
        }
    }
}
