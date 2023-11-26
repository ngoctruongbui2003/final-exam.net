using shoes_final_exam.Models;

namespace shoes_final_exam.Repositories
{
    public interface ISizeRepository
    {
        Task<List<Size>> GetAll();
        Task<Size> GetById(int id);
        Task<bool> Add(Size size);
        Task<bool> Delete(int id);
        void Update(int id, Size size);
    }
}
