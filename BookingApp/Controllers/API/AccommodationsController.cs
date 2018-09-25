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
using BookingApp.Models;
using BookingApp.Models.AppModel;
using System.Web;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http.Headers;
using BookingApp.Hubs;
using System.Web.Hosting;

namespace BookingApp.Controllers.API
{
    [RoutePrefix("acc")]
    public class AccommodationsController : ApiController
    {
        private BAContext db = new BAContext();
        public const string ServerUrl = "http://bookingserve.azurewebsites.net";
        public const int MaxImageSize = 1024 * 1024 * 6;

        [HttpGet]
        [Route("accs", Name = "AccApi")]
        public IHttpActionResult GetAccommodations()
        {
            var l = this.db.Accommodations.ToList();
            if (l != null)
            {
               /* foreach (Accommodation acc in l)
                {
                    if (acc.Comments.Count > 0)
                    acc.AverageGrade = Math.Round(acc.Comments.Average(x => x.Grade), 1);
                }*/
            }
            return Ok(l);
        }

        [HttpGet]
        [Route("accsmanager/{username}")]
        public IHttpActionResult GetAccommodations(string username)
        {
            AppUser user = this.db.AppUsers.FirstOrDefault(x => x.Username == username);
            var l = this.db.Accommodations.Where(x=>x.AppUser_Id==user.Id).ToList();
            if (l != null)
            {
                /* foreach (Accommodation acc in l)
                 {
                     if (acc.Comments.Count > 0)
                     acc.AverageGrade = Math.Round(acc.Comments.Average(x => x.Grade), 1);
                 }*/
            }
            return Ok(l);
        }

        [HttpGet]
        [Route("acc/{id}")]
        [ResponseType(typeof(Accommodation))]
        public IHttpActionResult GetAccommodation(int id)
        {
            Accommodation accommodation = db.Accommodations.Find(id);
            if (accommodation == null)
            {
                return NotFound();
            }

            return Ok(accommodation);
        }

        [HttpPut]
        [Route("acc/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAccommodation(int id, Accommodation accommodation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != accommodation.Id)
            {
                return BadRequest();
            }

            db.Entry(accommodation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccommodationExists(id))
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
        [Route("acc")]
        [ResponseType(typeof(Accommodation))]
        public IHttpActionResult PostAccommodation(Accommodation accommodation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Accommodations.Add(accommodation);
            db.SaveChanges();

            return CreatedAtRoute("AccApi", new { id = accommodation.Id }, accommodation);
        }

        [HttpGet]
        [Route("acc/image/{id}")]
        public string GetImage(int id)
        {
            Accommodation accommodation = this.db.Accommodations.FirstOrDefault(x => x.Id == id);
            if (accommodation.ImageURL == null)
            {
                return null;
            }

            var filePath = accommodation.ImageURL;
            var fullFilePath = HttpContext.Current.Server.MapPath("~/Content/BookingImages/" + Path.GetFileName(filePath));
            var relativePath = ServerUrl + "/Content/BookingImages/" + Path.GetFileName(filePath);

            if (File.Exists(fullFilePath))
            {
                return relativePath;
            }

            return null;
        }

        [HttpPost]
        [Route("image/upload")]
        [AllowAnonymous]
        public async Task<HttpResponseMessage> PostUserImage()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var fileCount = httpRequest.Files.Count;

                if (fileCount > 0)
                {
                    for (int i = 0; i < fileCount; i++)
                    {   
                        var postedFile = httpRequest.Files[i];
                        IList<string> AllowedExtension = new List<string> { ".jpg", ".gif", ".png" };
                        var fileExtension = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = fileExtension.ToLower();

                        if (!AllowedExtension.Contains(extension))
                        {
                            return Request.CreateResponse(HttpStatusCode.BadRequest, "Image extension is not valid.");
                        }
                        else if (postedFile.ContentLength > MaxImageSize)
                        {
                            return Request.CreateResponse(HttpStatusCode.BadRequest, "Image size is greater than one MB.");
                        }
                            //var filePath =Path.Combine(HttpRuntime.AppDomainAppPath, "/Content/BookingImages/"+ postedFile.FileName);
                        string filePath = HostingEnvironment.MapPath("~/Content/BookingImages/" + postedFile.FileName);
                        //    string filePath = Path.Combine(HttpRuntime.AppDomainAppPath, "email/teste.html");
                        postedFile.SaveAs(filePath);
                            return Request.CreateResponse(HttpStatusCode.Created, "Image successfuly posted.");
                    }
                }
                return Request.CreateResponse(HttpStatusCode.NotFound,"Something wrong.");
            }
            catch (HttpException e)
            {
                Console.WriteLine(e.ErrorCode);
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [HttpDelete]
        [Route("acc/{id}")]
        [ResponseType(typeof(Accommodation))]
        public IHttpActionResult DeleteAccommodation(int id)
        {
            Accommodation accommodation = db.Accommodations.Find(id);
            if (accommodation == null)
            {
                return NotFound();
            }

            db.Accommodations.Remove(accommodation);
            db.SaveChanges();

            return Ok(accommodation);
        }

        [AllowAnonymous]
        [HttpPut]
        [Route("approve/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult ApproveAccomodation(int id)
        {

            var accomodation = db.Accommodations.FirstOrDefault(x => x.Id == id);

            if (accomodation == null)
            {
                return this.NotFound();
            }

            accomodation.Approved = true;

            db.Entry(accomodation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();

                AppUser appUser = this.db.AppUsers.FirstOrDefault(x => x.Id == accomodation.AppUser_Id);
                if (appUser != null)
                {
                    NotificationHub.NotifyManager(accomodation.Name, appUser.Username);
                }

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccommodationExists(id))
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AccommodationExists(int id)
        {
            return db.Accommodations.Count(e => e.Id == id) > 0;
        }
    }
}