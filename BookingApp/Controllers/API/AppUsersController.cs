using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BookingApp.Models.AppModel;
using BookingApp.Models;

namespace BookingApp.Controllers.API
{
    [RoutePrefix("appUser")]
    public class AppUsersController : ApiController
    {
        private BAContext db = new BAContext();

        [HttpGet]
        [Route("appUser", Name = "AppUserApi")]
        public IHttpActionResult GetAppUsers()
        {
            var l = this.db.AppUsers.ToList();
            return Ok(l);
        }

        [HttpGet]
        [Route("managers")]
        public IHttpActionResult GetManagers()
        {
            IList<AppUser> retList = new List<AppUser>();
            var l = this.db.AppUsers.ToList();
            foreach (var user in l)
            {
                if (user.Role == "Manager")
                {
                    retList.Add(user);
                }
            }

            return Ok(retList);
        }

        [HttpGet]
        [Route("appUsers/{id}")]
        [ResponseType(typeof(AppUser))]
        public IHttpActionResult GetAppUser(int id)
        {
            AppUser appUser = db.AppUsers.Find(id);

            if (appUser == null)
            {
                return NotFound();
            }

            return Ok(appUser);
        }

        [HttpGet]
        [Route("manager/{username}")]
        [ResponseType(typeof(AppUser))]
        public IHttpActionResult GetAppUser(string username)
        {
            AppUser appUser = db.AppUsers.FirstOrDefault(x=>x.Username==username);

            if (appUser == null)
            {
                return NotFound();
            }

            return Ok(appUser);
        }

        [HttpPut]
        [Route("appUser/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAppUser(int id, AppUser appUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != appUser.Id)
            {
                return BadRequest();
            }

            db.Entry(appUser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [Route("appUser")]
        [ResponseType(typeof(AppUser))]
        public IHttpActionResult PostAppUser(AppUser appUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AppUsers.Add(appUser);
            db.SaveChanges();

            return CreatedAtRoute("AppUserApi", new { id = appUser.Id }, appUser);
        }

        [HttpDelete]
        [Route("appUser/{id}")]
        [ResponseType(typeof(AppUser))]
        public IHttpActionResult DeleteAppUser(int id)
        {
            AppUser appUser = db.AppUsers.Find(id);
            if (appUser == null)
            {
                return NotFound();
            }

            db.AppUsers.Remove(appUser);
            db.SaveChanges();

            return Ok(appUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AppUserExists(int id)
        {
            return db.AppUsers.Count(e => e.Id == id) > 0;
        }
    }
}