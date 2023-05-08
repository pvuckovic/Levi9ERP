using Levi9.ERP.Domain.Contracts;
using Levi9.ERP.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Levi9.ERP.Domain.Repository
{
    public class PriceListRepository : IPriceListRepository
    {
        private readonly DataBaseContext _dataBaseContext;
        public PriceListRepository(DataBaseContext dataBaseContext) 
        {
            _dataBaseContext = dataBaseContext;
        }
        public async Task<PriceList> GetByIdAsync(int id)
        {
            //return await _dataBaseContext.PriceLists.FirstOrDefaultAsync(p => p.Id == id);
            return await _dataBaseContext.PriceLists.FirstAsync(p => p.Id == id);
        }
    }
}
