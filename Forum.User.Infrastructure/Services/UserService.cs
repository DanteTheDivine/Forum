using System.Security.Claims;
using Forum.User.Core.Entities;
using Forum.User.Core.Repository;
using Forum.User.Core.Services;
using Forum.User.IntergrationEvents;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Forum.User.Infrastructure.Services;

public class UserService:IUserService
{
  
    private readonly IUserRepository _userRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimFactory;
    private readonly SignInManager<ApplicationUser> _signin;
    private readonly IMediator _mediator;


    public UserService(IUserRepository userRepository,
        UserManager<ApplicationUser> userManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimFactory,
        SignInManager<ApplicationUser> signin,
        IMediator mediator
       )
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _userClaimFactory = userClaimFactory;
        _signin = signin;
        _mediator = mediator;
    }
    public async Task<ForumUserServiceResult<ClaimsPrincipal>> SignInUser(string userEmail, string password)
    {
      
        var user = await _userRepository.GetByEmail(userEmail);
        if (user == null || user.IsBlocked)
        {
            return ForumUserServiceResult<ClaimsPrincipal>.ReturnError(new [] {"Invalid Credentials"});
        }
        var signInResults =await _signin.CheckPasswordSignInAsync(user,password,false);
        if (!signInResults.Succeeded)
        {
            return ForumUserServiceResult<ClaimsPrincipal>.ReturnError(new []{"Invalid Credentials"});
        }
        var claims=await _userClaimFactory.CreateAsync(user);
        return new ForumUserServiceResult<ClaimsPrincipal>()
            { SucessMessage = "Account Created With Success. Please Confirm Your Email",Data = claims};
    }

    public async Task BlockUser(string email)
    {
        var user=await _userRepository.GetByEmail(email);
        user.BlockUser();
        await _userRepository.UpdateAsync(user);
        
    }
    public async Task<ForumUserServiceResult<ClaimsPrincipal>> SingUpUser(string userName, string password)
    {
        ApplicationUser user = new ApplicationUser(userName);
        await  _mediator.Publish(new UserCreatedEvent(user.Id, user.Email, user.UserName));
        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            return ForumUserServiceResult<ClaimsPrincipal>.ReturnError(result.Errors.Select(x=>x.Description));
        }
        var claims=await _userClaimFactory.CreateAsync(user);
    
        return new ForumUserServiceResult<ClaimsPrincipal>()
            { SucessMessage = "Account Created With Success. Please Confirm Your Email",Data = claims};
    }

    public async Task ConfirmEmail(string token, string email)
    {
        var user = await _userRepository.GetByEmail(email);
        if (user is null)
        {
           return;
        }
        await  _userManager.ConfirmEmailAsync(user, token);
    }

    public async Task<ForumUserServiceResult<string>> GetUserConfirmationCode(string email)
    {
        var user = await _userRepository.GetByEmail(email);
        if (user is null)
        {
            return ForumUserServiceResult<string>.ReturnError(new []{"User Does Not Exist"});
        }
        var token=await  _userManager.GenerateEmailConfirmationTokenAsync(user);
        return new ForumUserServiceResult<string>()
            { SucessMessage = "Token Generated With Success",Data = token};
    }
}