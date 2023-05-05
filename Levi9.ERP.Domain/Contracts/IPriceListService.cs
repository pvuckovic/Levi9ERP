﻿using Levi9.ERP.Domain.Model;
using Levi9.ERP.Domain.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Levi9.ERP.Domain.Contracts
{
    public interface IPriceListService
    {
        Task<PriceListDTO> GetByIdAsync(int id);
    }
}
