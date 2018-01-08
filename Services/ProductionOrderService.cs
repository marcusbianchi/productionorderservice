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
    public class ProductionOrderService : IProductionOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IRecipeService _recipeService;
        private readonly IProductionOrderTypeService _productionOrderTypeService;
        private readonly IThingService _thingService;
        public ProductionOrderService(ApplicationDbContext context, IRecipeService recipeService,
            IProductionOrderTypeService productionOrderTypeService, IThingService thingService)
        {
            _context = context;
            _recipeService = recipeService;
            _productionOrderTypeService = productionOrderTypeService;
            _thingService = thingService;
        }

        public async Task<ProductionOrder> addProductionOrder(ProductionOrder productionOrder)
        {
            var recipe = await _recipeService.getRecipe(productionOrder.recipe.recipeId);
            if (recipe == null)
                return null;
            productionOrder.recipe = recipe;
            productionOrder.currentStatus = null;
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
            if (productionOrder == null)
                return null;
            var productionOrderType = await _productionOrderTypeService.getProductionOrderType(productionOrder.productionOrderTypeId.Value);
            if (productionOrderType != null)
                productionOrder.typeDescription = productionOrderType.typeDescription;
            if (productionOrder.currentThingId != null)
            {
                var (thing, status) = await _thingService.getThing(productionOrder.currentThingId.Value);
                if (status == HttpStatusCode.OK)
                    productionOrder.currentThing = thing;
            }

            return productionOrder;
        }

        public async Task<(List<ProductionOrder>, int)> getProductionOrders(int startat, int quantity
        , ProductionOrderFields fieldFilter, string fieldValue,
            ProductionOrderFields orderField, OrderEnum order)
        {
            var productionOrdersQuery = _context.ProductionOrders.Where(x => x.currentStatus != stateEnum.inactive.ToString());
            productionOrdersQuery = ApplyFilter(productionOrdersQuery, fieldFilter, fieldValue);
            productionOrdersQuery = ApplyOrder(productionOrdersQuery, orderField, order);

            var productionOrders = await productionOrdersQuery.Skip(startat).Take(quantity)
                                                        .ToListAsync();
            var queryCount = _context.ProductionOrders.Where(x => x.currentStatus != stateEnum.inactive.ToString());
            queryCount = ApplyFilter(queryCount, fieldFilter, fieldValue);
            queryCount = ApplyOrder(queryCount, orderField, order);
            var totalCount = queryCount.Count();
            List<ProductionOrder> result = new List<ProductionOrder>();
            foreach (var item in productionOrders)
            {
                result.Add(await getProductionOrder(item.productionOrderId));
            }

            return (result, totalCount);
        }

        public async Task<ProductionOrder> setProductionOrderToThing(ProductionOrder productioOrder, int thingId)
        {
            var productioOrderDb = productioOrder;
            if (productioOrderDb == null)
            {
                return null;
            }
            productioOrderDb.currentThingId = thingId;

            _context.ProductionOrders.Update(productioOrderDb);
            await _context.SaveChangesAsync();
            return productioOrderDb;
        }

        private IQueryable<ProductionOrder> ApplyFilter(IQueryable<ProductionOrder> queryProducts,
        ProductionOrderFields fieldFilter, string fieldValue)
        {
            switch (fieldFilter)
            {
                case ProductionOrderFields.currentStatus:
                    queryProducts = queryProducts.Where(x => x.currentStatus.Contains(fieldValue));
                    break;
                case ProductionOrderFields.productionOrderNumber:
                    queryProducts = queryProducts.Where(x => x.productionOrderNumber.Contains(fieldValue));
                    break;
                default:
                    break;
            }
            return queryProducts;
        }

        private IQueryable<ProductionOrder> ApplyOrder(IQueryable<ProductionOrder> queryProducts,
        ProductionOrderFields orderField, OrderEnum order)
        {
            switch (orderField)
            {
                case ProductionOrderFields.currentStatus:
                    if (order == OrderEnum.Ascending)
                        queryProducts = queryProducts.OrderBy(x => x.currentStatus);
                    else
                        queryProducts = queryProducts.OrderByDescending(x => x.currentStatus);
                    break;
                case ProductionOrderFields.productionOrderNumber:
                    if (order == OrderEnum.Ascending)
                        queryProducts = queryProducts.OrderBy(x => x.productionOrderNumber);
                    else
                        queryProducts = queryProducts.OrderByDescending(x => x.productionOrderNumber);
                    break;
                default:
                    queryProducts = queryProducts.OrderBy(x => x.productionOrderNumber);
                    break;
            }
            return queryProducts;
        }



    }
}