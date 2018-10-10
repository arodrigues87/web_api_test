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
            //var caracters = JsonConvert.DeserializeObject<List<Caracter>>(conteudo);

            dynamic resultado = JsonConvert.DeserializeObject(conteudo);
            List<Caracter> caracters = new List<Caracter>();

            if (resultado.data.results.Count > 0)
            {
                foreach (var data in resultado.data.results)
                {
                    var caracter = new Caracter()
                    {
                        Id = data.id,
                        Name = data.name,
                        Description = data.description,
                        UrlImage = data.thumbnail.path + "." + data.thumbnail.extension
                    };

                    caracters.Add(caracter);
                }
            }

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

            dynamic resultado = JsonConvert.DeserializeObject(conteudo);

            var caracter = new Caracter()
            {

            };
            return caracter;
        }
    }
}