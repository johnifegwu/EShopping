using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Ordering.API.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version.apiversion}/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
    }
}
