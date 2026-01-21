using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Herb_Track_Bulgaria_Server.Data;
using Herb_Track_Bulgaria_Server.Models;

namespace Herb_Track_Bulgaria_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HerbsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HerbsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Herb>>> GetHerbs()
        {
            return await _context.Herbs.ToListAsync();
        }
    }
}