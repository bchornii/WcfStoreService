using StoreService.BussinesObjectAccess;
using System;
using System.Data;

namespace StoreService.DataDomain
{
    public interface IProductRepository
    {
        ProductBDO GetProduct(int id);
        bool UpdateProduct(ProductBDO product, ref string message);
    }
    public class ProductRepository : Repositiry<ProductRepository>,IProductRepository
    {
        public ProductBDO GetProduct(int id)
        {
            ProductBDO product = null; // new ProductBDO();
            using(var conn = dbFactory.GetDbConnection())
            {
                using(var cmd = dbFactory.GetDbCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = cmdTimeout;
                    cmd.CommandText = "SELECT * FROM TSQLFundamentals2008.Production.Products AS p WHERE p.productid = @prodid";

                    cmd.Parameters.Add(dbFactory.CreateInParameter("@prodid", DbType.Int32, id));
                    conn.Open();

                    using (IDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            product = new ProductBDO();              
                            product.ProductId = id;
                            product.ProductName = rdr["productname"].ToString();
                            product.UnitPrice = Convert.ToDecimal(rdr["unitprice"]);
                            product.Discontinued = Convert.ToBoolean(rdr["discontinued"]);
                        }                         
                    }
                }
            }
            return product;
        }

        public bool UpdateProduct(ProductBDO product, ref string message)
        {
            bool res = true;
            message = "product was updated successfully";
            using (var conn = dbFactory.GetDbConnection())
            {
                using (var cmd = dbFactory.GetDbCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "UpsertProduct";
                    cmd.CommandTimeout = cmdTimeout;

                    cmd.Parameters.Add(dbFactory.CreateInParameter("@ProductId", DbType.Int32, product.ProductId));
                    cmd.Parameters.Add(dbFactory.CreateInParameter("@ProductName", DbType.String, product.ProductName));
                    cmd.Parameters.Add(dbFactory.CreateInParameter("@SupplierId", DbType.Int32, 1));
                    cmd.Parameters.Add(dbFactory.CreateInParameter("@CategoryId", DbType.Int32, 1));
                    cmd.Parameters.Add(dbFactory.CreateInParameter("@UnitPrice", DbType.Decimal, product.UnitPrice));
                    cmd.Parameters.Add(dbFactory.CreateInParameter("@Discounted", DbType.Boolean, product.Discontinued));

                    conn.Open();

                    if(cmd.ExecuteNonQuery() != 1)
                    {
                        res = false;
                        message = "product was not updated";
                    }
                }
            }
            return res;
        }
    }
}
