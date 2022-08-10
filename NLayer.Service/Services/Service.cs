using Microsoft.EntityFrameworkCore;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly IGenericRepository<T> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public Service(IGenericRepository<T> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }


        public async Task<T> AddAsycn(T entity)
        {
            await _repository.AddAsycn(entity);
            await _unitOfWork.CommitAsycn();
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsycn(IEnumerable<T> entities)
        {
            await _repository.AddRangeAsycn(entities);
            await _unitOfWork.CommitAsycn();
            return entities;
        }

        public async Task<bool> AnyAsycn(Expression<Func<T, bool>> expression)
        {
            return await _repository.AnyAsycn(expression);
        }

        public async Task<IEnumerable<T>> GetAllAsycn()
        {
            return await _repository.GetAll().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var hasProduct = await _repository.GetByIdAsync(id);

            if (hasProduct == null)
            {
                throw new NotFountException($"{typeof(T).Name} ({id}) not fount");
                
            }
            return hasProduct;
        }

        public async Task Remove(T entity)
        {
            _repository.Remove(entity);
            await _unitOfWork.CommitAsycn();
        }

        public async Task RemoveRange(IEnumerable<T> entities)
        {
            _repository.RemoveRange(entities);
            await _unitOfWork.CommitAsycn();
        }

        public async Task Update(T entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsycn();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _repository.Where(expression);
        }
    }
}
