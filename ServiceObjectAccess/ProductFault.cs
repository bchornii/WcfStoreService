using System.Runtime.Serialization;

namespace StoreService.ServiceObjectAccess
{
    [DataContract]
    public class ProductFault
    {
        [DataMember]
        public string FaultMessage { get; set; }
        public ProductFault(string msg)
        {
            FaultMessage = msg;
        }
    }
}
