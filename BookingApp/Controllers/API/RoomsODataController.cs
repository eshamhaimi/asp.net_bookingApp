using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using BookingApp.Models;
using BookingApp.Models.AppModel;

namespace BookingApp.Controllers.Api
{
  
    public class RoomsODataController : ODataController
    {
        private BAContext db = new BAContext();

        // GET: odata/Rooms1
        [EnableQuery]
        public IQueryable<Room> GetRooms1()
        {
            return db.Rooms;
        }

        // GET: odata/Rooms1(5)
        [EnableQuery]
        public SingleResult<Room> GetRoom([FromODataUri] int key)
        {
            return SingleResult.Create(db.Rooms.Where(room => room.Id == key));
        }

        // PUT: odata/Rooms1(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Room> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Room room = db.Rooms.Find(key);
            if (room == null)
            {
                return NotFound();
            }

            patch.Put(room);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(room);
        }

        // POST: odata/Rooms1
        public IHttpActionResult Post(Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Rooms.Add(room);
            db.SaveChanges();

            return Created(room);
        }

        // PATCH: odata/Rooms1(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Room> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Room room = db.Rooms.Find(key);
            if (room == null)
            {
                return NotFound();
            }

            patch.Patch(room);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(room);
        }

        // DELETE: odata/Rooms1(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Room room = db.Rooms.Find(key);
            if (room == null)
            {
                return NotFound();
            }

            db.Rooms.Remove(room);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Rooms1(5)/Accommodation
        [EnableQuery]
        public SingleResult<Accommodation> GetAccommodation([FromODataUri] int key)
        {
            return SingleResult.Create(db.Rooms.Where(m => m.Id == key).Select(m => m.Accommodation));
        }

        // GET: odata/Rooms1(5)/RoomReservations
        [EnableQuery]
        public IQueryable<RoomReservations> GetRoomReservations([FromODataUri] int key)
        {
            return db.Rooms.Where(m => m.Id == key).SelectMany(m => m.RoomReservations);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoomExists(int key)
        {
            return db.Rooms.Count(e => e.Id == key) > 0;
        }
    }
}
