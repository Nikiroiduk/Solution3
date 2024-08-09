using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Solution3BL
{
    public static class XmlToDataBase
    {
        public static void SaveXmlObjectsInDb(string xmlPath, string dbConnectionString)
        {
            var orders = Serializer.SerializeXmlToOrders(xmlPath);

            foreach (var item in orders)
            {
                try
                {
                    var userId = SqlServer.AddUser(item.User, dbConnectionString);
                    var prodPurch = new Dictionary<int, int>();
                    foreach (var product in item.Products)
                    {
                        var productId = SqlServer.AddProduct(product, dbConnectionString);
                        prodPurch[productId] = product.Quantity;
                    }
                    var orderId = SqlServer.AddOrder(userId, item, dbConnectionString);
                    var changedLines = SqlServer.AddOrdersProducts(orderId, prodPurch, dbConnectionString);
#if DEBUG
                    Console.WriteLine($"{item}\n");
                    Console.WriteLine($"User [{item.User.FirstName} {item.User.LastName}] saved with id: {userId}\n" +
                        $"Products saved by ids:\n" +
                        $"{string.Join(", ", prodPurch.Keys)}\n" +
                        $"Order saved with id: {orderId}\n");
#endif
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error processing user {item.User.FirstName} {item.User.LastName}: {ex.Message}");
                }
            }
        }
    }
}
