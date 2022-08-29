using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApi.Models;

namespace UniversityApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupsController : ControllerBase
    {
        private readonly UniversityContext _context;

        public GroupsController(UniversityContext context)
        {
            _context = context;
        }

        // GET: api/Groups
        [HttpGet]
        public ActionResult<IEnumerable<Group>> GetGroups()
        {
            if (_context == null) return NotFound();

            return _context.Groups;
        }

        // GET: api/Groups/1
        [HttpGet("{id}")]
        public ActionResult<Group> GetGroup(int id)
        {
            if (_context.Groups == null)
                return NotFound();

            var group = _context.Groups.Find(id);
            if (group == null)
                return NotFound();

            return group;
        }

        // GET: api/Groups/1/Students
        [HttpGet("{id}/Students")]
        public ActionResult<IEnumerable<Student>> GetGroupStudents(int id)
        {
            if (_context.Groups == null)
                return NotFound();

            var group = _context.Groups.Find(id);
            if (group == null)
                return NotFound();

            return _context.Students.Where(st => st.GroupId == id).ToList();
        }

        // GET: api/Groups/2/Subjects
        [HttpGet("{id}/Subjects")]
        public ActionResult<IEnumerable<Subject>> GetGroupSubjects(int id)
        {
            if (_context.Groups == null)
                return NotFound();

            if (_context.Groups.Find(id) == null)
                return NotFound();

            return _context.Groups
                    .Where(g => g.Id == id)
                    .Include(st => st.Subjects)
                    .First().Subjects.ToList();
        }

        // PUT: api/Groups/id
        [HttpPut("{id}")]
        public ActionResult PutGroup(int id, GroupCreate group)
        {
            if (_context.Groups == null) return NotFound();

            _context.Entry(new Group(){
                    Id = id,
                    GradeName = group.GradeName,
                    Section = group.Section
                }).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(id))
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

        // POST: api/Groups
        [HttpPost]
        public ActionResult PostGroup(GroupCreate group)
        {
            if (_context.Groups == null)
                return Problem("Entity set 'UniversityContext.Groups'  is null.");

            var g = new Group()
            {
                GradeName = group.GradeName,
                Section = group.Section
            };

            _context.Groups.Add(g);
            _context.SaveChanges();

            return CreatedAtAction("GetGroups", new { Id = 1 }, g);
        }

        // DELETE : api/Groups/1
        [HttpDelete("{id}")]
        public IActionResult DeleteGroup(int id)
        {
            if (_context == null)
                return Problem("Entity set 'UniversityContext.Groups'  is null.");

            var group = _context.Groups.Find(id);
            if (group == null)
                return NotFound();

            if (_context.Students.Where(st => st.GroupId == id) != null)
                return Problem("The group cannot be deleted because there are students in it.");

            _context.Groups.Remove(group);
            _context.SaveChanges();

            return NoContent();
        }

        private bool GroupExists(int id)
        {
            return (_context.Groups?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
