using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HV_tech.Domain;
using Microsoft.AspNetCore.Mvc;

namespace HV_tech.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{value}")]
        public string Get(string value)
        {
            Order domainOrder = new Order(value);
            domainOrder.CreateOrder();
            return domainOrder.GetOrderText();
        }

        // POST api/values
        [HttpPost]
        public void Post(string values)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
