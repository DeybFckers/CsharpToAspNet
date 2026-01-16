using LibraryManagementEFCORE.Models.DTOs;
using LibraryManagementEFCORE.Models.Entities;
using LibraryManagementEFCORE.Repositories.Interfaces;
using LibraryManagementEFCORE.Services.Interfaces;
using Mapster;

namespace LibraryManagementEFCORE.Services.Implementation
{
    public class MemberServices : IMemberServices
    {
        private readonly IMemberRepository _repo;
        private readonly ITokenServices _tokenServices;

        public MemberServices(IMemberRepository repo, ITokenServices tokenServices)
        {
            _repo = repo;
            _tokenServices = tokenServices;
        }

        public async Task AddMemberAsync(MemberCreateDto member)
        {
            var members = member.Adapt<Member>();
            members.Password = BCrypt.Net.BCrypt.HashPassword(members.Password);

            await _repo.AddMemberAsync(members);
        }

        public async Task DeleteMemberAsync(int id)
        {
            await _repo.DeleteMemberAsync(id);
        }

        public async Task <IEnumerable<MemberProfileDto>> GetAllMemberAsync()
        {
            var members = await _repo.GetAllMemberAsync();
            var membersDto = members.Adapt<IEnumerable<MemberProfileDto>>();

            return membersDto;

        }

        public async Task <MemberProfileDto> GetByMemberIdAsync(int id)
        {
            var member = await _repo.GetByMemberIdAsync(id);
            if (member == null) return null;

            var membersDto = member.Adapt<MemberProfileDto>();

            return membersDto;
        }

        public async Task<string?> Login(MemberLoginDto login)
        {
            var member = await _repo.GetByEmail(login.Email);

            if (member == null)
                return null;

            bool isValid = BCrypt.Net.BCrypt.Verify(login.Password, member.Password);
            if (!isValid)
                return null;

            return _tokenServices.GenerateToken(
                member.Id,
                member.Email
            );


        }

        public async Task <bool> UpdateMemberAsync(MemberUpdateDto member, int id)
        {
            var members = await _repo.GetByMemberIdAsync(id);

            if(members == null) return false;

            members.Name = member.Name;
            members.Email = member.Email;

            await _repo.UpdateMemberAsync(members);
            return true;

        }
    }
}
