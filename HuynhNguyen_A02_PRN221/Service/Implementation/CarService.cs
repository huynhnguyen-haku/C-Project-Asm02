using BusinessObject.Models;
using Repository.Implementation;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class CarService : ICarService
    {
        private readonly ICarRepo carRepo;
        public CarService()
        {
            carRepo = new CarRepo();
        }

        public Car CreateCar(Car car) => carRepo.CreateCar(car);

        public Car GetCarById(int id) => carRepo.GetCarById(id);

        public Car GetCarByName(string name) => carRepo.GetCarByName(name);

        public int GetCarCount() => carRepo.GetCarCount();

        public List<Car> GetCarList() => carRepo.GetCarList();

        public Car RemoveCar(Car car) => carRepo.RemoveCar(car);

        public IEnumerable<Car> searchCar(string name) => carRepo.searchCar(name);

        public Car UpdateCar(Car car) => carRepo.UpdateCar(car);
    }
}
