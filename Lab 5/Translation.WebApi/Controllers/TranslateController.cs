using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

using Translation.WebApi.Models;

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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetString(String input)
        {
            String[] split = input.Split(' ');

            if (split.Length <= 1)
            {
                return this.BadRequest();
            }

            return this.Ok(Translator.Translate(split));
        }
    }
}
