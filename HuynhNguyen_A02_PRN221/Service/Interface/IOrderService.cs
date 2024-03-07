using BusinessObject.Models;

namespace Service
{
    public interface IOrderService
    {
        public List<Order> GetOrderByUser(int userId);
        public List<Order> GetAllOrders();
        public Order GetOrderById(int id);
        public void DeleteOrder(int id);
        public int AdddOrder(Order order);

        public int AddOrder(int? userId, DateTime? shippedDate, string total, string orderStatus, out string message);
        public int UpdateOrder(Order oldOrder, int? userId, DateTime? shippedDate, string total, string orderStatus, out string message);
        public void UpdateOrder(Order order);
        public List<Order> GetDataInRange(DateTime startTime, DateTime endTime, out string message);
    }
}
