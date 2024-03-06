using BusinessObject.Models;
using Repository;
using Repository.Implementation;

namespace Service.Implementation
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepo orderDetailRepo = null;

        public OrderDetailService()
        {
            orderDetailRepo = new OrderDetailRepo();
        }

        public void AddOrderDetail(List<OrderDetail> orderDetails) => orderDetailRepo.AddOrderDetail(orderDetails);

        public List<OrderDetail> GetOrderDetailById(int orderId) => orderDetailRepo.GetOrderDetailById(orderId);

        public List<OrderDetail> GetOrderDetailByOrderId(int orderId) => orderDetailRepo.GetOrderDetailByOrderId(orderId);

        public void UpdateOrderDetail(List<OrderDetail> orderDetails) => orderDetailRepo.UpdateOrderDetail(orderDetails);
    }
}
