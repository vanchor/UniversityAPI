using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using UniversityApi.Models;

namespace UniversityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly UniversityContext _context;

        public GroupsController(UniversityContext context)
        {
            _context = context;
        }

        [HttpGet]
        // GET: api/groups
        public async Task<ActionResult<IEnumerable<Models.Group>>> GetGroups()
        {
            if(_context.Groups == null)    
                return NotFound();

            return await _context.Groups.ToListAsync();
        }

        [HttpPost]
        // POST: api/groups
        public async Task<ActionResult> PostGroup(Models.Group group)
        {
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
