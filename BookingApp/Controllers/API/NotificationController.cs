using BookingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookingApp.Controllers.Api
{
    [Authorize]
    [RoutePrefix("notifications")]
    public class NotificationController : ApiController
    {
        private BAContext db = new BAContext();



        [HttpGet]
        [Route("subscribe")]
        public IHttpActionResult Get()
        {



            return this.Ok("Successfully subscribed!");
        }
        [HttpGet]
        [Route("subscribe/{username}/{role}")]
        public IHttpActionResult Subscribe(string username, string role)
        {

            return this.Ok("Successfully subscribed!");
        }
    }
}
