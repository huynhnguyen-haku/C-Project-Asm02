using BusinessObject.Models;
using DataAccessObject;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementation
{
    public class CarRepo : ICarRepo
    {
        public int GetNumberOfCars() => CarDAO.Instance.GetNumberOfCars();
        public List<Car> GetCarsList() => CarDAO.Instance.GetCarsList();
        public List<int> GetCarType() => CarDAO.Instance.GetCarType();
        public Car CreateCars(Car car) => CarDAO.Instance.CreateCars(car);
        public Car GetCarByID(int id) => CarDAO.Instance.GetCarByID(id);
        public Car UpdateCar(Car updateCar) => CarDAO.Instance.UpdateCar(updateCar);
        public bool DeleteCar(int carId) => CarDAO.Instance.DeleteCar(carId);
    }
}
