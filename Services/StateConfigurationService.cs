using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using productionorderservice.Data;
using productionorderservice.Model;
using productionorderservice.Services.Interfaces;
using productionorderservice.Validation;

namespace productionorderservice.Services
{
    public class StateConfigurationService : IStateConfigurationService
    {
        private readonly ApplicationDbContext _context;
        public StateConfigurationService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> deleteProductionOrderType(int productionOrderTypeId)
        {
            var productionOrderType = await _context.ProductionOrderTypes
                            .Where(x => x.productionOrderTypeId == productionOrderTypeId)
                            .FirstOrDefaultAsync();
            if (productionOrderType == null)
                return false;
            productionOrderType.stateConfiguration = null;
            _context.ProductionOrderTypes.Update(productionOrderType);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<StateConfiguration> getStateConfiguration(int productionOrderTypeId)
        {
            var productionOrderType = await _context.ProductionOrderTypes
                                       .Where(x => x.productionOrderTypeId == productionOrderTypeId)
                                       .Include(x => x.stateConfiguration)
                                       .ThenInclude(x => x.states)
                                       .FirstOrDefaultAsync();
            if (productionOrderType == null)
                return null;
            return productionOrderType.stateConfiguration;
        }

        public async Task<StateConfiguration> updateProductionOrderType(int productionOrderTypeId, StateConfiguration stateConfiguration)
        {
            var productionOrderType = await _context.ProductionOrderTypes
                          .Where(x => x.productionOrderTypeId == productionOrderTypeId)
                          .Include(x => x.stateConfiguration)
                           .ThenInclude(x => x.states)
                          .FirstOrDefaultAsync();
            if (productionOrderType == null)
                return null;
            productionOrderType.stateConfiguration = stateConfiguration;
            _context.Entry(productionOrderType).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return stateConfiguration;
        }



    }
}