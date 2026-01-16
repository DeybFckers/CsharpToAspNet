using LibraryManagementEFCORE.Models.DTOs;
using LibraryManagementEFCORE.Models.Entities;
using LibraryManagementEFCORE.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementEFCORE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberServices _memberServices;

        public MemberController(IMemberServices memberServices)
        {
            _memberServices = memberServices;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberProfileDto>>> GetAllMember()
        {
            var member = await _memberServices.GetAllMemberAsync();
            return Ok(member);
        }

        [HttpGet("{id}")]
        public async Task <ActionResult<MemberProfileDto>> GetByIdMember(int id)
        {
            var member = await _memberServices.GetByMemberIdAsync(id);
            if (member == null) return NotFound();

            return Ok(new
            {
                message = "Member Found Successfully",
                member = member
            });
        }

        [HttpPost]
        public async Task<ActionResult> CreateMember(MemberCreateDto memberDto) 
        {
            await _memberServices.AddMemberAsync(memberDto);

            return Ok(new
            {
                message = "Member Registered Successfully",
                member = memberDto
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMember(MemberUpdateDto memberDto, int id)
        {
            var member = await _memberServices.UpdateMemberAsync(memberDto, id);

            if (!member)
                return NotFound(new { message = $"Member {id} not found" });

            return Ok(new {message = "Member Updated Sucessfully"});
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMember(int id)
        {
            var member = await _memberServices.GetByMemberIdAsync(id);
            if (member == null) return NotFound();

            await _memberServices.DeleteMemberAsync(id);

            return Ok(new { message = "Member Deleted Sucessfully" });
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> MemberLogin(MemberLoginDto login)
        {
            var token = _memberServices.Login(login);

            if (token == null)
                return Unauthorized("Invalid credentials");

            return Ok(new { accessToken = token });
        }
    }
}
