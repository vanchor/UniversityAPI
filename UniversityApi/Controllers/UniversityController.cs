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
    public class UniversitytController : ControllerBase
    {
        private readonly IMongoCollection<University> _univerityCollection;

        public UniversitytController(IOptions<UniversityMongoDbSettings> universityMongoDbSettings)
        {
            var mongoClient = new MongoClient(
                 universityMongoDbSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                universityMongoDbSettings.Value.DatabaseName);

            _univerityCollection = mongoDatabase.GetCollection<University>(
                universityMongoDbSettings.Value.UniversityCollectionName);
        }

        // GET: api/University
        [HttpGet]
        public ActionResult<IEnumerable<University>> GetUniversities()
        {
            return _univerityCollection
                    .Find(_ => true)
                    .ToList();
        }

        // GET: api/University/ID
        [HttpGet("{id}")]
        public async Task<ActionResult<University>> GetUniversity(string id)
        {
            if (!ObjectId.TryParse(id, out _))
                return NotFound("Incorrect id");

            var dep = await _univerityCollection
                    .Find(x => x.Id == id)
                    .FirstOrDefaultAsync();

            if (dep == null)
                return NotFound();

            return dep;
        }

        // PUT: api/University/ID
        [HttpPut("{id}")]
        public async Task<ActionResult> PutUniversity(string id, University updateUniversity)
        {
            if (!ObjectId.TryParse(id, out _))
                return NotFound("Incorrect id");
            if (id != updateUniversity.Id)
                return BadRequest("The id in the request does not match the id in the body.");

            await _univerityCollection
                    .ReplaceOneAsync(
                        x => x.Id == id,
                        updateUniversity);
            return NoContent();
        }

        // POST: api/University
        [HttpPost]
        public async Task<ActionResult> PostUniversity(University createUniversity)
        { 
            await _univerityCollection
                    .InsertOneAsync(createUniversity);

            return CreatedAtAction("GetUniversity", new { Id = 1 }, createUniversity);
        }

        // DELETE : api/University/ID
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUniversity(string id)
        {
            if (!ObjectId.TryParse(id, out _))
                return NotFound("Incorrect id");

            if (_univerityCollection.Find(x => x.Id == id).FirstOrDefault() == null)
                return NotFound();

            await _univerityCollection.DeleteOneAsync(x => x.Id == id);
            return NoContent();
        }
    }
}
