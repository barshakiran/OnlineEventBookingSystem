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
using OnlineEventBookingSystemDAL;
using OnlineEventBookingSystemAPI.Models;
using OnlineEventBookingSystemBL.Interface;
using AutoMapper;
using OnlineEventBookingSystemDomain;
using OnlineEventBookingSystemBL;

namespace OnlineEventBookingSystemAPI.Controllers
{
    public class EventDetailsController : ApiController
    {
        private EventBookingSystemEntities1 db = new EventBookingSystemEntities1();
        private IEventDetailBusiness eventDetailBusiness;
        private MapperConfiguration config;
        private Mapper mapper;


        public EventDetailsController(IEventDetailBusiness _eventDetailBusiness)
        {
            eventDetailBusiness = _eventDetailBusiness;
            config = new MapperConfiguration(x => x.CreateMap<EventDetailModel, EventDetailDomainModel>().ReverseMap());
            mapper = new Mapper(config);
        }
        // GET: api/EventDetails
        public IQueryable<EventDetail> GetEventDetails()
        {
            return db.EventDetails;
        }

        // GET: api/EventDetails/5
        [ResponseType(typeof(EventDetail))]
        public IHttpActionResult GetEventDetail(int id)
        {
            EventDetail eventDetail = db.EventDetails.Find(id);
            if (eventDetail == null)
            {
                return NotFound();
            }

            return Ok(eventDetail);
        }

        // PUT: api/EventDetails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEventDetail(int id, EventDetail eventDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eventDetail.Event_Id)
            {
                return BadRequest();
            }

            db.Entry(eventDetail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventDetailExists(id))
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

        // POST: api/EventDetails
        [ResponseType(typeof(EventDetailModel))]
        public IHttpActionResult PostEventDetail(EventDetailModel eventDetail)
        {
            EventDetailDomainModel eventDetailDModel;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                eventDetailDModel = new EventDetailDomainModel();
                mapper.Map(eventDetail, eventDetailDModel);
                eventDetailBusiness.AddEventDetails(eventDetailDModel);
                return Ok("Inserted");
            }
        }
        // DELETE: api/EventDetails/5
        [ResponseType(typeof(EventDetail))]
        public IHttpActionResult DeleteEventDetail(int id)
        {
            EventDetail eventDetail = db.EventDetails.Find(id);
            if (eventDetail == null)
            {
                return NotFound();
            }

            db.EventDetails.Remove(eventDetail);
            db.SaveChanges();

            return Ok(eventDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EventDetailExists(int id)
        {
            return db.EventDetails.Count(e => e.Event_Id == id) > 0;
        }
    }
}