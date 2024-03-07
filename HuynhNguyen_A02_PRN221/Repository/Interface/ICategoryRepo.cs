using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface ICategoryRepo
    {
        public List<Category> GetAllCategory();
        public List<int> GetCatagories();
        public Category GetCategoryById(int id);
        public Category CreateCategory(Category category);
        public Category UpdateCategory(Category category);
        public void DeleteCategory(int categoryId);
    }
}
