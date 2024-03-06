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

        public CarDAO() { }
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

        public int GetCarCount()
        {
            int count = 0;
            try
            {
                var dbContext = new CarManagementContext();
                count = dbContext.Cars.Count();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return count;
        }

        public List<Car> GetCarList()
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

        public Car GetCarById(int id)
        {
            Car car = null;
            try
            {
                var dbContext = new CarManagementContext();
                car = dbContext.Cars.SingleOrDefault(m => m.CarId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return car;
        }

        public Car GetCarByName(string name)
        {
            Car car = null;
            try
            {
                var dbContext = new CarManagementContext();
                car = dbContext.Cars.SingleOrDefault(m => m.CarName.Trim().Equals(name));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return car;
        }

        public Car CreateCar(Car car)
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

        public Car RemoveCar(Car car)
        {
            try
            {
                if (car != null)
                {
                    using (var dbContext = new CarManagementContext())
                    {
                        var findCar = dbContext.Cars.Find(car.CarId);
                        if (findCar != null) {
                            dbContext.Cars.Remove(car);
                            dbContext.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return car;
        }

        public Car UpdateCar(Car car)
        {
            var dbContext = new CarManagementContext();
            var existingCar = GetCarById(car.CarId);
            try
            {
                if (existingCar != null)
                {
                    existingCar.CarName = car.CarName;
                    existingCar.CategoryId = car.CategoryId;
                    existingCar.Description = car.Description;
                    existingCar.UnitPrice = car.UnitPrice;
                    existingCar.UnitsInStock = car.UnitsInStock;
                    existingCar.CarStatus = car.CarStatus;

                    dbContext.Entry(existingCar).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return existingCar;
        }

        public IEnumerable<Car> searchCar(string name)
        {
            List<Car> list = null;
            try
            {
                var dbContext = new CarManagementContext();
                list = dbContext.Cars.Where(m => m.CarName.Contains(name)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
    }
}
