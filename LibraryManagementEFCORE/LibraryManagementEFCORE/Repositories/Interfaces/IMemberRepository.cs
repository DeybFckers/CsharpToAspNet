using LibraryManagementEFCORE.Models.Entities;

namespace LibraryManagementEFCORE.Repositories.Interfaces
{
    public interface IMemberRepository
    {
        Task<IEnumerable<Member>> GetAllMemberAsync();
        Task<Member> GetByMemberIdAsync(int id);
        Task AddMemberAsync(Member member);
        Task UpdateMemberAsync(Member member);
        Task DeleteMemberAsync(int id);
        Task<Member>GetByEmail(string email);

    }
}
