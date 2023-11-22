using shoes_final_exam.Models;

namespace shoes_final_exam.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
        Category GetById(int id);
    }
}
