using OnlineEventBookingSystem;
using OnlineEventBookingSystemUI.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Web.Mvc;
using System;
using System.Linq;

namespace OnlineEventBookingSystemUI.Controllers
{
    public class EventDetailController_bak : Controller
    {

        private string controller = "api/EventDetails";

        public ActionResult AddEvents()
        {
            return View(new EventDetailViewModel { });
        }

        [HttpPost]
        public ActionResult AddEvents(EventDetailViewModel eventDetailViewModel)
        {
           // GetEventTypeEnumList();

            //Set the Image File Path.
            eventDetailViewModel.Event_Picture = "~/Images/" + eventDetailViewModel.Event_Picture;

            var consume = GlobalVariables.WebApiClient.PostAsJsonAsync<EventDetailViewModel>(controller + "/PostEventDetail", eventDetailViewModel);

            // var displayRecord = consume.Result;
            if (ModelState.IsValid && consume.Result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var statusCode = consume.Result.ReasonPhrase;
                ModelState.AddModelError(string.Empty, statusCode + "...Server Error. Please contact administrator.");
                return View(eventDetailViewModel);
            }
        }

        //GetEventDetails
        public ActionResult Index()
        {
            List<EventDetailViewModel> listViewModel;
            var response = GlobalVariables.WebApiClient.GetAsync(controller + "/GetEventDetails").Result;
            if (response.IsSuccessStatusCode && ModelState.IsValid)
            {
                listViewModel = response.Content.ReadAsAsync<List<EventDetailViewModel>>().Result;
                ViewBag.EventList = listViewModel;
                return View();
            }
            else
            {
                var statusCode = response.ReasonPhrase;
                ModelState.AddModelError(string.Empty, statusCode + "...Server Error. Please contact administrator.");
                return View();

            }
        }

        public ActionResult Details(int id)
        {
            EventDetailViewModel eventDetailViewModel = new EventDetailViewModel();
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var consume = GlobalVariables.WebApiClient.GetAsync(controller + "/GetEventDetail/" + id).Result;
            
            if (consume.IsSuccessStatusCode ==false)
            {
                var statusCode = consume.ReasonPhrase;
                ModelState.AddModelError(string.Empty, statusCode + "...Server Error. Please contact administrator.");
                return RedirectToAction("Index");

            }
            eventDetailViewModel = consume.Content.ReadAsAsync<EventDetailViewModel>().Result;
            return View(eventDetailViewModel);
        }

       // GET: EventDetails/Edit/5
        public ActionResult Edit(int id)
        {
            EventDetailViewModel eventDetailViewModel = new EventDetailViewModel();
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var consume = GlobalVariables.WebApiClient.GetAsync(controller + "/GetEventDetail/" + id).Result;

            if (consume.IsSuccessStatusCode == false)
            {
                var statusCode = consume.ReasonPhrase;
                ModelState.AddModelError(string.Empty, statusCode + "...Server Error. Please contact administrator.");
                View(eventDetailViewModel);

            }
            eventDetailViewModel = consume.Content.ReadAsAsync<EventDetailViewModel>().Result;
            return View(eventDetailViewModel);
        }

        // POST: EventDetails/Edit/5
        [HttpPost]
        public ActionResult Edit(EventDetailViewModel eventDetailViewModel)
        {
            var consume = GlobalVariables.WebApiClient.PostAsJsonAsync<EventDetailViewModel>(controller + "/UpdateEventDetail", eventDetailViewModel);
            // var displayRecord = consume.Result;

            if (consume.Result.IsSuccessStatusCode && ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, consume.Result.StatusCode + "...Server Error. Please contact administrator.");
                return View(eventDetailViewModel);
            }
        }



        // POST: EventDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var consume = GlobalVariables.WebApiClient.PostAsJsonAsync(controller + "/Delete/" + id, id);
            // var delConfirmed = consume.Result;


            if (ModelState.IsValid && consume.Result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            else
            {
                ModelState.AddModelError(string.Empty, consume.Result.StatusCode + "...Server Error. Please contact administrator.");
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int id)
        {
            EventDetailViewModel eventDetailViewModel = new EventDetailViewModel();
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var consume = GlobalVariables.WebApiClient.GetAsync(controller + "/GetEventDetail/" + id).Result;

            if (consume.IsSuccessStatusCode == false)
            {
                var statusCode = consume.ReasonPhrase;
                ModelState.AddModelError(string.Empty, statusCode + "...Server Error. Please contact administrator.");
                View(eventDetailViewModel);
            }
            eventDetailViewModel = consume.Content.ReadAsAsync<EventDetailViewModel>().Result;
            return View(eventDetailViewModel);
        }

      

       
    }
}