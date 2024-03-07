using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class CarDAO
    {
        private static CarDAO instance = null;
        private readonly CarManagementContext dbContext = null;

        private CarDAO()
        {
            dbContext = new CarManagementContext();
        }
        public static CarDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CarDAO();
                }
                return instance;
            }
        }

        public int GetNumberOfCars()
        {
            int NumberOfCars = 0;
            try
            {
                var dbContext = new CarManagementContext();
                NumberOfCars = dbContext.Cars.Count();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return NumberOfCars;
        }

        public List<Car> GetCarsList()
        {
            List<Car> list = null;
            try
            {
                var dbContext = new CarManagementContext();
                list = dbContext.Cars.Include(c => c.Category).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public List<int> GetCarType()
        {
            List<int> carTypes;
            try
            {
                var dbContext = new CarManagementContext();
                carTypes = dbContext.Cars
                .Where(car => car.CarId != null)
                .Select(car => car.CarId)
                .Distinct()
                .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return carTypes;
        }

        public Car CreateCars(Car car)
        {
            try
            {
                if (car != null)
                {
                    var dbContext = new CarManagementContext();
                    dbContext.Cars.Add(car);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return car;
        }

        public Car GetCarByID(int id)
        {
            Car car = null;
            try
            {
                var dbContext = new CarManagementContext();
                car = dbContext.Cars.SingleOrDefault(c => c.CarId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return car;
        }

        public Car UpdateCar(Car updateCar)
        {
            try
            {
                using (var dbContext = new CarManagementContext())
                {
                    var existingCar = dbContext.Cars.Find(updateCar.CarId);

                    if (existingCar != null)
                    {
                        existingCar.CategoryId = updateCar.CategoryId;
                        existingCar.CarName = updateCar.CarName;
                        existingCar.Description = updateCar.Description;
                        existingCar.UnitPrice = updateCar.UnitPrice;
                        existingCar.UnitsInStock = updateCar.UnitsInStock;
                        existingCar.CarStatus = updateCar.CarStatus;

                        dbContext.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return updateCar;
        }

        public bool DeleteCar(int carId)
        {
            try
            {
                var carToDelete = dbContext.Cars.Find(carId);

                if (carToDelete != null)
                {
                    dbContext.Cars.Remove(carToDelete);
                    dbContext.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
