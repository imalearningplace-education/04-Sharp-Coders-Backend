using Domain;
using Domain.Dto;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Domain.Dto;
using AutoMapper;

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
            .Select(user =>
                _mapper.Map<UserProfileDto>(user)
            )
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

        var userProfile = _mapper.Map<UserProfileDto>(user);
        return Ok(userProfile);
    }

    [HttpPost]
    public IActionResult RegisterUser(
        [FromBody] UserRegisterDto userRegisterDto
    ) {
        var user = _mapper.Map<User>(userRegisterDto);
        _context.Users.Add(user);
        _context.SaveChanges();
        var userProfile = _mapper.Map<UserProfileDto>(user);

        return CreatedAtAction(nameof(GetById), new { Id = user.Id }, userProfile );    
    }
}
