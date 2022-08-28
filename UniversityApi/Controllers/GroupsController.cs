using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace UniversityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Group>>> GetGroups(){
            return NotFound();
        }
    }
}
