using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LobUwp.Models;
using System.Data.SqlClient;

namespace LobUwp.Services
{
    public static class DataService
    {
        const string DbUser = "sa";
        const string DbPass = "pass";

        public static async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            using (SqlConnection conn = new SqlConnection(
                $"Database=WideWorldImporters;Server=.;User ID={DbUser};Password={DbPass}"))
            {
                try
                {
                    await conn.OpenAsync();
                    SqlCommand cmd = new SqlCommand("select o.OrderId, " +
                        "c.CustomerName, o.OrderDate, o.PickingCompletedWhen, " +
                        "sum(l.Quantity * l.UnitPrice) as OrderTotal " +
                        "from Sales.Orders o " +
                        "inner join Sales.Customers c on c.CustomerID = o.CustomerID " +
                        "inner join Sales.OrderLines l on o.OrderID = l.OrderID " +
                        "group by o.OrderId, c.CustomerName, o.OrderDate, o.PickingCompletedWhen " +
                        "order by o.OrderDate desc", conn);

                    var results = new List<Order>();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while(reader.Read())
                        {
                            var order = new Order
                            {
                                Company = reader.GetString(1),
                                OrderId = reader.GetInt32(0),
                                OrderDate = reader.GetDateTime(2),
                                OrderTotal = reader.GetDecimal(4),
                                DatePicked = !reader.IsDBNull(3) ? reader.GetDateTime(3) : (DateTime?)null
                            };
                            results.Add(order);
                        }
                        return results;
                    }
                }
                catch 
                {
                    return null;
                }
            }
        }

        public static async Task<IEnumerable<OrderItem>> GetOrderItemsAsync(int orderId)
        {
            using (SqlConnection conn = new SqlConnection(
                 $"Database=WideWorldImporters;Server=.;User ID={DbUser};Password={DbPass}"))
            {
                try
                {
                    await conn.OpenAsync();
                    SqlCommand cmd = new SqlCommand("select Description,Quantity,UnitPrice " +
                        $"from Sales.OrderLines where OrderID = {orderId}", conn);

                    var results = new List<OrderItem>();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            var orderItem = new OrderItem
                            {
                                Description = reader.GetString(0),
                                Quantity = reader.GetInt32(1),
                                UnitPrice = reader.GetDecimal(2),
                            };
                            results.Add(orderItem);
                        }
                        return results;
                    }
                }
                catch
                {
                    return null;
                }
            }
        }

        public static async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            using (SqlConnection conn = new SqlConnection(
                 $"Database=WideWorldImporters;Server=.;User ID={DbUser};Password={DbPass}"))
            {
                try
                {
                    await conn.OpenAsync();
                    SqlCommand cmd = new SqlCommand("select c.CustomerID, c.CustomerName, " +
                        "cat.CustomerCategoryName, c.DeliveryAddressLine2, c.DeliveryPostalCode, " +
                        "city.CityName, c.PhoneNumber " +
                        "from Sales.Customers c " +
                        "inner join Sales.CustomerCategories cat on c.CustomerCategoryID = cat.CustomerCategoryID " +
                        "inner join Application.Cities city on c.DeliveryCityID = city.CityID", conn);

                    var results = new List<Customer>();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            var customer = new Customer
                            {
                                CustomerId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Category = reader.GetString(2),
                                Address = reader.GetString(3),
                                PostalCode = reader.GetString(4),
                                City = reader.GetString(5),
                                Phone = reader.GetString(6)
                            };
                            results.Add(customer);
                        }
                        return results;
                    }
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
