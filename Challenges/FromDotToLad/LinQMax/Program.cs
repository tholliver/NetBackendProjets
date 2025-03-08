using Dumpify;
using LinQMax.Config;
using LinQMax.Services;
using LinQMax.Utils;


var configuration = new ConfigReader();
var connectionString = configuration.GetConnectionString("DefaultConnection");
var dbService = new DatabaseService(connectionString);


var _dbContext = dbService.GetContext();

var query = _dbContext.Customers
    .OrderBy(c => c.FirstName)
    .Take(5)
    .ToArray();


// Gets the customers
var customerOrdersById = _dbContext.Orders
    .Where(o => o.Customer!.Id == 2)
    .Select(o => new { o.Customer!.FirstName, o.Customer.LastName, o.OrderDate, o.OrderStatus })
    .Take(5).ToList();

// Get the most frequent products in a [store] [stock] [producs]

var getTopProductsPerStores = _dbContext.Stores.Join(
    _dbContext.Stocks,
    store => store.Id,
    stock => stock.StoreId,
    (store, stock) => new { store.Id, store.StoreName, stock.ProductId }
)
.GroupBy(p => new { p.StoreName, p.ProductId })
.Select(group => new
{
    StoreName = group.Key.StoreName,
    ProductId = group.Key.ProductId,
    Count = group.Count()
})
.OrderByDescending(x => x.Count)
.ToList();


// Do it in raw sql
var getTotalProductsByStore = from store in _dbContext.Stores
                              join stock in _dbContext.Stocks
                                on store.Id equals stock.StoreId
                              group stock by new { store.Id, store.StoreName } into storeGroup
                              select new
                              {
                                  StoreId = storeGroup.Key.Id,
                                  StoreName = storeGroup.Key.StoreName,
                                  TotalProducts = storeGroup.Count()
                              };

query.Display();
getTotalProductsByStore.Display();


