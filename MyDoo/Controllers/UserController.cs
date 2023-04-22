using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    
    [HttpGet]
    [Route("user/get/tg")]
    public async Task<IActionResult> GetUserTg(string tgname)
    {
        try
        {
            var result = await _userLogic.GetUserTgAsync(tgname);
        
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"{ex.GetType()}: {ex.Message}");
        }
    }
    
    [HttpPost]
    [Route("user/register/")]
    public async Task<IActionResult> AddUserAsync(UserRegisterView userView)
    {
        try
        {
            var userInfo = _mapper.Map<User>(userView);
            
            var result = await _userLogic.GetUserTgAsync(userView.TGName);

            if (result == null)
            {
                var user = new User();
                user.Email = userView.Email;
                user.Name = userView.Name;
                user.TGName = userView.TGName;

                var rnd = new Random();
                byte[] bytes = new byte[25];
                rnd.NextBytes(bytes);
                byte[] randomPassword = bytes.Select(b => (byte)(b % 26 + 97)).ToArray();
                string resultPassword = "";
                foreach (var c in randomPassword)
                {
                    bool rndUpper = rnd.Next(0, 2) == 1;
                    resultPassword += rndUpper ? (char) (c - 32) : (char) c;
                }
            
                user.Password = resultPassword;
            
                await _userLogic.AddUserAsync(user);
        
                return Ok();
            }
            return BadRequest("Пользователь с таким Telegramm уже зарегистрирован");
        }
        catch (Exception ex)
        {
            return BadRequest($"{ex.GetType()}: {ex.Message}");
        }
    }
    
    [HttpPost]
    [Route("user/login/")]
    public async Task<IActionResult> CheckUserAsync(UserLoginView userView)
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