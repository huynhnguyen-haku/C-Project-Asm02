using BusinessObject.Models;

namespace Service
{
    public interface IOrderDetailService
    {
        public List<OrderDetail> GetOrderDetailById(int orderId);
        public void AddOrderDetail(List<OrderDetail> orderDetails);
        public void UpdateOrderDetail(List<OrderDetail> orderDetails);
        public List<OrderDetail> GetOrderDetailByOrderId(int orderId);
    }
}
