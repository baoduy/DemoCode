using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ViewModels.Models;
using WebApi.Dal;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class ModelsController : ApiController
    {
        static ModelsController()
        {
            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<ViewModel, Model>()
                    .ForMember(m => m.CreatedBy, o => o.Ignore())
                    .ForMember(m => m.CreatedTime, o => o.Ignore())
                    .ForMember(m => m.UpdatedBy, o => o.Ignore())
                    .ForMember(m => m.UpdatedTime, o => o.Ignore());
                config.CreateMap<Model, ViewModel>();
            });
        }

        private DataContext db = new DataContext();

        protected T To<T>(object source) => AutoMapper.Mapper.Map<T>(source);

        // GET: api/Models
        public IEnumerable<ViewModel> GetModels()
        {
            return db.Models.ToList().Select(s => To<ViewModel>(s));
        }

        // GET: api/Models/5
        [ResponseType(typeof(ViewModel))]
        public async Task<IHttpActionResult> GetModel(int id)
        {
            var model = await db.Models.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            return Ok(To<ViewModel>(model));
        }

        // PUT: api/Models/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutModel(int id, ViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.Id)
            {
                return BadRequest();
            }

            var m = db.Models.Find(id);

            AutoMapper.Mapper.Map(model, m, typeof(ViewModel), typeof(Model));

            m.UpdatedBy = WindowsIdentity.GetCurrent().Name;
            m.UpdatedTime = DateTime.Now;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.OK);
        }

        // POST: api/Models
        [ResponseType(typeof(ViewModel))]
        public async Task<IHttpActionResult> PostModel(ViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var m = To<Model>(model);

            m.CreatedBy = WindowsIdentity.GetCurrent().Name;
            m.CreatedTime = DateTime.Now;

            db.Models.Add(m);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = model.Id }, model);
        }

        // DELETE: api/Models/5
        [ResponseType(typeof(ViewModel))]
        public async Task<IHttpActionResult> DeleteModel(int id)
        {
            var model = await db.Models.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            db.Models.Remove(model);
            await db.SaveChangesAsync();

            return Ok(To<ViewModel>(model));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ModelExists(int id)
        {
            return db.Models.Count(e => e.Id == id) > 0;
        }
    }
}