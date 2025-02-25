using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tickter.Core.Context;
using tickter.Core.DTO;
using tickter.Core.Entities;

namespace tickter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        // we need Database so we inject it using constructor
        // we need automapper so we need to inject it using constructor
        private readonly ApplicationDbContext _context;

        private readonly IMapper _mapper;

        public TicketsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // curd 

        // create
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateTicket([FromBody] CreateTicketDto createTicketDto)
        {
            var newTicket = new Ticket();
            _mapper.Map(createTicketDto, newTicket);
            await _context.Tickets.AddAsync(newTicket);
            await _context.SaveChangesAsync();

            return Ok("Ticket Saved Successfully");
        }

        // Read all new changes
        [HttpGet] 
        public async Task<ActionResult<IEnumerable<GetTicketDto>>> GetTickets(string? q)
        {
            // search
            IQueryable<Ticket> query = _context.Tickets;
            if(q is not null)
            {
                query = query.Where(t => t.PassengerName.Contains(q));
            }

            // get tickets from context

            var tickets = await query.ToListAsync();
            var convertedTickets = _mapper.Map<IEnumerable<GetTicketDto>>(tickets);

            return Ok(convertedTickets);

        }


        // Read one by id
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<GetTicketDto>> GetTicketById([FromRoute] long id)
        {
            // get ticket from context and check if it exists 
            var ticket = await _context.Tickets.SingleOrDefaultAsync(x => x.Id == id);
            if (ticket is null)
            {
                return NotFound("Ticket Not Found");
            }
            var convertedTicket = _mapper.Map<GetTicketDto>(ticket);
            return Ok(convertedTicket);
        }

        // Update
        [HttpPut]
        [Route("edit/{id}")]
        public async Task<IActionResult> EditTicket([FromRoute] long id, [FromBody] UpdateTicketDto updateTicketDto)
        {
            // Get ticket from context and check if it exits
            var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == id);
            if (ticket is null)
            {
                return NotFound("Ticket Not Found");
            }
            _mapper.Map(updateTicketDto, ticket);
            ticket.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            return Ok("Ticket Updated Successfully");
        }
        // Delete
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteTask([FromRoute]long id)
        {
            // Get Ticket from context and check if it exists 
            var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == id);
            if(ticket is null)
            {
                return NotFound("Ticket Not Found");
            }
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return Ok("Ticket Deleted Successfully");
        }
    }
}
