using Conferences.Api.Entities;
using Conferences.Api.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Conferences.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConferencesController : ControllerBase
{
    private readonly ConferenceContext _context;
    private readonly ILogger<ConferencesController> _logger;

    public ConferencesController(ConferenceContext context, ILogger<ConferencesController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // GET: api/conferences
    [HttpGet]
    public async Task<IActionResult> GetAsync() 
        => Ok(await _context.Conferences.ToListAsync());

    // GET: api/conferences/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var conference = await _context.Conferences.FindAsync(id);

        if (conference == null)
            return NotFound();

        return Ok(conference);
    }

    // POST: api/conferences
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateAsync(Conference entity)
    {
        _context.Conferences.Add(entity);
        await _context.SaveChangesAsync();
        return CreatedAtAction(
            nameof(Get),
            new { id = entity.Id },
            null);
    }

    // PUT: api/conferences/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, Conference entity)
    {
        if (id != entity.Id)
            return BadRequest();

        _context.Entry(entity).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }

        return NoContent();
    }

    // DELETE: api/conferences/5
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var conference = await _context.Conferences.FindAsync(id);
        if (conference == null)
            return NotFound();

        _context.Conferences.Remove(conference);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
