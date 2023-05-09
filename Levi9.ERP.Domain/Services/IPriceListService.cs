﻿using Levi9.ERP.Domain.Models;
using Levi9.ERP.Domain.Models.DTO;

namespace Levi9.ERP.Domain.Services
{
    public interface IPriceListService
    {
        Task<PriceListDTO> GetByIdAsync(int id);
        Task<IEnumerable<PriceListDTO>> GetAllPricesLists();
    }
}