using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

            var subject = new Subject()
            {
                Name = subjectVM.Name,
                Description = subjectVM.Description
            };
            
            if(subjectVM.GroupsId != null)
                subject.Groups.AddRange(_context.Groups.Where( g => subjectVM.GroupsId.Contains(g.Id) ));

            _context.Subjects.Add(subject);
            _context.SaveChanges();

            return CreatedAtAction("GetSubjects", new { id = subject.Id }, subject);
        }

        

    }
}
