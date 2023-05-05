using Levi9.ERP.Domain.Model;
using Levi9.ERP.Domain.Model.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Levi9.ERP.Domain.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataBaseContext _dataBaseContext;

        public ProductRepository(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }

        public async Task<ProductDTO> AddProductAsync(Product product)
        {
            _dataBaseContext.Products.Add(product);
            await _dataBaseContext.SaveChangesAsync();
            var productDTO = await _dataBaseContext.Products
            .Include(p => p.ProductDocuments)
            .Include(p => p.Prices)
            .ThenInclude(pr => pr.PriceList)
            .Where(p => p.Id == product.Id)
            .Select(p => new ProductDTO
            {
                Id = p.Id,
                GlobalId = p.GlobalId,
                Name = p.Name,
                ImageUrl = p.ImageUrl,
                AvailableQuantity = p.AvailableQuantity,
                LastUpdate = p.LastUpdate,
                PriceList = p.Prices.Select(pr => new PriceDTO
                {
                    Id = pr.PriceList.Id,
                    GlobalId = pr.PriceList.GlobalId,
                    Price = pr.PriceValue,
                    Currency = pr.Currency,
                    LastUpdate = pr.LastUpdate
                }).ToList()
            })
            .FirstOrDefaultAsync();

            return productDTO;
        }

        public async Task<Product> GetProductByName(string name)
        {
            return await _dataBaseContext.Products.FirstOrDefaultAsync(p => p.Name == name);
        }
    }
}
