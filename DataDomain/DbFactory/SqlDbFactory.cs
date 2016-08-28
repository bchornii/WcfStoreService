using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace StoreService.DataDomain
{
    public class SqlDbFactory : DbFactory
    {
        public override DbParameter CreateInParameter(string name, DbType dbType, object value)
        {
            return new SqlParameter
            {
                ParameterName = name,
                DbType = dbType,
                Direction = ParameterDirection.Input,
                Value = value
            };
        }
        public override DbCommand GetDbCommand()
        {
            return new SqlCommand();
        }
        public override DbConnection GetDbConnection()
        {
            return new SqlConnection(cs);
        }
    }
}
