using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public String GetString(String input) => "You sent the string " + input;
    }
}
