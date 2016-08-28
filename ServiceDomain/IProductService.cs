using StoreService.ServiceObjectAccess;
using System.ServiceModel;

namespace StoreService.ServiceDomain
{
    [ServiceContract]
    public interface IProductService
    {
        [OperationContract]
        [FaultContract(typeof(ProductFault))]
        Product GetProduct(int id);

        [OperationContract]
        [FaultContract(typeof(ProductFault))]
        bool UpdateProduct(Product product, ref string message);        
    }   
}
