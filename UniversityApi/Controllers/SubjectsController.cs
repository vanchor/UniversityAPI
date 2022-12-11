using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using UniversityApi.Models;
using UniversityApi.ViewModels;

namespace UniversityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly UniversityContext _context;

        public SubjectsController(UniversityContext context)
        {
            _context = context;
        }

        // GET: api/Subjects
        [HttpGet]
        public ActionResult<IEnumerable<Subject>> GetSubjects()
        {
            if (_context.Subjects == null)
                return NotFound();

            return _context.Subjects;
        }

        // GET: api/Subjects/1
        [HttpGet("{id}")]
        public ActionResult<Subject> GetSubject(int id)
        {
            if (_context.Subjects == null)
                return NotFound();

            var subject = _context.Subjects.Find(id);
            if (subject == null)
                return NotFound();

            return subject;
        }

        // POST: api/Subjects
        [HttpPost]
        public ActionResult<Subject> PostSubject(SubjectViewModel subjectVM)
        {
            if (_context.Subjects == null)
                return NotFound();

            var subject = SubjectVmToSubject(subjectVM);

            _context.Subjects.Add(subject);
            _context.SaveChanges();

            return CreatedAtAction("GetSubjects", new { id = subject.Id }, subject);
        }

        // PUT: api/Subjects/1
        [HttpPut("{id}")]
        public IActionResult PutSubject(int id, SubjectViewModel subjectVM)
        {
            if (_context.Subjects == null)
                return NotFound();

            var subject = SubjectVmToSubject(subjectVM);
            subject.Id = id;

            _context.Entry(subject).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(_context.Subjects?.Any(e => e.Id == id)).GetValueOrDefault())
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/Subjects/5
        [HttpDelete("{id}")]
        public IActionResult DeleteSubjects(int id)
        {
            if (_context.Subjects == null)
                return NotFound();

            var subjects = _context.Subjects.Find(id);
            if (subjects == null)
                return NotFound();

            _context.Subjects.Remove(subjects);
            _context.SaveChanges();

            return NoContent();
        }

        private Subject SubjectVmToSubject(SubjectViewModel subjectVM)
        {
            var subject = new Subject()
            {
                Name = subjectVM.Name,
                Description = subjectVM.Description,
                DepartmentID = subjectVM.DepartmentID,
                numberOfHours = subjectVM.numberOfHours,
                ECTS = subjectVM.ECTS
            };

            if (subjectVM.GroupsId != null)
                subject.Groups.AddRange(_context.Groups.Where(g => subjectVM.GroupsId.Contains(g.Id)));

            return subject;
        }

    }
}
