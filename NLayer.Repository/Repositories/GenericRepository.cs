﻿using Microsoft.EntityFrameworkCore;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        // Readonly verilmesinin sebebi değer atamalarının sadece consturation da yapılmasını sağlamaktır.
        protected readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        
        public async Task AddAsycn(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsycn(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<bool> AnyAsycn(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }


        //AsNoTracking() methodu verileri memory alıp takip etmesini engeller
        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking().AsQueryable();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }


        //Remove methodunun asycn methodu bulunmuyor
        public  void Remove(T entity)
        {
             _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public  IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return  _dbSet.Where(expression);
        }
    }
}
