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

namespace BookingApp.Controllers.API
{
    [RoutePrefix("comment")]
    public class CommentsController : ApiController
    {
        private BAContext db = new BAContext();

        [HttpGet]
        [Route("comments", Name = "CommentApi")]
        public IHttpActionResult GetComments()
        {
            var l = this.db.Comments.ToList();
            return Ok(l);
        }

        [HttpGet]
        [Route("comment/{id}")]
        [ResponseType(typeof(Comment))]
        public IHttpActionResult GetComment(int id)
        {
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        [HttpPut]
        [Route("comment/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutComment(int id, Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != comment.Id)
            {
                return BadRequest();
            }

            db.Entry(comment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
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
        [Route("comment")]
        [ResponseType(typeof(Comment))]
        public IHttpActionResult PostComment(Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Comments.Add(comment);
            Accommodation accommodation = db.Accommodations.FirstOrDefault(x => x.Id == comment.Acc_Id);
            accommodation.AverageGrade = Math.Round(accommodation.Comments.Average(x => x.Grade), 1);
            db.Entry(accommodation).State = EntityState.Modified;
            db.SaveChanges();

            return CreatedAtRoute("CommentApi", new { id = comment.Id }, comment);
        }

        [HttpDelete]
        [Route("comment/{id}")]
        [ResponseType(typeof(Comment))]
        public IHttpActionResult DeleteComment(int id)
        {
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return NotFound();
            }

            db.Comments.Remove(comment);
            db.SaveChanges();

            return Ok(comment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CommentExists(int id)
        {
            return db.Comments.Count(e => e.Id == id) > 0;
        }
    }
}