using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using productionorderservice.Data;
using productionorderservice.Model;
using productionorderservice.Services.Interfaces;

namespace productionorderservice.Services
{
    public class ProductionOrderTypeService : IProductionOrderTypeService
    {
        private readonly ApplicationDbContext _context;
        public ProductionOrderTypeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProductionOrderType> addProductionOrderType(ProductionOrderType productionOrderType)
        {
            _context.ProductionOrderTypes.Add(productionOrderType);
            await _context.SaveChangesAsync();
            return productionOrderType;
        }

        public async Task<ProductionOrderType> deleteProductionOrderType(int productionOrderTypeId)
        {

            var productionOrderType = await _context.ProductionOrderTypes
                    .Where(x => x.productionOrderTypeId == productionOrderTypeId)
                    .FirstOrDefaultAsync();
            if (productionOrderType != null)
            {
                _context.Entry(productionOrderType).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
            return productionOrderType;
        }

        public async Task<ProductionOrderType> getProductionOrderType(int productionOrderTypeId)
        {
            var productionOrderType = await _context.ProductionOrderTypes
                             .Where(x => x.productionOrderTypeId == productionOrderTypeId)
                             .FirstOrDefaultAsync();
            return productionOrderType;
        }

        public async Task<List<ProductionOrderType>> getProductionOrderTypes(int startat, int quantity)
        {
            var productionOrderTypes = await _context.ProductionOrderTypes
                                .OrderBy(x => x.productionOrderTypeId)
                                .Skip(startat).Take(quantity)
                                .ToListAsync();
            return productionOrderTypes;
        }

        public async Task<ProductionOrderType> updateProductionOrderType(int productionOrderTypeId, ProductionOrderType productionOrderType)
        {

            if (productionOrderTypeId != productionOrderType.productionOrderTypeId || productionOrderType == null)
            {
                return null;
            }

            _context.ProductionOrderTypes.Update(productionOrderType);
            await _context.SaveChangesAsync();
            return productionOrderType;
        }
    }
}