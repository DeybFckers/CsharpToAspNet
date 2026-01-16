using LibraryManagementEFCORE.Models.DTOs;
using LibraryManagementEFCORE.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementEFCORE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordController : ControllerBase
    {
        private readonly IRecordServices _recordServices;

        public RecordController(IRecordServices recordServices)
        {
            _recordServices = recordServices;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecordProfileDto>>> GetAllRecord()
        {
            var record = await _recordServices.GetAllRecordAsync();

            return Ok(record);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RecordProfileDto>> GetByIdRecord(int id)
        {
            var record = await _recordServices.GetByRecordIdAsync(id);
            if(record == null) return NotFound();

            return Ok(new
            {
                message = "Record Found Sucessfully",
                record = record 
            });
        } 

        [HttpPost]
        public async Task<ActionResult> CreateRecord(RecordCreateDto recordDto)
        {

            try
            {
                await _recordServices.AddRecordAsync(recordDto);

                return Ok(new
                {
                    message = "Recorded Successfully",
                    recordDto = recordDto
                });
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRecord(RecordUpdateDto recordDto, int id)
        {
            var record = await _recordServices.UpdateRecordAsync(recordDto, id);
            if (record == null)
                return NotFound(new { message = $"Record with id {id} not found" });

            return Ok(new { message = "Member Updated Successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRecord(int id)
        {
            var record = await _recordServices.GetByRecordIdAsync(id);
            if (record == null) return NotFound(new { message = $"Record with id {id} not found" });

            await _recordServices.DeleteRecordAsync(id);

            return Ok(new { message = "Record Deleted Successfully" });
        }
        
    }
}
