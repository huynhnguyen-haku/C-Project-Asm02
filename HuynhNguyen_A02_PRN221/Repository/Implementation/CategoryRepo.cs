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
    public class CategoryRepo : ICategoryRepo
    {
        private readonly CategoryDAO CategoryDAO;
        public CategoryRepo()
        {
            CategoryDAO = new CategoryDAO();
        }

        public Category CreateCategory(Category category) => CategoryDAO.CreateCategory(category);

        public void DeleteCategory(int id) => CategoryDAO.DeleteCategory(id);

        public Category GetCategoryById(int id) => CategoryDAO.GetCategoryById(id);
        public List<Category> GetCategoryList() => CategoryDAO.GetCategoryList();

        public IEnumerable<int> GetCategoryType() => CategoryDAO.GetCategoryType();

        public Category UpdateCategory(Category category) => CategoryDAO.UpdateCategory(category);
    }
}
