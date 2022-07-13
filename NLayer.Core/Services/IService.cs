﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Services
{
    public interface IService<T> where T : class
    {
        //Premetre olarak verilen id değerine göre
        Task<T> GetByIdAsync(int id);

        Task<IEnumerable<T>> GetAll();

        //productList.Where(x=>x.productId > 10). "ordeyBy".toList() denildiği zaman veri tabanına sorgu atılır.
        IQueryable<T> Where(Expression<Func<T, bool>> expression);

        //Paremetre olarak gönderilen nesnenin veri tabanında varlığını kontrol eder
        Task<bool> AntAsycn(T entity);

        //Tek kayıt eklenir
        Task AddAsycn(T entity);

        //Birden fazla kayıt eklenir
        Task AddRangeAsycn(IEnumerable<T> entities);

        Task Update(T entity);

        Task Remove(T entity);

        //List<T> yerine IEnumerable<T> kullanılmıştır
        Task RemoveRange(IEnumerable<T> entities);
    }
}
