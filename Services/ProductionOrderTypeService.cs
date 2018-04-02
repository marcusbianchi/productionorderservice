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
        private readonly IThingGroupService _thingGroupService;

        public ProductionOrderTypeService(ApplicationDbContext context,
            IThingGroupService thingGroupService)
        {
            _context = context;
            _thingGroupService = thingGroupService;

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
                    .Include(x => x.stateConfiguration)
                    .ThenInclude(x => x.states)
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
                                .Include(x => x.stateConfiguration)
                                .ThenInclude(x => x.states)
                             .FirstOrDefaultAsync();

            if (productionOrderType.thingGroupIds != null && productionOrderType.thingGroupIds.Length != 0)
            {
                var (group, status) = await _thingGroupService.getGroupsList(productionOrderType.thingGroupIds);
                if (status == HttpStatusCode.OK)
                    productionOrderType.thingGroups = group;
            }
            return productionOrderType;
        }

        public async Task<List<ProductionOrderType>> getProductionOrderTypes(int startat, int quantity)
        {
            var productionOrderTypesIds = await _context.ProductionOrderTypes
                                .OrderBy(x => x.productionOrderTypeId)
                                .Skip(startat).Take(quantity)
                                .Select(x => x.productionOrderTypeId)
                                .ToListAsync();
            List<ProductionOrderType> pOtypes = new List<ProductionOrderType>();
            foreach (var item in productionOrderTypesIds)
            {
                pOtypes.Add(await getProductionOrderType(item));
            }
            return pOtypes;
        }

        public async Task<ProductionOrderType> updateProductionOrderType(int productionOrderTypeId, ProductionOrderType productionOrderType)
        {
            var currentType = await _context.ProductionOrderTypes
                .Where(x => x.productionOrderTypeId == productionOrderTypeId)
                .Include(x => x.stateConfiguration)
                .ThenInclude(x => x.states)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if (productionOrderTypeId != productionOrderType.productionOrderTypeId
                || productionOrderType == null
                || currentType == null)
            {
                return null;
            }
            productionOrderType.stateConfiguration = currentType.stateConfiguration;

            _context.ProductionOrderTypes.Update(productionOrderType);
            await _context.SaveChangesAsync();
            return await getProductionOrderType(productionOrderTypeId);
        }


    }
}