using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApi.Models;
using UniversityApi.ViewModels;

namespace UniversityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly UniversityContext _context;

        public StudentsController(UniversityContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetStudents()
        {
            if (_context.Students == null) return NotFound();

            return _context.Students.ToList();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public ActionResult<Student> GetStudent(int id)
        {
            if (_context.Students == null)
                return NotFound();

            var student = _context.Students.Find(id);

            if (student == null)
                return NotFound();

            return student;
        }

        // GET: api/Students/5/Group
        [HttpGet("{id}/Group")]
        public ActionResult<Group> GetStudentsGroup(int id)
        {
            if (_context.Students == null)
                return NotFound();

            var student = _context.Students.Find(id);
            if (student == null)
                return NotFound();

            var group = _context.Groups
                .Include(x => x.Students)
                .Include(x => x.Subjects)
                .FirstOrDefault(x => x.Id == student.GroupId);
            if (group == null)
                return NotFound();

            return group;
        }

        // GET: api/Students/5/Subjects
        [HttpGet("{id}/Subjects")]
        public ActionResult<IEnumerable<Subject>?> GetGroupSubjects(int id)
        {
            if (_context.Students == null)
                return NotFound();

            var student = _context.Students.Find(id);
            if (student == null)
                return NotFound();

            return new GroupsController(_context).GetGroupSubjects(student.GroupId);
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public IActionResult PutStudent(int id, StudentViewModel student)
        {
            if (_context.Groups.Find(student.GroupId) == null)
                return BadRequest("Incorrect GroupId");

            _context.Entry(new Student()
            {
                Id = id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Pasel = student.Pasel,
                PhoneNumber = student.PhoneNumber,
                Email = student.Email,
                age = student.age,
                GroupId = student.GroupId
            }).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/Students
        [HttpPost]
        public ActionResult<Student> PostStudent(StudentViewModel student)
        {
            if (_context.Students == null)
                return Problem("Entity set 'UniversityContext.Students'  is null.");

            if (_context.Groups.Find(student.GroupId) == null)
                return BadRequest("Incorrect GroupId");

            var st = new Student()
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Pasel = student.Pasel,
                PhoneNumber = student.PhoneNumber,
                Email = student.Email,
                age = student.age,
                GroupId = student.GroupId
            };

            _context.Students.Add(st);
            _context.SaveChanges();

            return CreatedAtAction("GetStudent", new { id = st.Id }, st);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            if (_context.Students == null)
                return NotFound();

            var student = _context.Students.Find(id);
            if (student == null)
                return NotFound();

            _context.Students.Remove(student);
            _context.SaveChanges();

            return NoContent();
        }

        private bool StudentExists(int id)
        {
            return (_context.Students?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
