using OnlineEventBookingSystem;
using OnlineEventBookingSystemUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace OnlineEventBookingSystemUI.Controllers
{
    public class EventDetailController : Controller
    {

        private string controller = "api/EventDetails";

        // GET: EventDetail
        public ActionResult Index()
        {
            return View(new EventDetailViewModel { } );
        }


        [HttpPost]
        public ActionResult Index(EventDetailViewModel eventDetailViewModel)
        {
            //Set the Image File Path.
            eventDetailViewModel.Event_Picture = "~/Images/" + eventDetailViewModel.Event_Picture;
            var consume = GlobalVariables.WebApiClient.PostAsJsonAsync<EventDetailViewModel>(controller + "/PostEventDetail", eventDetailViewModel);

            var displayRecord = consume.Result;
            if (ModelState.IsValid)
            {


                if (displayRecord.IsSuccessStatusCode)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User already exists.");
                    return View(eventDetailViewModel);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                return View(eventDetailViewModel);
            }
        }
    }
}