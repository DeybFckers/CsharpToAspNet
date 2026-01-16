using LibraryManagementEFCORE.Models.Entities;

namespace LibraryManagementEFCORE.Repositories.Interfaces
{
    public interface IRecordRepository
    {
        Task <IEnumerable<Record>> GetAllRecordsAsync();
        Task<Record> GetByRecordIdAsync(int id);
        Task AddRecordAsync(Record record);
        Task UpdateRecordAsync(Record record);
        Task DeleteRecordAsync(int id);
    }
}
