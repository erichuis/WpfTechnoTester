using AutoMapper;
using Domain.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class JournalEntryController : ControllerBase
    {
        private readonly IJournalEntryService _journalEntryService;
        private readonly IMapper _mapper;
        
        public JournalEntryController(IJournalEntryService journalEntryService, IMapper mapper)
        {
            _journalEntryService = journalEntryService;
            _mapper = mapper;
        }

        [HttpGet()]
        public async Task<ActionResult<string>> GetHelloWorldAsync(string name) => await Task.Run(() => "Hello " + name);

        [Authorize(Policy = "ApiScope")]
        [HttpGet()]
        public async IAsyncEnumerable<JournalEntryDto> GetAllJournalEntriesAsync()
        {
            var results = _journalEntryService.GetAllAsync();
            await foreach (var item in results)
            {
                yield return item;
            }
        }

        [Authorize(Policy = "ApiScope")]
        [HttpGet("category")]
        public async IAsyncEnumerable<JournalEntryDto> GetAllJournalEntriesByCategoryAsync(string category)
        {
            var results = _journalEntryService.GetAllByCategoryAsync(category);
            await foreach (var item in results)
            {
                yield return item;
            }
        }

        [Authorize(Policy = "ApiScope")]
        [HttpGet("date")]
        public async IAsyncEnumerable<JournalEntryDto> GetAllJournalEntriesByDateEntryAsync(DateTime date)
        {
            var results = _journalEntryService.GetAllByDateAsync(date);
            await foreach (var item in results)
            {
                yield return item;
            }
        }

        [Authorize(Policy = "ApiScope")]
        [HttpGet()]
        public async IAsyncEnumerable<JournalEntryDto> FindAllJournalEntriesByAsync(string search)
        {
            var results = _journalEntryService.FindAllAsync(search);
            await foreach (var item in results)
            {
                yield return item;
            }
        }

        [Authorize(Policy = "ApiScope")]
        [HttpGet()]
        public async Task<ActionResult<JournalEntryDto>> GetJournalEntryAsync(Guid id)
        {
            var result = await _journalEntryService.GetAsync(id); 
            return Ok(result);
        }

        [Authorize(Policy = "ApiScope")]
        [HttpPost()]
        public async Task<ActionResult<JournalEntryDto>> CreateJournalEntryAsync(JournalEntryDto journalEntry)
        {
            var newJournalEntry = await _journalEntryService.CreateAsync(journalEntry);
            var result = CreatedAtAction(nameof(GetJournalEntryAsync), new { id = newJournalEntry.Id }, newJournalEntry);
            var newResult = result.Value as JournalEntryDto;

            if (newResult == null)
            {
                return BadRequest();
            }

            return Ok(newResult);
        }

        [Authorize(Policy = "ApiScope")]
        [HttpPut()]
        public async Task<ActionResult<bool>> UpdateJournalEntryAsync([FromBody] JournalEntryDto JournalEntryUpdated)
        {
            if (JournalEntryUpdated == null)
            {
                throw new NullReferenceException(nameof(JournalEntryDto));
            }

            try
            {
                var result = await _journalEntryService.UpdateAsync(JournalEntryUpdated);
                return Ok(result);
            }
            catch (Exception)
            {
                return new ActionResult<bool>(false);
            }
        }

        [Authorize(Policy = "ApiScope")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<bool>> DeleteJournalEntryAsync(Guid id)
        {
            try
            {
                return await _journalEntryService.DeleteAsync(id);

            }
            catch (Exception)
            {
                return false;
                //log something
            }
        }
    }
}
