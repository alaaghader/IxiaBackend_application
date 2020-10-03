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
    public class TypeService : ITypeService
    {
        public readonly IxiaContext ixiaContext;
        public readonly IMapper _mapper;

        public TypeService(IxiaContext ixiaContext,
            IMapper mapper)
        {
            this.ixiaContext = ixiaContext;
            _mapper = mapper;
        }

        public async Task<Result<List<TypeView>>> GetTypeAsync(int subCategoryId)
        {
            var result = await (from types in ixiaContext.Types
                               where types.Sub_Category.Id == subCategoryId
                               select _mapper.Map(types, new TypeView {
                                    Sub_Category = _mapper.Map(types.Sub_Category, new Sub_CategoryView {
                                        Category = _mapper.Map(types.Sub_Category.Category, new CategoryView { }),
                                    }),
                               }))
                    .ToListAsync();

            return result;
        }

        public async Task<Result<List<TypeView>>> GetAllTypesAsync() 
        {
            var result = await (from types in ixiaContext.Types
                                select _mapper.Map(types, new TypeView {
                                    Sub_Category = _mapper.Map(types.Sub_Category, new Sub_CategoryView
                                    {
                                        Category = _mapper.Map(types.Sub_Category.Category, new CategoryView { }),
                                    }),
                                })).ToListAsync();

            return result;
        }
    }
}
