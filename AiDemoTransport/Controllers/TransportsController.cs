using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AiDemoTransport.Data;
using AiDemoTransport.Models;
using AiDemoTransport.Services;

namespace AiDemoTransport.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransportsController : ControllerBase
{
    private readonly TransportDbContext _context;
    private readonly ITransportEventService _transportEventService;
    private readonly ILogger<TransportsController> _logger;

    public TransportsController(
        TransportDbContext context, 
        ITransportEventService transportEventService,
        ILogger<TransportsController> logger)
    {
        _context = context;
        _transportEventService = transportEventService;
        _logger = logger;
    }

    // GET: api/Transports
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Transport>>> GetTransports()
    {
        return await _context.Transports.ToListAsync();
    }

    // GET: api/Transports/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Transport>> GetTransport(int id)
    {
        var transport = await _context.Transports.FirstOrDefaultAsync(t => t.Id == id);

        if (transport == null)
        {
            return NotFound();
        }

        return transport;
    }

    // POST: api/Transports
    [HttpPost]
    public async Task<ActionResult<Transport>> PostTransport(Transport transport)
    {
        try
        {
            _context.Transports.Add(transport);
            await _context.SaveChangesAsync();

            // Publish transport created event to RabbitMQ
            await _transportEventService.PublishTransportCreatedAsync(transport);

            _logger.LogInformation("Transport created successfully with ID: {TransportId}", transport.Id);

            return CreatedAtAction("GetTransport", new { id = transport.Id }, transport);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating transport");
            throw;
        }
    }

    // PUT: api/Transports/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTransport(int id, Transport transport)
    {
        if (id != transport.Id)
        {
            return BadRequest();
        }

        _context.Entry(transport).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TransportExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/Transports/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTransport(int id)
    {
        var transport = await _context.Transports.FindAsync(id);
        if (transport == null)
        {
            return NotFound();
        }

        _context.Transports.Remove(transport);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TransportExists(int id)
    {
        return _context.Transports.Any(e => e.Id == id);
    }
}