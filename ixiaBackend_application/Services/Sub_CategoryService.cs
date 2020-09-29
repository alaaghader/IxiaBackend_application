using AutoMapper;
using ixiaBackend_application.Models;
using ixiaBackend_application.Models.ModelsView;
using ixiaBackend_application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ixiaBackend_application.Services
{
    public class Sub_CategoryService : ISub_CategoriesService
    {
        public readonly IxiaContext ixiaContext;
        public readonly IMapper _mapper;

        public Sub_CategoryService(IxiaContext ixiaContext,
            IMapper mapper) 
        {
            this.ixiaContext = ixiaContext;
            _mapper = mapper;
        }

        public async Task<Result<List<Sub_CategoryView>>> GetSubCategoryAsync(int categoryId)
        {
            var result = await (from subcategories in ixiaContext.Sub_Categories
                                where subcategories.Category.Id == categoryId
                                select _mapper.Map(subcategories, new Sub_CategoryView { 
                                    Category = _mapper.Map(subcategories.Category, new CategoryView { }),
                                }))
                                .ToListAsync();

            return result;
        }
    }
}
