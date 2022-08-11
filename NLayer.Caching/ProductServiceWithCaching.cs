using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
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
                //Tüm datayı al
                _memortCache.Set(CacheProductKey, _repository.GetAll().ToList());
            }
        }


        public Task<Product> AddAsycn(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> AddRangeAsycn(IEnumerable<Product> entities)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsycn(Expression<Func<Product, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllAsycn()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductWithCatagory()
        {
            throw new NotImplementedException();
        }

        public Task Remove(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRange(IEnumerable<Product> entities)
        {
            throw new NotImplementedException();
        }

        public Task Update(Product entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
