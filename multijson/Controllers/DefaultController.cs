using multijson.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace multijson.Controllers
{
    public class DefaultController : ApiController
    {
        List<string> strings = new List<string>()
        {
            "<test><option>1</option><option>2</option></test>",
            "<test><option>3</option><option>4</option></test>",
            "<test><option>5</option><option>6</option></test>",
        };

        public IHttpActionResult GetResult(int id)
        {
            var result = strings.ElementAtOrDefault(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
