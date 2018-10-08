using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using heroes_api.Models;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace heroes_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaractersController : BaseController
    {
        public CaractersController(IHttpClientFactory httpClientFactory, IConfiguration config) : base(httpClientFactory, config)
        {
        }

        [HttpGet]
        public ActionResult<IEnumerable<Caracter>> Get()
        {
            var client = this.GetClient($"/characters");
            var response = client.GetAsync(client.BaseAddress).Result;

            response.EnsureSuccessStatusCode();
            string conteudo =
                response.Content.ReadAsStringAsync().Result;
            var caracters = JsonConvert.DeserializeObject<List<Caracter>>(conteudo);
            return caracters;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Caracter> Get(int id)
        {
            var client = this.GetClient($"/character/" + id);
            var response = client.GetAsync(client.BaseAddress).Result;

            response.EnsureSuccessStatusCode();
            string conteudo =
                response.Content.ReadAsStringAsync().Result;
            var caracter = JsonConvert.DeserializeObject<Caracter>(conteudo);
            return caracter;
        }
    }
}