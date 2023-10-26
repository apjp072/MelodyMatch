using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //will use UserController as the route, /api/users
    public class BaseApiController : ControllerBase
    {

    }
}



