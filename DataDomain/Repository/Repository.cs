namespace StoreService.DataDomain
{
    public interface IRepository<T> { }
    public class Repositiry<T> : IRepository<T> where T : class
    {
        protected int cmdTimeout = 120;
        protected DbFactory dbFactory;
        public Repositiry()
        {
            dbFactory = new SqlDbFactory();
        }
    }
}
