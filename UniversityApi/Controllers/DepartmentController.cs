using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using UniversityApi.Data;
using UniversityApi.Models;
using UniversityApi.ViewModels;

namespace UniversityApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IMongoCollection<Department> _departmentCollection;

        public DepartmentController(IOptions<UniversityMongoDbSettings> universityMongoDbSettings)
        {
            var mongoClient = new MongoClient(
                 universityMongoDbSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                universityMongoDbSettings.Value.DatabaseName);

            _departmentCollection = mongoDatabase.GetCollection<Department>(
                universityMongoDbSettings.Value.DepartmentCollectionName);
        }

        // GET: api/Department
        [HttpGet]
        public ActionResult<IEnumerable<Department>> GetDepartments()
        {
            return _departmentCollection
                    .Find(_ => true)
                    .ToList();
        }

        // GET: api/Department/ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(string id)
        {
            if (!ObjectId.TryParse(id, out _))
                return NotFound("Incorrect id");

            var dep = await _departmentCollection
                    .Find(x => x.Id == id)
                    .FirstOrDefaultAsync();

            if (dep == null)
                return NotFound();

            return dep;
        }

        // GET: api/Department/ID/Educators
        [HttpGet("{id}/Educators")]
        public ActionResult<IEnumerable<Educator>?> GetEducators(string id)
        {
            if (!ObjectId.TryParse(id, out _))
                return NotFound("Incorrect id");
            var dep = _departmentCollection
                    .Find(x => x.Id == id)
                    .FirstOrDefault();
            if (dep == null)
                return NotFound("No department with such id.");

            return dep.Educators?.ToList();
        }

        // GET: api/Department/ID/Educators/ID
        [HttpGet("{id}/Educators/{EducatorId}")]
        public ActionResult<Educator> GetEducator(string id, int EducatorId)
        {
            if (!ObjectId.TryParse(id, out _))
                return NotFound("Incorrect id");

            var dep = _departmentCollection
                   .Find(x => x.Id == id)
                   .FirstOrDefault();

            if (dep == null)
                return NotFound();

            var educator = dep.Educators?.
                            FirstOrDefault(x => x.Id == EducatorId);

            if (educator == null)
                return NotFound();

            return educator;
        }

        // PUT: api/Department/ID
        [HttpPut("{id}")]
        public async Task<ActionResult> PutDepartment(string id, Department updateDepartment)
        {
            if (!ObjectId.TryParse(id, out _))
                return NotFound("Incorrect id");
            if (id != updateDepartment.Id)
                return BadRequest("The id in the request does not match the id in the body.");

            await _departmentCollection
                    .ReplaceOneAsync(
                        x => x.Id == id, 
                        updateDepartment);
            return NoContent();
        }

        // POST: api/Department
        [HttpPost]
        public async Task<ActionResult> PostDepartment(DepartmentVM createDepartment)
        {
            var dep = new Department()
            {
                Name = createDepartment.Name,
                UniversityId = createDepartment.UniversityId,
                address = createDepartment.address,
                PhoneNumber = createDepartment.PhoneNumber,
                Email = createDepartment.Email,
            };

            await _departmentCollection
                    .InsertOneAsync(dep);

            return CreatedAtAction("GetDepartment", new { Id = 1 }, dep);
        }

        // POST: api/Department/ID/Educators
        [HttpPost("{id}/Educators")]
        public async Task<ActionResult> PostEducatorToDepartment(string id, Educator createEducator)
        {
            if (!ObjectId.TryParse(id, out _))
                return NotFound("Incorrect id");

            var dep = _departmentCollection
                    .Find(x => x.Id == id)
                    .FirstOrDefault();

            if (dep == null)
                return NotFound();

            var filter = Builders<Department>.Filter.Eq(x => x.Id, id);
            var update = Builders<Department>.Update.Push<Educator>(x => x.Educators, createEducator);
            await _departmentCollection.FindOneAndUpdateAsync(filter, update);

            return Ok();
        }

        // DELETE : api/Department/ID/Educators/ID
        [HttpDelete("{id}/Educators/{educatorId}")]
        public async Task<ActionResult> DeleteEducatorInDepartment(string id, int educatorId)
        {
            if (!ObjectId.TryParse(id, out _))
                return NotFound("Incorrect Department id");

            var dep = _departmentCollection
                    .Find(x => x.Id == id)
                    .FirstOrDefault();
            if (dep == null)
                return NotFound();

            var educator = dep.Educators?
                            .FirstOrDefault(x => x.Id == educatorId);

            if (educator == null)
                return NotFound("There is no educator with such id in this department");

            var filter = Builders<Department>.Filter.Eq(x => x.Id, id);
            var update = Builders<Department>.Update.Pull(x => x.Educators, educator);
            await _departmentCollection.FindOneAndUpdateAsync(filter, update);

            return NoContent();
        }

        // DELETE : api/Department/ID
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDepartment(string id)
        {
            if (!ObjectId.TryParse(id, out _))
                return NotFound("Incorrect id");

            if (!isDepartmentExist(id))
                return NotFound();

            await _departmentCollection.DeleteOneAsync(x => x.Id == id);

             return NoContent();
        }

        private bool isDepartmentExist(string id)
        {
            return _departmentCollection.Find(x => x.Id == id).FirstOrDefault() != null;
        }
    }
}
