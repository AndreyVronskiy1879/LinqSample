using System;
using Xunit;
using AnalitycsAdapter;
using FluentAssertions;


namespace DataAcsses.Tes
{
    public class RepositoryTest
    {
        //AAA - tripleA test
        [Fact]
        public void GetOrders_ForNonExistingCustomer_ReturnsResult()
        {
            //arrange
            var db = new Database();
            var repository = new Repository(db);

            //act   
            var orders=repository.GetOrders(1000);

            //assert
            Assert.Empty(orders);
        }

        public void GetOrders_ForExistingCustomer_ReturnsResult()
        {
            //arrange
            var db = new Database();
            var repository = new Repository(db);

            //act   
            var orders = repository.GetOrders(1);

            //assert
            Assert.True(orders.Length==3);
            orders.Length.Should().Be(3);
        }

    }
}
