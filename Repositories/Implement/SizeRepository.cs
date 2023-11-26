using Microsoft.EntityFrameworkCore;
using shoes_final_exam.Data;
using shoes_final_exam.Models;

namespace shoes_final_exam.Repositories.Implement
{
    public class SizeRepository : ISizeRepository
    {
        private readonly MyDbContext _context;

        public SizeRepository(MyDbContext context) {
            _context = context;
        }

        public Task<bool> Add(Size size)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Size>> GetAll()
        {
            return await _context.Sizes.Include(s => s.Products).ToListAsync();
        }

        public Task<Size> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Size size)
        {
            throw new NotImplementedException();
        }
    }
}
