using shoes_final_exam.Models;
using shoes_final_exam.Models.ViewModels;

namespace shoes_final_exam.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAll();
        Task<List<CategoryVM>> GetAllReturnMV();
        Task<Category> GetById(int id);
        Task<bool> Add(Category category);
        Task<bool> Delete(int id);
        void Update(int id, Category category);
    }
}
