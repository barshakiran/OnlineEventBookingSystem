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
        public List<EventDetailModel> GetEventDetails()
        {
           // return db.EventDetails

            List<EventDetailDomainModel> list = eventDetailBusiness.DisplayEventDetails();
            List<EventDetailModel> listViewModel;
            if (list != null)
            {
                listViewModel = new List<EventDetailModel>();
                var user = mapper.Map(list, listViewModel);
                return listViewModel;
            }
            else
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Data not found in the table")),
                    ReasonPhrase = "Data not found"
                };
                throw new HttpResponseException(response);
            }
        }

        [HttpPost]
        // POST: api/EventDetails
        [ResponseType(typeof(EventDetailModel))]
        public IHttpActionResult PostEventDetail(EventDetailModel eventDetail)
        {
            EventDetailDomainModel eventDetailDModel;
            if (!ModelState.IsValid)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(string.Format("Server error.")),
                    ReasonPhrase = "Server error."
                };
                throw new HttpResponseException(response);
               // return BadRequest(ModelState);
            }
            else
            {
                eventDetailDModel = new EventDetailDomainModel();
                mapper.Map(eventDetail, eventDetailDModel);
                eventDetailBusiness.AddEventDetails(eventDetailDModel);
                return Ok("Inserted");
            }
        }

        // GET: api/EventDetails/5
        public IHttpActionResult GetEventDetail(int id)
        {
            EventDetailModel eventDetailModel = new EventDetailModel();
            EventDetailDomainModel eventDetailDModel = eventDetailBusiness.DisplayEvent(id);
            if (id == 0)
            {
                return BadRequest();
            }
            if (eventDetailDModel == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Data not found for the id {}", id)),
                    ReasonPhrase = "Data not found"
                };
                throw new HttpResponseException(response);
            }
            mapper.Map(eventDetailDModel, eventDetailModel);
            return Ok(eventDetailModel);
        }

        [HttpPost]
        // PUT: api/EventDetails
        [ResponseType(typeof(EventDetailModel))]
        public IHttpActionResult UpdateEventDetail(EventDetailModel eventDetailModel)
        {
            EventDetailDomainModel eventDomainModel = new EventDetailDomainModel();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var check = eventDetailBusiness.DisplayEvent(eventDetailModel.Event_Id);

            if (check != null)
            {
                mapper.Map(eventDetailModel, eventDomainModel);
                eventDetailBusiness.UpdateEventDetails(eventDomainModel);
                return Ok("Updated");
            }

            else
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Data not found for the Id: {0}", eventDetailModel.Event_Id)),
                    ReasonPhrase = "Data not found"
                };

                throw new HttpResponseException(response);
                //return NotFound();
            }
        }


        //DELETE: api/EventDetails/5
        //[ResponseType(typeof(EventDetail))]
        [HttpPost]
        public IHttpActionResult Delete(int id)
        {
            var check = eventDetailBusiness.DisplayEvent(id);
            //bool isDeleted = false;
            if (check != null)
            {
                if (eventDetailBusiness.DeleteEvent(id) == true)
                {
                    return Ok(true);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Data not found for the Id: {0}", id)),
                    ReasonPhrase = "Data not found"
                };

                throw new HttpResponseException(response);
                //return BadRequest();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


    }
    }