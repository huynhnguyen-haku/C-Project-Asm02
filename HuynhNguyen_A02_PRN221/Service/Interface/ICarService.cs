using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ICarService
    {
        public int GetCarCount();
        public Car GetCarById(int id);
        public Car GetCarByName(string name);
        public Car CreateCar(Car car);
        public Car UpdateCar(Car car);
        public List<Car> GetCarList();
        public IEnumerable<Car> searchCar(string name);
        public Car RemoveCar(Car car);
    }
}
