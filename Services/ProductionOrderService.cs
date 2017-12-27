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
        private readonly IProductionOrderTypeService _productionOrderTypeService;
        public ProductionOrderService(ApplicationDbContext context, IRecipeService recipeService, IProductionOrderTypeService productionOrderTypeService)
        {
            _context = context;
            _recipeService = recipeService;
            _productionOrderTypeService = productionOrderTypeService;
        }

        public async Task<ProductionOrder> addProductionOrder(ProductionOrder productionOrder)
        {
            var recipe = await _recipeService.getRecipe(productionOrder.recipe.recipeId);

            productionOrder.recipe = recipe;
            _context.ProductionOrders.Add(productionOrder);
            await _context.SaveChangesAsync();
            return productionOrder;
        }

        public async Task<bool> checkProductionOrderNumber(string productionOrderNumber)
        {
            var productionOrderCount = await _context.ProductionOrders.Where(x => x.productionOrderNumber == productionOrderNumber).CountAsync();
            return productionOrderCount != 0;
        }

        public async Task<bool> checkProductionOrderType(int productionOrderTypeId)
        {
            var productionOrdertype = await _productionOrderTypeService.getProductionOrderType(productionOrderTypeId);
            return productionOrdertype != null;
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
            var productionOrderType = await _productionOrderTypeService.getProductionOrderType(productionOrder.productionOrderTypeId.Value);
            if (productionOrderType != null)
                productionOrder.typeDescription = productionOrderType.typeDescription;

            return productionOrder;
        }

        public async Task<List<ProductionOrder>> getProductionOrders(int startat, int quantity)
        {
            var productionOrders = await _context.ProductionOrders
                                            .OrderBy(x => x.productionOrderId)
                                            .Skip(startat).Take(quantity)
                                            .ToListAsync();
            List<ProductionOrder> result = new List<ProductionOrder>();
            foreach (var item in productionOrders)
            {
                result.Add(await getProductionOrder(item.productionOrderId));
            }

            return result;
        }


    }
}