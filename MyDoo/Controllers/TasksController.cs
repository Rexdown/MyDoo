using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyDoo.Bll.Interfaces;
using MyDoo.Entities;
using MyDoo.Views;

namespace MyDoo.Controllers;

[Route("api/")]
[ApiController]
public class TasksController : Controller
{
    private readonly ITaskLogic _taskLogic;
    private readonly IMapper _mapper;

    public TasksController(ITaskLogic taskLogic, IMapper mapper)
    {
        _taskLogic = taskLogic;
        _mapper = mapper;
    }
    
    [HttpGet]
    [Route("tasks/get/")]
    public async Task<IActionResult> GetTasks(int userId, DateTime date)
    {
        try
        {
            // var userTask = _mapper.Map<UserTask>(getTasksView);
            var result = await _taskLogic.GetUserTaskListAsync(userId, date);
        
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"{ex.GetType()}: {ex.Message}");
        }
    }
    
    // [HttpGet]
    // [Route("tasks/get/types/")]
    // public IActionResult GetTasksTypes(GetTasksView getTasksView)
    // {
    //     try
    //     {
    //         var userTask = _mapper.Map<UserTask>(getTasksView);
    //         var result = _taskLogic.GetUserTaskList(
    //             userTask.UserId,
    //             userTask.Date,
    //             userTask.Type,
    //             userTask.Important, 
    //             userTask.Complete);
    //     
    //         return Ok(result);
    //     }
    //     catch (Exception ex)
    //     {
    //         return BadRequest($"{ex.GetType()}: {ex.Message}");
    //     }
    // }
    
    [HttpPost]
    [Route("tasks/add/")]
    public async Task<IActionResult> AddTaskAsync(UserTask task)
    {
        try
        {
            await _taskLogic.AddUserTaskAsync(task);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest($"{ex.GetType()}: {ex.Message}");
        }
    }
    
    [HttpDelete]
    [Route("tasks/remove/")]
    public async Task<IActionResult> RemoveTaskAsync(int id)
    {
        try
        {
            await _taskLogic.RemoveUserTaskAsync(id);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest($"{ex.GetType()}: {ex.Message}");
        }
    }
    
    [HttpPut]
    [Route("tasks/update/")]
    public async Task<IActionResult> UpdateTaskAsync(UserTask task)
    {
        try
        {
            await _taskLogic.UpdateUserTaskAsync(task);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest($"{ex.GetType()}: {ex.Message}");
        }
    }
}