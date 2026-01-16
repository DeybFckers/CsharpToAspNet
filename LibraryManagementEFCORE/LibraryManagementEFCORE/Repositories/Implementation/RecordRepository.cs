using LibraryManagementEFCORE.Data;
using LibraryManagementEFCORE.Models.Entities;
using LibraryManagementEFCORE.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementEFCORE.Repositories.Implementation
{
    public class RecordRepository : IRecordRepository
    {
        private readonly ApplicationDbContext _context;

        public RecordRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddRecordAsync(Record record)
        {
            await _context.AddAsync(record);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRecordAsync(int id)
        {
            var record = await _context.Records.FindAsync(id);
            _context.Records.Remove(record);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Record>> GetAllRecordsAsync()
        {
            return await _context.Records.ToListAsync();
        }

        public async Task<Record> GetByRecordIdAsync(int id)
        {
            return await _context.Records.FindAsync(id);
        }

        public async Task UpdateRecordAsync(Record record)
        {
            await _context.SaveChangesAsync();
        }
    }
}
