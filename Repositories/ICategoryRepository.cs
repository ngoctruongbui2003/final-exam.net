using shoes_final_exam.Models;
using shoes_final_exam.Models.ViewModels;

namespace shoes_final_exam.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAll();
        Task<List<CategoryVM>> GetAllReturnMV();
        Task<CategoryVM> GetByIdReturnMV(int id);
        Task<bool> Add(CategoryVM categoryVM);
        Task<bool> Delete(int id);
        Task Update(CategoryVM categoryVM);
    }
}
