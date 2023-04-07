using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyDoo.Bll.Interfaces;
using MyDoo.Entities;
using MyDoo.Views;

namespace MyDoo.Controllers;

[Route("api/")]
[ApiController]
public class UserController : Controller
{
    private readonly IUserLogic _userLogic;
    private readonly IMapper _mapper;

    public UserController(IUserLogic userLogic, IMapper mapper)
    {
        _userLogic = userLogic;
        _mapper = mapper;
    }
    
    [HttpGet]
    [Route("user/get/")]
    public async Task<IActionResult> GetUser(int id)
    {
        try
        {
            var result = await _userLogic.GetUserAsync(id);
        
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"{ex.GetType()}: {ex.Message}");
        }
    }
    
    [HttpPost]
    [Route("user/register/")]
    public async Task<IActionResult> AddUserAsync(User user)
    {
        try
        {
            await _userLogic.AddUserAsync(user);
        
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest($"{ex.GetType()}: {ex.Message}");
        }
    }
    
    [HttpPost]
    [Route("user/login/")]
    public async Task<IActionResult> CheckUserAsync(UserView userView)
    {
        try
        {
            var userInfo = _mapper.Map<User>(userView);
            var result = await _userLogic.CheckUserAsync(userInfo.Email, userInfo.Password);

            if (result)
                return Ok();
            
            return BadRequest();
        }
        catch (Exception ex)
        {
            return BadRequest($"{ex.GetType()}: {ex.Message}");
        }
    }
}