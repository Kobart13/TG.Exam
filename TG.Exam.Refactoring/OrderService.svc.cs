using System;
using System.Collections.Concurrent;
using System.Data.SqlClient;
using System.Diagnostics;
using log4net;
using log4net.Config;

namespace TG.Exam.Refactoring
{
    public class OrderService : IOrderService
    {
        /// <summary>
        /// An old implementation's potential problems:
        /// - It's complicated to read such a big methods like LoadOrder.
        /// LoadOrder needs to be splited to some specific methods cause it is not following Single Responsibility
        /// - It is better to implement Repository pattern
        /// - ConnectionString may not exist. Need to check it
        /// - There are many issues with Cache and Lock statement. One of them from business perpective,
        /// for example what if value has been changed in DB?
        /// Better to use ConcurrentDictinary instead of Dictionary ot even better to implement CacheHelper
        /// - Paramter OrderId is a String (but it is int in DB). It may lead to some sql injections
        /// - IDisposables aren't closed after using. Better to use them in "using" statement
        /// - It is better to use constants or properties instead of dirty strings and query templates
        /// - Better to use dependency injection for logger
        /// - It is better to provide Error details in response for users
        /// - So, below I've implemented not ideal but better approach of the code which covers all mentioned issues and a little bit more
        /// </summary>
        private const string ElapsedTimeMessage = "Elapsed - {0}";
        private const string ErrorMessage = "Error";

        private static readonly ILog logger = LogManager.GetLogger(typeof(OrderService));
        private static readonly ConcurrentDictionary<int, Order> cache = new ConcurrentDictionary<int, Order>();

        public OrderService()
        {
            BasicConfigurator.Configure();
        }

        public Order LoadOrder(string orderId)
        {
            int orderIdValue;
            if (int.TryParse(orderId, out orderIdValue))
            {
                return LoadOrder(orderIdValue);
            }

            return null;
        }

        private Order LoadOrder(int orderId)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            try
            {
                var order = GetCachedOrder(orderId);

                if (order != null)
                    return order;

                var orderRepository = new OrderRepository();
                order = orderRepository.GetOrder(orderId);

                SetCachedOrder(order);

                return order;

            }
            catch (SqlException ex)
            {
                logger.Error(ex.Message, ex);
                throw new ApplicationException(ErrorMessage, ex);
            }
            finally
            {
                stopWatch.Stop();
                logger.InfoFormat(ElapsedTimeMessage, stopWatch.Elapsed);
            }
        }

        private Order GetCachedOrder(int orderId)
        {
            if (cache.ContainsKey(orderId) && cache.TryGetValue(orderId, out var cachedOrder))
            {
                return cachedOrder;
            }

            return null;

        }

        private void SetCachedOrder(Order order)
        {
            var orderId = order.OrderId;
            if (order != null)
            {
                cache.TryAdd(orderId, order);
            }
        }

    }
}
