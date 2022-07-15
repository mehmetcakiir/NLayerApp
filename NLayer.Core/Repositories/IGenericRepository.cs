using System.Linq.Expressions;

namespace NLayer.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {

        //Premetre olarak verilen id değerine göre
        Task<T> GetByIdAsync(int id);

        IQueryable<T> GetAll();

        //productList.Where(x=>x.productId > 10). "ordeyBy".toList() denildiği zaman veri tabanına sorgu atılır.
        IQueryable<T> Where(Expression<Func<T, bool>> expression);

        //Paremetre olarak gönderilen nesnenin veri tabanında varlığını kontrol eder
        Task<bool> AnyAsycn(Expression<Func<T, bool>> expression);

        //Tek kayıt eklenir
        Task AddAsycn(T entity);

        //Birden fazla kayıt eklenir
        Task AddRangeAsycn(IEnumerable<T> entities);

        //Update işlemi öncelikle memory de ilgili kaydın status u modified yapılır. Veri tabanına gidilmediği için asekron method değildir.
        void Update(T entity);

        //Remove işlemi öncelikle memory de ilgili kaydın status u modified yapılır. Veri tabanına gidilmediği için asekron method değildir.
        void Remove(T entity);

        //List<T> yerine IEnumerable<T> kullanılmıştır
        void RemoveRange(IEnumerable<T> entities);
    }
}
