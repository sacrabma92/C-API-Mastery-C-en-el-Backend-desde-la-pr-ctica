using API.FumitureStore.Data;
using API.FumitureStore.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.FumitureStore.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly APIFurnitureStoreContext _context;

        public ClientsController(APIFurnitureStoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Client>> GetClient()
        {
            return await _context.Clients.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetClientDetails(int id)
        {
            var client = await _context.Clients.FirstOrDefaultAsync( c => c.Id == id);

            if(client == null) 
            {
                return NotFound();
            }

            return Ok(client);
        }

        [HttpPost]
        public async Task<IActionResult> PostCliente(Client client)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
            return CreatedAtAction("PostCliente", client.Id, client);
        }

        [HttpPut]
        public async Task<IActionResult> PutClient(Client client)
        {
            _context.Clients.Update(client);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteClient(Client client)
        {
            if(client == null) return NotFound();

            _context.Clients.Remove(client);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
