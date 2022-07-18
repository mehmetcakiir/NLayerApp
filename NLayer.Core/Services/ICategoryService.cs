using NLayer.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Services
{
    public interface ICategoryService : IService<Category>
    {
        //Bu bölümde kısımda özelleştirilmiş Dto dönülür.
        public Task<CustomResponseDto<CategoryWithProductDto>> GetSingleCategoryByIdWithProductAsync(int categoryId);
    }
}
