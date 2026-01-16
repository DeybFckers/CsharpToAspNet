using LibraryManagementEFCORE.Data;
using LibraryManagementEFCORE.Models.Entities;
using LibraryManagementEFCORE.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementEFCORE.Repositories.Implementation
{
    public class MemberRepository : IMemberRepository
    {
        private readonly ApplicationDbContext _context;

        public MemberRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddMemberAsync(Member member)
        {
            await _context.AddAsync(member);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMemberAsync(int id)
        {
            var member = await _context.Members.FindAsync(id);
            _context.Members.Remove(member);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Member>> GetAllMemberAsync()
        {
            return await _context.Members.ToListAsync();
        }

        public async Task<Member> GetByEmail(string email)
        {
            return await _context.Members.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<Member> GetByMemberIdAsync(int id)
        {
            return await _context.Members.FindAsync(id);
        }

        public async Task UpdateMemberAsync(Member member)
        {
            await _context.SaveChangesAsync();
        }
    }
}
