using NLayer.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Services
{
    public interface IProductService : IService<Product>
    {
        //Bu bölümde kısımda özelleştirilmiş Dto dönülür.
        Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductWithCatagory();
    }
}
