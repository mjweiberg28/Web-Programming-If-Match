using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Gargoyles.Entities;
using Gargoyles.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace Gargoyles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GargoylesController : Controller
    {
        private readonly GargoylesDatabase gargoylesDatabase;

        public GargoylesController(GargoylesDatabase gargoylesDatabase)
        {
            this.gargoylesDatabase = gargoylesDatabase;
        }

        [HttpGet]
        public IEnumerable<GargoyleEntity> Get()
        {
            var model = this.gargoylesDatabase.GetAll();

            return model;
        }

        [HttpGet("{index}")]
        public IActionResult Get(string index)
        {
            if (!gargoylesDatabase.Contains(index))
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }

            var model = this.gargoylesDatabase.Get(index);

            Response.Headers["ETag"] = model.ETag();

            return Json(new GargoyleEntity(model));
        }

        [HttpPost]
        public IActionResult Post([FromBody] GargoyleEntity gargoyleEntity)
        {
            if (this.gargoylesDatabase.Contains(gargoyleEntity.ToModel().Name))
            {
                return StatusCode((int)HttpStatusCode.Conflict);
            }

            gargoyleEntity.Updated = DateTime.UtcNow;

            this.gargoylesDatabase.AddOrReplace(gargoyleEntity.ToModel());

            return Json(gargoyleEntity);
        }

        [HttpPut("{index}")]
        public IActionResult Put(string index, [FromBody] GargoyleEntity gargoyleEntity)
        {
            if (!gargoylesDatabase.Contains(index))
            {
                gargoyleEntity.Updated = DateTime.UtcNow;

                this.gargoylesDatabase.AddOrReplace(gargoyleEntity.ToModel());

                return Json(gargoyleEntity);
            }

            if (!Request.Headers.TryGetValue("If-Match", out StringValues ifMatch))
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            var model = this.gargoylesDatabase.Get(gargoyleEntity.Name);

            if (model.ETag() != ifMatch || ifMatch.Equals("*"))
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            var updatedModel = this.gargoylesDatabase.Update(index, gargoyleEntity.ToModel());

            return Json(new GargoyleEntity(updatedModel));
        }

        [HttpPatch("{index}")]
        public IActionResult Patch(string index, [FromBody] GargoyleEntity gargoyleEntity)
        {
            if (!this.gargoylesDatabase.Contains(gargoyleEntity.ToModel().Name))
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }

            if (!Request.Headers.TryGetValue("If-Match", out StringValues ifMatch))
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            var model = this.gargoylesDatabase.Get(gargoyleEntity.Name);

            if (model.ETag() != ifMatch || ifMatch.Equals("*"))
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            var updatedModel = this.gargoylesDatabase.Update(index, gargoyleEntity.ToModel());

            return Json(new GargoyleEntity(updatedModel));
        }
    }
}