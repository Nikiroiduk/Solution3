using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Solution3BL.Models;

namespace Solution3BL
{
    internal static class SqlServer
    {
        internal static int AddUser(User user, string connectionString)
        {
            try
            {
                using SqlConnection connection = new(connectionString);
                var sql = @"
                    DECLARE @UserId int;

                    SELECT @UserId = UserID FROM Пользователи 
                    WHERE FirstName = @FirstName 
                      AND LastName = @LastName 
                      AND Email = @Email;

                    IF @UserId IS NULL
                    BEGIN
                        INSERT INTO [Пользователи] (FirstName, LastName, Email) 
                        VALUES (@FirstName, @LastName, @Email);

                        SET @UserId = SCOPE_IDENTITY();
                    END

                    SELECT @UserId;";
                using SqlCommand command = new(sql, connection);
                command.Parameters.AddWithValue("@FirstName", user.FirstName);
                command.Parameters.AddWithValue("@LastName", user.LastName);
                command.Parameters.AddWithValue("@Email", user.Email);
                connection.Open();
                var result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int userId))
                {
                    return userId;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        internal static int AddProduct(Product product, string connectionString)
        {
            try
            {
                using SqlConnection connection = new(connectionString);
                var sql = @"
                    DECLARE @ProductId int;

                    SELECT @ProductId = ProductID FROM Товары 
                    WHERE ProductName = @ProductName 
                    AND Price = @Price;

                    IF @ProductId IS NULL
                    BEGIN
                        INSERT INTO [Товары] (ProductName, Price) 
                        VALUES (@ProductName, @Price);

                        SET @ProductId = SCOPE_IDENTITY();
                    END

                    SELECT @ProductId;";
                
                using SqlCommand command = new(sql, connection);
                command.Parameters.AddWithValue("@ProductName", product.Name);
                command.Parameters.AddWithValue("@Price", product.Price);
                connection.Open();
                var result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int productId))
                {
                    return productId;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        internal static int AddOrder(int UserId, Order order, string connectionString)
        {
            try
            {
                using SqlConnection connection = new(connectionString);
                var sql = @"
                    DECLARE @OrderId int;

                    SELECT @OrderId = PurchaseID FROM Покупки 
                    WHERE PurchaseID = @PurchaseID
                    AND UserID = @UserID 
                    AND PurchaseDate = @PurchaseDate;

                    IF @OrderId IS NULL
                    BEGIN
                        INSERT INTO [Покупки] (PurchaseID, UserID, PurchaseDate) 
                        VALUES (@PurchaseID, @UserID, @PurchaseDate);

                        SET @OrderId = SCOPE_IDENTITY();
                    END

                    SELECT @OrderId;";
                using SqlCommand command = new(sql, connection);
                command.Parameters.AddWithValue("@PurchaseID", order.No);
                command.Parameters.AddWithValue("@UserID", UserId);
                command.Parameters.AddWithValue("@PurchaseDate", order.RegistrationDate);
                connection.Open();
                var result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int orderId))
                {
                    return orderId;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        internal static int AddOrdersProducts(int orderId, Dictionary<int, int> productsIdsQuantity, string connectionString)
        {
            try
            {
                using SqlConnection connection = new(connectionString);
                connection.Open();

                using SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    var sql = new StringBuilder();

                    sql.AppendLine("INSERT INTO Покупки_Товары (PurchaseID, ProductID, Quantity)");
                    sql.AppendLine("VALUES");

                    int index = 0;
                    foreach (var kvp in productsIdsQuantity)
                    {
                        sql.Append($"(@OrderId, @ProductId{index}, @Quantity{index})");
                        if (index < productsIdsQuantity.Count - 1)
                        {
                            sql.Append(",");
                        }
                        sql.AppendLine();
                        index++;
                    }

                    using SqlCommand command = new(sql.ToString(), connection, transaction);
                    command.Parameters.AddWithValue("@OrderId", orderId);

                    index = 0;
                    foreach (var kvp in productsIdsQuantity)
                    {
                        command.Parameters.AddWithValue($"@ProductId{index}", kvp.Key);
                        command.Parameters.AddWithValue($"@Quantity{index}", kvp.Value);
                        index++;
                    }

                    int rowsAffected = command.ExecuteNonQuery();

                    transaction.Commit();

                    return rowsAffected;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("An error occurred while inserting records: " + ex.Message);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
