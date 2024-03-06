using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface ICarRepo
    {
        public int GetCarCount();
        public List<Car> GetCarList();
        public Car GetCarById(int id);
        public Car GetCarByName(string name);
        public Car CreateCar(Car car);
        public Car RemoveCar(Car car);  
        public Car UpdateCar(Car car);
        public IEnumerable<Car> searchCar(string name);
    }
}
