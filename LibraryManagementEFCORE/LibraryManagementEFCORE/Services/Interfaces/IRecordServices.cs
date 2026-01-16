using LibraryManagementEFCORE.Models.DTOs;

namespace LibraryManagementEFCORE.Services.Interfaces
{
    public interface IRecordServices
    {
        Task<IEnumerable<RecordProfileDto>> GetAllRecordAsync();
        Task<RecordProfileDto> GetByRecordIdAsync(int id);
        Task AddRecordAsync(RecordCreateDto record);
        Task DeleteRecordAsync(int id);
        Task<bool> UpdateRecordAsync(RecordUpdateDto record, int id);

    }
}
