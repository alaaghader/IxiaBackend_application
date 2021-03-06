﻿using ixiaBackend_application.Models.ModelsView;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ixiaBackend_application.Services.Interfaces
{
    public interface IPriceService
    {
        Task<Result<PriceView>> AddPriceAsync(int prodiId, int countryId, int currencyId, double price);
        Task<Result<List<PriceView>>> GetPricesByCountry(string countryName, string userId);
        Task<Result<List<PriceView>>> SearchPricesByCountry(string countryName, string prodName, string userId);
        Task<Result<PriceView>> GetProductDetailsByCountryAsync(int id, string userId, string countryName);
        Task<Result<List<PriceView>>> GetPricesByCountryAndCategory(string countryName, string userId, int categoryId);
        Task<Result<List<PriceView>>> GetPricesByCountryAndSubCategory(string countryName, string userId, int subCategoryId);
        Task<Result<List<PriceView>>> GetRecommendedProducts(int prodId, string userId, string countryName);
    }
}
