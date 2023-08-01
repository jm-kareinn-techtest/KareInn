using BeerDirectory.Core.Domain.Enumerations;
using Microsoft.AspNetCore.Mvc;

namespace beer_directory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StylesController : ControllerBase
    {
        public IEnumerable<Style> Get()
        {
            return Enumeration.GetAll<Style>();
        }
    }
}
