using EShope.API.DataObjects;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EShope.API.Migrations
{
    public class EShopeSeedData
    {
        public static void AddToContext(Models.EShopeMobileServiceContext context)
        {
            var userWithOrders = new User { Id = Guid.NewGuid(), Name = "userOrders" };
            var userWithEmptyOrders = new User { Id = Guid.NewGuid(), Name = "emptyUser" };
            var users = new List<User> { userWithOrders, userWithEmptyOrders };

            context.Set<User>().AddRange(users);

            //--------------
            var tvProduct = new Product
            {
                Id = Guid.NewGuid().ToString(),
                Name = "TV",
                Description = "TV Desc: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                Price = 150,
                StockQuantity = 7,
                ThumnailURL = ""
            };
            var laptopProduct = new Product
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Laptop",
                Description = "Laptop Desc: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                Price = 300,
                StockQuantity = 4,
                ThumnailURL = ""
            };
            var mobileProduct = new Product
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Mobile",
                Description = "Mobile Desc: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                Price = 200,
                StockQuantity = 2,
                ThumnailURL = "",
            };

            var keyboardProduct = new Product
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Keyboard",
                Description = "Keyboard Desc",
                Price = 70,
                StockQuantity = 1,
                ThumnailURL = "",
            };

            var products = new List<Product>
            {
                tvProduct, laptopProduct, mobileProduct, keyboardProduct,
                new Product{
                    Id = Guid.NewGuid().ToString(),
                    Name = "Headphone",
                    Description = "Headphone Desc: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum." ,
                    Price=70,
                    StockQuantity=0,
                    ThumnailURL=""
                }
            };

            context.Set<Product>().AddRange(products);

            //--------------
            var order1 = new Order
            {
                Id = Guid.NewGuid().ToString(),
                CheckoutDateTime = new DateTime(2018, 12, 1, 2, 1, 1),
                UserId = userWithOrders.Id,
                User = userWithOrders
            };

            var order2 = new Order
            {
                Id = Guid.NewGuid().ToString(),
                CheckoutDateTime = new DateTime(2018, 11, 25, 12, 45, 10),
                UserId = userWithOrders.Id,
                User = userWithOrders
            };
            var orders = new List<Order> { order1, order2 };
            context.Set<Order>().AddRange(orders);

            //--------------
            order1.OrderItems = new List<OrderItem> {
                new OrderItem{
                    Id = Guid.NewGuid().ToString(),
                    OrderId = order1.Id,
                    Product = tvProduct,
                    ProductId = tvProduct.Id,
                    Quantity = 5
                }, new OrderItem{
                    Id = Guid.NewGuid().ToString(),
                    OrderId = order1.Id,
                    Product = laptopProduct,
                    ProductId = laptopProduct.Id,
                    Quantity = 3
                }
            };

            order2.OrderItems = new List<OrderItem> {
                new OrderItem{
                    Id = Guid.NewGuid().ToString(),
                    OrderId = order2.Id,
                    Product = mobileProduct,
                    ProductId = mobileProduct.Id,
                    Quantity = 9
                }
            };

            var orderItems = order1.OrderItems.Union(order2.OrderItems).ToList();
            context.Set<OrderItem>().AddRange(orderItems);

            //return context as TDbContext;
            //base.Seed(context);
        }
    }
}