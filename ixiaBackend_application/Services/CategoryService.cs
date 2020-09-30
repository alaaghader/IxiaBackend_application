using AutoMapper;
using ixiaBackend_application.Models;
using ixiaBackend_application.Models.Entities;
using ixiaBackend_application.Models.ModelsView;
using ixiaBackend_application.ModelsInput;
using ixiaBackend_application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ixiaBackend_application.Services
{
    public class CategoryService : ICategoryService
    {
        public readonly IxiaContext _context;
        public readonly IMapper _mapper;

        public CategoryService(IxiaContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<bool>> AddCategoryAsync(CategoryInput categoryInput)
        {
            Category category = new Category
            {
                Description = categoryInput.Description,
                Name = categoryInput.Name,
            };
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Result<List<CategoryView>>> GetAllCategoriesAsync()
        {
            var result = await (from categories in _context.Categories
                                select _mapper.Map(categories, new CategoryView { }))
                                .ToListAsync();
            return result;
        }
    }
}
