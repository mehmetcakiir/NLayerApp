using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NLayer.Core;
using NLayer.Core.DTOs;
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

namespace NLayer.Caching
{
    public class ProductServiceWithCaching : IProductService
    {
        private const string CacheProductKey = "productsCache" ;

        private readonly IMapper _mapper;

        private readonly IMemoryCache _memortCache;

        private readonly IProductRepository _repository;

        private readonly IUnitOfWork _unitOfWork;

        public ProductServiceWithCaching(IUnitOfWork unitOfWork, IProductRepository repository, IMemoryCache memortCache, IMapper mapper = null)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _memortCache = memortCache;
            _mapper = mapper;


            // Uygulama ilk ayağa kalktığında Key e sahip data yok ise
            if (!_memortCache.TryGetValue(CacheProductKey, out _))
            {
                //Tüm datayı al !(constructor da asekron olamayacağı için Result ekleyerek senkron hale getirildi.)
                _memortCache.Set(CacheProductKey, _repository.GetProductWithCatagory().Result);
            }
        }


        public async Task<Product> AddAsycn(Product entity)
        {
            //Öncelikle db ye eklenir
            await _repository.AddAsycn(entity);

            //Veri tabanına kaydedilip yansıtılır.
            await _unitOfWork.CommitAsycn();

            //Verileri cache tekrar çeker
            await CashAllProductsAsync();
            return entity;
        }

        public async Task<IEnumerable<Product>> AddRangeAsycn(IEnumerable<Product> entities)
        {
            await _repository.AddRangeAsycn(entities);

            await _unitOfWork.CommitAsycn();

            await CashAllProductsAsync();
            return entities;
        }

        public Task<bool> AnyAsycn(Expression<Func<Product, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllAsycn()
        {
           return Task.FromResult(_memortCache.Get<IEnumerable<Product>>(CacheProductKey));
        }

        public Task<Product> GetByIdAsync(int id)
        {
            var product = _memortCache.Get<List<Product>>(CacheProductKey).FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                throw new NotFountException($"{typeof(Product).Name} ({id}) not fount");
            }

            return Task.FromResult(product);
        }

        public Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductWithCatagory()
        {
            var product = _memortCache.Get<IEnumerable<Product>>(CacheProductKey);

            var productWithCategoryDto = _mapper.Map<List<ProductWithCategoryDto>>(product);

            return Task.FromResult(CustomResponseDto<List<ProductWithCategoryDto>>.Succes(200, productWithCategoryDto));
        }

        public async Task Remove(Product entity)
        {
             _repository.Remove(entity);
            await _unitOfWork.CommitAsycn();
            await CashAllProductsAsync();
        }

        public async Task RemoveRange(IEnumerable<Product> entities)
        {
            _repository.RemoveRange(entities);
            await _unitOfWork.CommitAsycn();
            await CashAllProductsAsync();

        }

        public async Task Update(Product entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsycn();
            await CashAllProductsAsync();
        }

        public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
        {
            return _memortCache.Get<List<Product>>(CacheProductKey).Where(expression.Compile()).AsQueryable();
        }



        public async Task CashAllProductsAsync()
        {
            //Chach e tüm verileri çeker
            _memortCache.Set(CacheProductKey, await _repository.GetAll().ToListAsync());
        }
    }
}
