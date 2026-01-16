using LibraryManagementEFCORE.Models.DTOs;
using LibraryManagementEFCORE.Models.Entities;
using LibraryManagementEFCORE.Repositories.Interfaces;
using LibraryManagementEFCORE.Services.Interfaces;
using Mapster;

namespace LibraryManagementEFCORE.Services.Implementation
{
    public class RecordServices : IRecordServices
    {
        private readonly IRecordRepository _repo;
        private readonly IBookRepository _bookRepo;
        private readonly IMemberRepository _memberRepo;

        public RecordServices(IRecordRepository repo, IBookRepository bookRepo, IMemberRepository memberRepo)
        {
            _repo = repo;
            _bookRepo = bookRepo;
            _memberRepo = memberRepo;
        }

        public async Task AddRecordAsync(RecordCreateDto record)
        {
            var records = record.Adapt<Record>();
            var book = await _bookRepo.GetByBookIdAsync(records.BookId);
            var member = await _memberRepo.GetByMemberIdAsync(records.MemberId);

            if (book == null)
                throw new Exception("Book does not Exists");

            if (member == null)
                throw new Exception("Member does not Exists");

            if (!book.IsAvailable)
                throw new Exception("Book is already borrowed");

            book.IsAvailable = false;

            await _repo.AddRecordAsync(records);
        }

        public async Task DeleteRecordAsync(int id)
        {
            await _repo.DeleteRecordAsync(id);
        }

        public async Task<IEnumerable<RecordProfileDto>> GetAllRecordAsync()
        {
            var records = await _repo.GetAllRecordsAsync();

            var recordsDto = records.Adapt<IEnumerable<RecordProfileDto>>();

            return recordsDto;
        }

        public async Task<RecordProfileDto> GetByRecordIdAsync(int id)
        {
            var records = await _repo.GetByRecordIdAsync(id);

            var recordsDto = records.Adapt<RecordProfileDto>();

            if (recordsDto == null) return null;

            return recordsDto;
        }

        public async Task<bool> UpdateRecordAsync(RecordUpdateDto record, int id)
        {
            var records = await _repo.GetByRecordIdAsync(id);
            var book = await _bookRepo.GetByBookIdAsync(id);
            var member = await _memberRepo.GetByMemberIdAsync(id);

            if(records == null)
                throw new Exception("Record does not exists");
            if (book == null)
                throw new Exception("Book does not exists");
            if (member == null) 
                throw new Exception("Member does not exists");

            records.BookId = record.BookId;
            record.MemberId = record.MemberId;
            record.BorrowedTime = record.BorrowedTime;
            record.ReturnedTime = record.ReturnedTime;

            await _repo.UpdateRecordAsync(records);
            return true;
        }
    }
}
