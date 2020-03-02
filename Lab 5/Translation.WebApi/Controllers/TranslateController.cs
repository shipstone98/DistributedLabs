using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Translation.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TranslateController : ControllerBase
    {
        [HttpGet]
        public String Get() => "Hello World";
        
        [HttpGet("{id}", Name = "get")]
        public String Get(int id) => "Your number plus 100 is " + id;
    }
}
