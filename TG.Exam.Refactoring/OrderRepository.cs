using System;
using System.Data.SqlClient;

namespace TG.Exam.Refactoring
{
    public class OrderRepository : BaseRepository, IOrderRepository
    {
        private const string qtGetOrderById =
            "SELECT OrderId, OrderCustomerId, OrderDate FROM dbo.Orders WHERE OrderId={0}";

        public Order GetOrder(int orderId)
        {
            using (var connection = GetConnection())
            {
                var query = string.Format(qtGetOrderById, orderId);
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                            return null;

                        return new Order
                        {
                            OrderId = reader[nameof(Order.OrderId)] as int? ?? 0,
                            OrderCustomerId = reader[nameof(Order.OrderCustomerId)] as int? ?? 0,
                            OrderDate = reader[nameof(Order.OrderDate)] as DateTime? ?? DateTime.MinValue
                        };
                    }
                }
            }


        }
    }
}
