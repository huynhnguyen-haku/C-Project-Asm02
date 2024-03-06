using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ICategoryService
    {
        public List<Category> GetCategoryList();
        public IEnumerable<int> GetCategoryType();
        public  Category GetCategoryById(int id);
        public Category CreateCategory(Category category);
        public Category UpdateCategory(Category category);
        public void DeleteCategory(int id);
    }
}
