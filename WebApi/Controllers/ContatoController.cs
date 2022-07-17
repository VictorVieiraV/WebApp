using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApp.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoController : ControllerBase
    {
        private readonly WebApiContext _context;

        public ContatoController(WebApiContext context)
        {
            _context = context;
        }
        
        [HttpPost]
        public async Task<ActionResult<Contato>> PostContato(Contato contato)
        {
          if (_context.Contato == null)
          {
              return Problem("Entity set 'WebApiContext.Contato'  is null.");
          }
            _context.Contato.Add(contato);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ContatoExists(contato.Cpf))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool ContatoExists(string id)
        {
            return (_context.Contato?.Any(e => e.Cpf == id)).GetValueOrDefault();
        }
    }
}
