using BusinessObject.Models;

namespace DataAccessObject
{
    public class OrderDetailDAO
    {
        private static OrderDetailDAO instance = null;
        private static object instanceLock = new object();

        public static OrderDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDetailDAO();
                    }
                    return instance;
                }
            }
        }

        private CarManagementContext _context = new CarManagementContext();

        public List<OrderDetail> GetOrderDetailByOrderId(int orderId)
        {
            return _context.OrderDetails.Where(od => od.OrderId == orderId).ToList();
        }

        public void AddOrderDetail(List<OrderDetail > orderDetails)
        {
            foreach (var orderDetail in orderDetails)
            {
                var car = CarDAO.Instance.GetCarById(orderDetail.CarId);
                if (car.UnitsInStock < orderDetail.Quantity)
                {
                    throw new Exception("Not enough car in stock");
                }
                car.UnitsInStock -= orderDetail.Quantity;
                _context.Cars.Update(car);
                _context.OrderDetails.Add(orderDetail);
            }
            _context.SaveChanges();
        }

        public void UpdateOrderDetails(List<OrderDetail> orderDetails)
        {
            if (orderDetails.Count <= 0)
            {
                return;
            }

            var listOrder = _context.OrderDetails.Where(or => or.OrderId == orderDetails[0].OrderId).ToList();
            foreach (var orderDetail in listOrder)
            {
                var car = CarDAO.Instance.GetCarById(orderDetail.CarId);
                car.UnitsInStock += orderDetail.Quantity;
                _context.Cars.Update(car);
                _context.Remove(orderDetail);
            }

            _context.SaveChanges();
            foreach (var orderDetail in orderDetails)
            {
                var car = CarDAO.Instance.GetCarById(orderDetail.CarId);
                if (car.UnitsInStock < orderDetail.Quantity)
                {
                    throw new Exception("Not enough car");
                }
                car.UnitsInStock -= orderDetail.Quantity;

                _context.Cars.Update(car);
                _context.OrderDetails.Add(orderDetail);
            }

            _context.SaveChanges();
        }
    }
}
