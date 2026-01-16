using LibraryManagementEFCORE.Models.DTOs;

namespace LibraryManagementEFCORE.Services.Interfaces
{
    public interface IMemberServices
    {
        Task <IEnumerable<MemberProfileDto>> GetAllMemberAsync();
        Task <MemberProfileDto> GetByMemberIdAsync(int id);
        Task AddMemberAsync(MemberCreateDto member);
        Task <bool> UpdateMemberAsync(MemberUpdateDto member, int id);
        Task DeleteMemberAsync(int id);
        Task <string?> Login(MemberLoginDto login);
    }
}
