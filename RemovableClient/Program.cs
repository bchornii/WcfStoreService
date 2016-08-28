using Svc = RemovableClient.SS.ProductServiceRef;
using System;
using System.ServiceModel;

namespace RemovableClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //Svc.ProductServiceClient client = new Svc.ProductServiceClient();
            //Svc.Product p = client.GetProduct(12);
            //Console.WriteLine("Product id = {0}", p.ProductId);
            //Console.WriteLine("Product name = {0}", p.ProductName);
            //Console.WriteLine("Product price = {0}",p.UnitPrice.ToString());
            
            Svc.ProductServiceClient client = new Svc.ProductServiceClient();

            // Fault exception
            TestException(client, 0);

            // regular exception
            TestException(client, 999);                      

            Console.Read();
        }

        static void TestException(Svc.ProductServiceClient client,int id)
        {
            if(id != 999)
            {
                Console.WriteLine("\n\nTest Fault Exception");
            }
            else
            {
                Console.WriteLine("\n\nTest normal Exception");
            }

            try
            {
                Svc.Product p = client.GetProduct(id);
            }
            catch(TimeoutException ex)
            {
                Console.WriteLine("Timeout exception");
            }
            catch(FaultException<Svc.ProductFault> ex)
            {
                Console.WriteLine("Product fault.");
                Console.WriteLine("\nFault reason: " + ex.Reason);
                Console.WriteLine("\nFault message: " + ex.Detail.FaultMessage);                
            }
            catch(FaultException ex)
            {
                Console.WriteLine("Unknown fault (fault exception).");
                Console.WriteLine(ex.Message);
            }
            catch(CommunicationException ex)
            {
                Console.WriteLine("Communication exception");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Unknown exception");
            }
        }
    }
}
