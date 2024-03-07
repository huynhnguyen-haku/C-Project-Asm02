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
        private ICarRepo carRepository;
        public CarService()
        {
            carRepository = new CarRepo();
        }

        public Car CreateCars(Car car)
        {
            return carRepository.CreateCars(car);
        }

        public bool DeleteCar(int carId)
        {
            return carRepository.DeleteCar(carId);
        }

        public Car GetCarByID(int id)
        {
            return carRepository.GetCarByID(id);
        }

        public List<Car> GetCarsList()
        {
            return carRepository.GetCarsList();
        }

        public List<int> GetCarType()
        {
            return carRepository.GetCarType();
        }

        public int GetNumberOfCars()
        {
            return carRepository.GetNumberOfCars();
        }

        public Car UpdateCar(Car updateCar)
        {
            return carRepository.UpdateCar(updateCar);
        }
    }
}
