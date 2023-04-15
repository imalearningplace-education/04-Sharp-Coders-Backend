using Domain;
using Domain.Dto;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Domain.Dto;
using AutoMapper;
using System.Net;
using Microsoft.Win32;

namespace UserCrudApi.Controllers;

[ApiController]
[Route("/users")]
public class UserController : ControllerBase {

    private UserCrudContext _context;
    private IMapper _mapper;

    public UserController(UserCrudContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAll() {
        var users = _context
            .Users
            .Where(user => user.IsActive)
            .Select(user => _mapper.Map<UserViewDto>(user))
            .ToList();

        if (users.Count == 0)
            return NotFound(new {
                Moment = DateTime.Now,
                Message = "The user list is empty."
            });

        return Ok(users);
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult GetById(int id) {
        var user = _context.Users.FirstOrDefault(u => u.Id == id);

        if (user == null) return NotFound(new {
            Moment = DateTime.Now,
            Message = $"Cannot find user with id = {id}."
        });

        var userProfile = _mapper.Map<UserViewDto>(user);
        return Ok(userProfile);
    }

    [HttpPost]
    [Route("register")]
    public IActionResult UserRegister(
        [FromBody] UserRegisterDto register
    ) {
        var isEmailAlreadyRegistered = _context
            .Users
            .Any(user => user.Email == register.Email);

        if (isEmailAlreadyRegistered)
            return StatusCode(
                statusCode: (int)HttpStatusCode.NotAcceptable,
                value: new {
                    Message = $"The email: {register.Email} is already registered",
                    Moment = DateTime.Now
                });

        var user = _mapper.Map<User>(register);
        user.Password = BCrypt.Net.BCrypt.HashPassword(register.Password);

        _context.Users.Add(user);
        _context.SaveChanges();
        var userProfile = _mapper.Map<UserViewDto>(user);

        return CreatedAtAction(nameof(GetById), new { Id = user.Id }, userProfile);
    }

    [HttpPost]
    [Route("login")]
    public IActionResult UserLogin(
        [FromBody] UserLoginDto login
    ) {
        var user = _context.Users.FirstOrDefault(user => user.Email == login.Email);

        var isNotEmailRegistered = (user == null);
        var errorResponse = StatusCode(
            statusCode: (int)HttpStatusCode.Unauthorized,
            value: new {
                Message = $"Email and Password didn't match",
                Moment = DateTime.Now
        });

        if (isNotEmailRegistered) return errorResponse;

        var isNotAuthorized = !BCrypt.Net.BCrypt.Verify(login.Password, user.Password);

        if (isNotAuthorized) return errorResponse;

        var userView = _mapper.Map<UserViewDto>(user);
        return CreatedAtAction(nameof(GetById), new { Id = user.Id }, userView);
    }
}
