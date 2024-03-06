using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class CategoryDAO
    {
        private static CategoryDAO instance = null;

        public CategoryDAO()
        {
        }

        public static CategoryDAO Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new CategoryDAO();
                }
                return Instance;
            }
        }

        public List<Category> GetCategoryList()
        {
            List<Category> list = null;
            try
            {
                var dbContext = new CarManagementContext();
                list = dbContext.Categories.ToList();
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public IEnumerable<int> GetCategoryType()
        {
            List<int> categoryTypes;
            try
            {
                var dbContext = new CarManagementContext();
                categoryTypes = dbContext.Categories
                    .Where(c => c.CategoryId != null)
                    .Select(c => c.CategoryId)
                    .Distinct()
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return categoryTypes;
        }

        public Category GetCategoryById(int id)
        {
            Category category = null;
            try
            {
                var dbContext = new CarManagementContext();
                category = dbContext.Categories
                    .Where(c => c.CategoryId == id)
                    .SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return category;
        }

        public Category CreateCategory(Category category)
        {
            try
            {
                if(category != null)
                {
                    var dbContext = new CarManagementContext();
                    dbContext.Categories.Add(category);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return category;
        }

        public Category UpdateCategory(Category category)
        {
            try
            {
                Category checkExisted = GetCategoryById(category.CategoryId);
                if (checkExisted != null) { }
                {
                    var dbContext = new CarManagementContext();
                    dbContext.Categories.Update(category);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return category;
        }

        public void DeleteCategory(int id)
        {
            try
            {
                Category checkExisted = GetCategoryById(id);
                if (checkExisted != null)
                {
                    var dbContext = new CarManagementContext();
                    dbContext.Categories.Remove(checkExisted);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
