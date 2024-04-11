using Forum.User.Core.Services;
using Forum.User.IntergrationEvents;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Forum.User.Api.Controllers;

public class AuthController:BaseController
{
    private readonly IMediator _mediator;
    private readonly IUserService _userService;

    public AuthController(IMediator mediator,IUserService userService)
    {
        _mediator = mediator;
        _userService = userService;
    }
    [HttpPost("signin")]
    public async Task<IActionResult> SignIn()
    {
        
        return Ok();
    }
    [HttpPost("signup")]
    public async Task<IActionResult> SignUp()
    {
        await _userService.SingUpUser("seadahmeti2@gmail.com", "Seadi2001!@@122");
        return Ok();
    }
}