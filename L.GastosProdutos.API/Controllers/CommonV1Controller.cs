using System.Net.Mime;

using Microsoft.AspNetCore.Mvc;

namespace L.GastosProdutos.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class CommonV1Controller : ControllerBase
    {
    }
}
