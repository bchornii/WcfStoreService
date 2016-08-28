using System.Configuration;
using System.Data;
using System.Data.Common;

namespace StoreService.DataDomain
{
    public abstract class DbFactory
    {
        protected string cs = null;
        public DbFactory()
        {
            cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        }
        public abstract DbConnection GetDbConnection();
        public abstract DbCommand GetDbCommand();
        public abstract DbParameter CreateInParameter(string name, DbType dbType, object value);
    }
}
