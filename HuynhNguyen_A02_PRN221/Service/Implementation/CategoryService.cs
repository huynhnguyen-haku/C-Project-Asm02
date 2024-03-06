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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepo categoryRepo;

        public CategoryService()
        {
            categoryRepo = new CategoryRepo();
        }

        public Category CreateCategory(Category category) => categoryRepo.CreateCategory(category);

        public void DeleteCategory(int id) => categoryRepo.DeleteCategory(id);

        public Category GetCategoryById(int id) => categoryRepo.GetCategoryById(id);

        public List<Category> GetCategoryList() => categoryRepo.GetCategoryList();

        public IEnumerable<int> GetCategoryType() => categoryRepo.GetCategoryType();

        public Category UpdateCategory(Category category) => categoryRepo.UpdateCategory(category);
    }
}
