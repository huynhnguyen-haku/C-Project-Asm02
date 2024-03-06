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
        private CarDAO carDAO;
        
        public CarRepo()
        {
            carDAO = new CarDAO();
        }

        public Car CreateCar(Car car) => carDAO.CreateCar(car);

        public Car GetCarById(int id) => carDAO.GetCarById(id);

        public Car GetCarByName(string name)    => carDAO.GetCarByName(name);

        public int GetCarCount() => carDAO.GetCarCount();

        public List<Car> GetCarList() => carDAO.GetCarList();

        public Car RemoveCar(Car car) => carDAO.RemoveCar(car);

        public IEnumerable<Car> searchCar(string name) => carDAO.searchCar(name);

        public Car UpdateCar(Car car) => carDAO.UpdateCar(car);
    }
}
