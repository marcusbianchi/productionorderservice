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
    public class ProductionOrderService : IProductionOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IRecipeService _recipeService;
        public ProductionOrderService(ApplicationDbContext context, IRecipeService recipeService)
        {
            _context = context;
            _recipeService = recipeService;
        }

        public async Task<ProductionOrder> addProductionOrder(ProductionOrder productionOrder)
        {
            var recipe = await _recipeService.getRecipe(productionOrder.recipe.recipeId);
            productionOrder.recipe = recipe;
            _context.ProductionOrders.Add(productionOrder);
            await _context.SaveChangesAsync();
            return productionOrder;
        }

        public async Task<ProductionOrder> deleteProductionOrder(int productionOrderId)
        {
            var productionOrder = await _context.ProductionOrders
                               .Where(x => x.productionOrderId == productionOrderId)
                               .FirstOrDefaultAsync();
            if (productionOrder != null)
            {
                _context.Entry(productionOrder).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
            return productionOrder;
        }

        public async Task<ProductionOrder> getProductionOrder(int productionOrderId)
        {
            var productionOrder = await _context.ProductionOrders
                                        .Include(x => x.recipe)
                                        .Include(x => x.recipe.phases)
                                        .Include(x => x.recipe.recipeProduct)
                                        .Include(x => x.recipe.recipeProduct.product)
                                        .Include("recipe.phases.phaseProducts")
                                        .Include("recipe.phases.phaseParameters")
                                        .Include("recipe.phases.phaseProducts.product")
                                        .Where(x => x.productionOrderId == productionOrderId)
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync();
            return productionOrder;
        }

        public async Task<List<ProductionOrder>> getProductionOrders(int startat, int quantity)
        {
            var productionOrders = await _context.ProductionOrders
                                            .OrderBy(x => x.productionOrderId)
                                            .Include(x => x.recipe)
                                            .Include(x => x.recipe.phases)
                                            .Include(x => x.recipe.recipeProduct)
                                            .Skip(startat).Take(quantity)
                                            .ToListAsync();
            return productionOrders;
        }

        public async Task<ProductionOrder> updateProductionOrder(int productionOrderId, ProductionOrder productionOrder)
        {
            var curProductionOrder = await getProductionOrder(productionOrderId);
            if (productionOrderId != productionOrder.productionOrderId || curProductionOrder == null)
            {
                return null;
            }
            productionOrder.recipe = curProductionOrder.recipe;
            _context.ProductionOrders.Update(productionOrder);
            await _context.SaveChangesAsync();
            return productionOrder;
        }
    }
}