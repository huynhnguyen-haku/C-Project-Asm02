using BusinessObject.Models;
using Repository;
using Repository.Implementation;

namespace Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo orderRepo = null;
        public OrderService()
        {
            orderRepo = new OrderRepo();
        }   

        public int AdddOrder(Order order) => orderRepo.AdddOrder(order);

        public int AddOrder(int? userId, DateTime? shippedDate, string total, string orderStatus, out string message)
        => orderRepo.AddOrder(userId, shippedDate, total, orderStatus, out message);

        public void DeleteOrder(int id) =>  orderRepo.DeleteOrder(id);

        public List<Order> GetAllOrders() =>  orderRepo.GetAllOrders();

        public List<Order> GetDataInRange(DateTime startTime, DateTime endTime, out string message)
        => orderRepo.GetDataInRange(startTime, endTime, out message);

        public Order GetOrderById(int id) =>  orderRepo.GetOrderById(id);

        public List<Order> GetOrderByUser(int userId) =>  orderRepo.GetOrderByUser(userId); 

        public int UpdateOrder(Order oldOrder, int? userId, DateTime? shippedDate, string total, string orderStatus, out string message)
        =>  orderRepo.UpdateOrder(oldOrder, userId, shippedDate, total, orderStatus, out message);

        public void UpdateOrder(Order order) =>  orderRepo.UpdateOrder(order);
    }
}
