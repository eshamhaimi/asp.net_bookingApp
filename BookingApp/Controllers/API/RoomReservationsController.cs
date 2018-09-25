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
    [RoutePrefix("roomReservation")]
    public class RoomReservationsController : ApiController
    {
        private BAContext db = new BAContext();

        [HttpGet]
        [Route("roomReservations", Name = "RoomResApi")]
        public IHttpActionResult GetRoomsReservations()
        {
            var l = this.db.RoomsReservations.ToList();
            return Ok(l);
        }

        [HttpGet]
        [Route("roomReservation/{id}")]
        [ResponseType(typeof(RoomReservations))]
        public IHttpActionResult GetRoomReservations(int id)
        {
            RoomReservations roomReservations = db.RoomsReservations.Find(id);
            if (roomReservations == null)
            {
                return NotFound();
            }

            return Ok(roomReservations);
        }

        [HttpPut]
        [Route("roomReservation/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRoomReservations(int id, RoomReservations roomReservations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != roomReservations.Id)
            {
                return BadRequest();
            }

            db.Entry(roomReservations).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomReservationsExists(id))
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
        [Route("roomReservation")]
        [ResponseType(typeof(RoomReservations))]
        public IHttpActionResult PostRoomReservations(RoomReservations roomReservations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RoomsReservations.Add(roomReservations);
            db.SaveChanges();

            return CreatedAtRoute("RoomResApi", new { id = roomReservations.Id }, roomReservations);
        }

        [HttpDelete]
        [Route("roomReservation/{id}")]
        [ResponseType(typeof(RoomReservations))]
        public IHttpActionResult DeleteRoomReservations(int id)
        {
            RoomReservations roomReservations = db.RoomsReservations.Find(id);
            if (roomReservations == null)
            {
                return NotFound();
            }

            db.RoomsReservations.Remove(roomReservations);
            db.SaveChanges();

            return Ok(roomReservations);
        }

        [HttpGet]
        [Route("roomReservationForRoom/{id}")]
        [ResponseType(typeof(RoomReservations))]
        public IHttpActionResult GetRoomReservationsForRoom(int id)
        {
            List<RoomReservations> roomReservations = new List<RoomReservations>();
            try
            {
                Room room = db.Rooms.Find(id);
                roomReservations = room.RoomReservations.ToList();
            }
            catch (Exception)
            {

                return NotFound();
            }
            
            if (roomReservations == null)
            {
                return NotFound();
            }

            return Ok(roomReservations);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoomReservationsExists(int id)
        {
            return db.RoomsReservations.Count(e => e.Id == id) > 0;
        }
    }
}