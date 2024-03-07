using BusinessObject.Models;
using DataAccessObject;


namespace Repository.Implementation
{
    public class OrderDetailRepo : IOrderDetailRepo
    {
        public void AddOrderDetail(List<OrderDetail> orderDetails)
        {
            OrderDetailDAO.Instance.AddOrderDetail(orderDetails);
        }

        public List<OrderDetail> GetOrderDetailById(int orderId)
        {
            return OrderDetailDAO.Instance.GetOrderDetailByOrderId(orderId);
        }

        public List<OrderDetail> GetOrderDetailByOrderId(int orderId)
        {
            return OrderDetailDAO.Instance.GetOrderDetailByOrderId(orderId);
        }

        public void UpdateOrderDetail(List<OrderDetail> orderDetails)
        {
            OrderDetailDAO.Instance.UpdateOrderDetails(orderDetails);
        }
    }
}
