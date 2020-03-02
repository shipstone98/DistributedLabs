using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Translation.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TranslateController : ControllerBase
    {
        [HttpGet]
        public String Get() => "Hello World";

        [ActionName("GetInt")]
        [HttpGet("{id:int}", Name = "getInt")]
        public String Get(int id) => "Your number plus 100 is " + id;

        [HttpGet("{name}", Name = "getName")]
        public String GetName(String name) => "Your name is " + name;

        [HttpGet("{input}", Name = "getString")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetString(String input) => this.Ok("You sent the string " + input);
    }
}
