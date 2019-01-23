using HV_tech.Domain;
using System.Collections.Generic;
using Xunit;

namespace HV_tech.UnitTests
{
    public class OrdersTest
    {
        public OrdersTest()
        {

        }

        [Fact]
        public void TestExamples()
        {
            var ordersList = CreateListOrders();

            foreach (var _orderMock in ordersList)
            {
                string order = string.Empty;

                var domain = new Order(_orderMock.Key);
                domain.CreateOrder();
                var result = domain.GetOrderText();

                Assert.Equal(_orderMock.Value, result);
            }
        }

        [Fact]
        public void CheckInvalidMultipleOrderedItems()
        {
            var order = "morning,1,1,1";

            var domain = new Order(order);
            domain.CreateOrder();
            var result = domain.GetOrderText();

            Assert.Equal("eggs, error", result);
        }

        [Fact]
        public void CheckValidMultipleOrderedItems()
        {
            var order = "morning,3,3,3,3";

            var domain = new Order(order);
            domain.CreateOrder();
            var result = domain.GetOrderText();

            Assert.Equal("coffee(x4)", result);
        }

        private Dictionary<string, string> CreateListOrders()
        {
            return new Dictionary<string, string>()
            {
                { "morning, 1, 2, 3", "eggs, toast, coffee" },
                { "morning, 2, 1, 3", "eggs, toast, coffee" },
                { "morning, 1, 2, 3, 4","eggs, toast, coffee, error" },
                { "morning, 1, 2, 3, 3, 3","eggs, toast, coffee(x3)" },
                { "night, 1, 2, 3, 4", "steak, potato, wine, cake" },
                { "night, 1, 2, 2, 4", "steak, potato(x2), cake" },
                { "night, 1, 2, 3, 5", "steak, potato, wine, error" },
                { "night, 1, 1, 2, 3, 5", "steak, error" }
            };
        }
    }
}
