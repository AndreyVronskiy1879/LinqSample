using System;
using System.Collections.Generic;
using System.Linq;


namespace AnalyticsAdapter
{
    public class Repository : IRepository
    {
        private readonly Database _db;

        public Repository(Database db)
        {
            _db = db;                                                                                                                                                                                                                        ;
        }
        public Order[] GetOrders(int customerId)
        {
           return _db.Orders
                .Where(order => order.CustomerId == customerId)
                .ToArray();
        }

        public Order GetOrder(int orderId)
        {
            var order= _db.Orders.SingleOrDefault(order => order.Id == orderId);
            if(order == null)
            {
                throw new InvalidOperationException();
            }
            return order;
        }

         public decimal GetMoneySpentBy(int customerId) 
        {
            return _db.Orders.Join(_db.Products,
                (o) => o.ProductId,
                (p)=>p.Id,

                (o, p) => new
                {
                    p.Price,
                    o.CustomerId,
                })
                .Where(x=>x.CustomerId==customerId)
                .Sum(x=>x.Price);
        }
        public CustomerOverview GetCustomerOverview(int customerId)
        {
            var name = _db.Customers.Single(x => x.Id == customerId).Name;
            return new CustomerOverview
            {
                Name = name,
                TotalMoneySpent = GetMoneySpentBy(customerId),
                FavoriteProductName = GetFavoriteProductName(customerId)
            };
        }

        private string GetFavoriteProductName(int customerId)
        {
            var productId = GetOrders(customerId).Join(_db.Products,
                (o) => o.ProductId,
                (p) => p.Id,
                (o, p) => new
                {
                    ProductId = p.Id,
                    ProductName = p.Name,

                })
                .GroupBy(x => x.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    Count = g.Count(),
                    ProductName=g.First().ProductName
                })
                .OrderBy(x => x.Count)
                .Last()
                .ProductId;
            return _db.Products.Single(x => x.Id == productId).Name;
        }

        public bool AreAllPurchasesHigherThan(int customerId, decimal targetPrice)
        {
            return _db.Orders.Where(order => order.CustomerId == customerId)
                 .Join(_db.Products,
                     order => order.ProductId,
                     product => product.Id,
                     (order, product) => product)
                 .All(product => product.Price > targetPrice);
        }

        public Product[] GetAllProductsPurchased(int customerId)
        {
            return _db.Orders.Where(order => order.CustomerId == customerId)
                .Join(_db.Products,
                    order => order.ProductId,
                    product => product.Id,
                    (order, product) => product).ToArray();
        }
        public Product[] GetUniqueProductsPurchased(int customerId)
        {
            return _db.Orders.Where(order => order.CustomerId == customerId)
                .Select(order => order.ProductId)
                .Distinct()
                .Join(_db.Products,
                    id => id,
                    product => product.Id,
                    (id, product) => product).ToArray();
        }
        public int GetTotalProductsPurchased(int productId)
        {
            return _db.Orders.Count(order => order.ProductId == productId);
        }

        public bool HasEverPurchasedProduct(int customerId, int productId)
        {
            return _db. Orders.Any(order => order.CustomerId == customerId && order.ProductId == productId);
            
        }

        public bool DidPurchaseAllProducts(int customerId, params int[] productIds)
        {
            throw new NotImplementedException();
        }

        public List<(string productName, int numberOfPurchases)> GetProductsPurchased(int customerId)
        {
            throw new NotImplementedException();
        }
    }
    public interface IRepository
    {
        Order[] GetOrders(int customerId);//

        Order GetOrder(int orderId);//

        decimal GetMoneySpentBy(int customerId);//

        Product[] GetAllProductsPurchased(int customerId);

        Product[] GetUniqueProductsPurchased(int customerId);

        int GetTotalProductsPurchased(int productId);

        bool HasEverPurchasedProduct(int customerId, int productId);

        bool AreAllPurchasesHigherThan(int customerId, decimal targetPrice);

        bool DidPurchaseAllProducts(int customerId, params int[] productIds);

        CustomerOverview GetCustomerOverview(int customerId);//

        List<(string productName, int numberOfPurchases)> GetProductsPurchased(int customerId);
    }
}
