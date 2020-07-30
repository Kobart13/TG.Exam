using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace TG.Exam.SQL
{
    public class DAL
    {
        private const string qtGetAllOrders = @"
                                                SELECT OrderId
                                                      ,OrderCustomerId
                                                      ,OrderDate
                                                FROM dbo.Orders
                                                ";

        private const string qtGetAllOrdersWithCustomers = @"
                                                SELECT o.OrderId
                                                       ,o.OrderCustomerId
                                                       ,o.OrderDate
                                                       ,c.CustomerFirstName
                                                       ,c.CustomerLastName
                                                FROM dbo.Orders AS o 
                                                JOIN dbo.Customers AS c
                                                    ON c.CustomerId = o.OrderCustomerId
                                                ";

        private const string qtGetAllOrdersWithPriceUnder = @"
                                               SELECT o.OrderId
                                                      ,o.OrderCustomerId
                                                      ,o.OrderDate
                                                      ,SUM(i.ItemPrice) AS OrderPrice
                                               FROM dbo.Orders o 
                                               JOIN dbo.OrdersItems oi
                                                    ON o.OrderId = oi.OrderId 
                                               JOIN dbo.Items i
                                                    ON i.ItemId = oi.ItemId 
                                               GROUP BY o.OrderId, o.OrderCustomerId, o.OrderDate
                                               HAVING SUM(i.ItemPrice) < {0}
                                               ";

        private const string qtDeleteCustomer = @""; // Have questions about it below

        private const string qtGetAllItemsAndTheirOrdersCountIncludingTheItemsWithoutOrders = @"
                                              SELECT i.ItemId
                                                    ,i.ItemName
                                                    ,i.ItemPrice
                                                    ,COUNT(o.OrderId) AS OrdersCount
                                              FROM dbo.Items i
                                              LEFT JOIN dbo.OrdersItems oi ON oi.ItemId = i.ItemId
                                              LEFT JOIN dbo.Orders o ON oi.OrderId = o.OrderId
                                              GROUP BY i.ItemId, i.ItemName, i.ItemPrice
                                              ";

        private SqlConnection GetConnection()
        {
            var connectionString = ConfigurationManager.AppSettings["ConnectionString"];

            var con = new SqlConnection(connectionString);

            con.Open();

            return con;
        }

        private DataSet GetData(string sql)
        {
            var ds = new DataSet();

            using (var con = GetConnection())
            {
                using (var cmd = new SqlCommand(sql, con))
                {
                    using (var adp = new SqlDataAdapter(cmd))
                    {
                        adp.Fill(ds);
                    }
                }
            }

            return ds;
        }

        private void Execute(string sql)
        {
            using (var con = GetConnection())
            {
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable GetAllOrders()
        {
            var sql = qtGetAllOrders;

            var ds = GetData(sql);

            var result = ds.Tables.OfType<DataTable>().FirstOrDefault();

            return result;
        }
        public DataTable GetAllOrdersWithCustomers()
        {
            var sql = qtGetAllOrdersWithCustomers;

            var ds = GetData(sql);

            var result = ds.Tables.OfType<DataTable>().FirstOrDefault();

            return result;
        }

        public DataTable GetAllOrdersWithPriceUnder(int price)
        {
            var sql = qtGetAllOrdersWithPriceUnder;

            var ds = GetData(sql);

            var result = ds.Tables.OfType<DataTable>().FirstOrDefault();

            return result;
        }

        //HACK Why do we have orderId as a parameter of this method called DeleteCustomer?
        // What should we do in this case?
        // Need more clarification from business perspective on this before implementation.
        // In my opinion if we delete customer we should delete all related Orders and OrdersItem.
        // Otherwise we will have dirty records in our database
        public void DeleteCustomer(int orderId)
        {
            var sql = qtDeleteCustomer;

            Execute(sql);
        }

        internal DataTable GetAllItemsAndTheirOrdersCountIncludingTheItemsWithoutOrders()
        {
            var sql = qtGetAllItemsAndTheirOrdersCountIncludingTheItemsWithoutOrders;

            var ds = GetData(sql);

            var result = ds.Tables.OfType<DataTable>().FirstOrDefault();

            return result;
        }
    }
}
