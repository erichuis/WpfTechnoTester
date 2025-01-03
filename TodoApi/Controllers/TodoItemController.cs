﻿using AutoMapper;
using Domain.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        private readonly ITodoItemService _todoItemService;
        private readonly IMapper _mapper;
        
        public TodoItemController(ITodoItemService todoItemService, IMapper mapper)
        {
            _todoItemService = todoItemService;
            _mapper = mapper;
        }

        [HttpGet()]
        public async Task<ActionResult<string>> GetHelloWorldAsync(string name) => await Task.Run(() => "Hello " + name);

        [Authorize(Policy = "ApiScope")]
        [HttpGet()]
        public async IAsyncEnumerable<TodoItemDto> GetAllTodoItemsAsync()
        {
            var results = _todoItemService.GetAllAsync();
            await foreach (var item in results)
            {
                yield return item;
            }
        }

        [Authorize(Policy = "ApiScope")]
        [HttpGet()]
        public async Task<ActionResult<TodoItemDto>> GetTodoItemAsync(Guid id)
        {
            var result = await _todoItemService.GetAsync(id); 
            return Ok(result);
        }

        [Authorize(Policy = "ApiScope")]
        [HttpPost()]
        public async Task<ActionResult<TodoItemDto>> CreateTodoItemAsync(TodoItemDto todoItem)
        {
            var newTodoItem = await _todoItemService.CreateAsync(todoItem);
            var result = CreatedAtAction(nameof(GetTodoItemAsync), new { id = newTodoItem.Id }, newTodoItem);
            var newResult = result.Value as TodoItemDto;

            if (newResult == null)
            {
                return BadRequest();
            }

            return Ok(newResult);
        }

        [Authorize(Policy = "ApiScope")]
        [HttpPut()]
        public async Task<ActionResult<bool>> UpdateTodoItemAsync([FromBody] TodoItemDto todoItemUpdated)
        {
            if (todoItemUpdated == null)
            {
                throw new NullReferenceException(nameof(TodoItemDto));
            }

            try
            {
                var result = await _todoItemService.UpdateAsync(todoItemUpdated);
                return Ok(result);
            }
            catch (Exception)
            {
                return new ActionResult<bool>(false);
            }
        }

        [Authorize(Policy = "ApiScope")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<bool>> DeleteTodoItemAsync(Guid id)
        {
            try
            {
                return await _todoItemService.DeleteAsync(id);

            }
            catch (Exception)
            {
                return false;
                //log something
            }
        }
    }
}
