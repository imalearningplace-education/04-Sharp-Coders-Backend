using HelloWorldApi.Domain;
using HelloWorldApi.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorldApi.Controllers;

[ApiController]
[Route("heroes")]
public class HeroController : ControllerBase {

    private HeroContext _context;

    public HeroController(HeroContext heroContext) {
        _context = heroContext;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<Hero>> GetAllHeroes(
        [FromQuery] bool includeRetired = true
    ) {
        var heroes = _context.Heroes.ToList();

        if (heroes.Count == 0) 
            return NotFound(new {
                message = "Cannot found any hero in heroes db.",
                moment = DateTime.UtcNow
            });

        return Ok(includeRetired ?
            heroes :
            heroes.Where(h => !h.IsRetired) );
    }

    [HttpGet]
    [Route("{id}")] 
    public IActionResult GetHeroById(int id) {
        var hero = _context.Heroes.FirstOrDefault(h => h.Id == id);
        if (hero == null) return NotFound();
        return Ok(hero);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult CreateNewHero([FromBody] Hero hero) {
        _context.Heroes.Add(hero);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetHeroById), new { id = hero.Id }, hero);
    }

}
