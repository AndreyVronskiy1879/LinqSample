using AnalyticsAdapter;
using System;
using Xunit;
using FluentAssertions;

namespace DataAcsses.Test
{
    public class RepositoryTests
    {
        private Database _db = new Database();
        [Fact]
        public void GetOrders_NonExistingCustomer_ReturnEmptyResult()
        {
            //arrange
            var repository = new Repository(_db);

            //act
            var orders = repository.GetOrders(1000);

            //assert
            Assert.Empty(orders);
        }

        [Fact]
        public void GetOrders_ExistingCustomer_ReturnResult()
        {
            //arrange
            var repository = new Repository(_db);

            //act
            var orders = repository.GetOrders(1);

            //assert
            orders.Length.Should().Be(3);
            orders.Should().BeEquivalentTo(new[]
            {
                new Order(1, 1, 1),
                new Order(2, 1, 1),
                new Order(3, 4, 1),
            });
        }

        [Fact]
        public void GetOrders_ForExistingCustomer_ReturnResult()
        {
            //arrange
            var repository = new Repository(_db);

            //act
            var orders = repository.GetOrders(1000);

            //assert
            Assert.Empty(orders);
        }

        [Fact]
        public void GetOrders_ForExistingCustomer_ReturnNull
            ()
        {
            //arrange
            var repository = new Repository(_db);

            //act
            var orders = repository.GetOrders(1000);

            //assert
            Assert.NotNull(orders);
        }

        [Fact]
        public void ProductsId_ForNonExistingProduct_ReturnResult()
        {
            //arrange
            var repository = new Repository(_db);

            //act
            var productsId = repository.GetOrders(3);

            //assert
            Assert.Empty(productsId);
        }

        [Fact]
        public void ProductsId_ForExistingProduct_ReturnResult()
        {
            //arrange
            var repository = new Repository(_db);

            //act
            var productsId = repository.GetOrders(3);

            //assert
            Assert.Empty(productsId);
        }

        [Fact]
        public void GetFavoriteProductName_ForFirstCostumer_ReturnProductId()
        {
            var repository = new Repository(_db);

            //act
            var productId = repository.GetOrders(3);

            AndConstraint<FluentAssertions.Collections.GenericCollectionAssertions<Order>> andConstraint = productId.Should().OnlyHaveUniqueItems();
        }

        [Fact]
        public void AreAllPurchasesHigherThan_ReturnResult()
        {
            var repository = new Repository(_db);

            //act
            var productId = repository.GetOrders(3);

            Assert.True(productId != null);
        }
    }
}
