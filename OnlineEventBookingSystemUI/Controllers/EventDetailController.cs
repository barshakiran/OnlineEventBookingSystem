using OnlineEventBookingSystemUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using OnlineEventBookingSystem;
using System.Web.Mvc;

namespace OnlineEventBookingSystemUI.Controllers
{
    public class EventDetailController : Controller
    {
        // GET: EventDetail
        private string controller = "api/EventDetails";

        public List<SelectListItem> PoputaleEventTypes()
        {
            List<SelectListItem> eventTypes;
                eventTypes = new List<SelectListItem>();
                foreach (var item in Enum.GetNames(typeof(EventTypes)))
                {
                    eventTypes.Add(new SelectListItem
                    {
                        Text = item,
                        Value = item
                    });
                }
                return eventTypes;
        }

        public List<SelectListItem> PopulateCityList()
        {
            List<SelectListItem> cityList ;
            var response = OnlineEventBookingSystem.GlobalVariables.WebApiClient.GetAsync(controller + "/GetLocationDetailList").Result;
            if (response.IsSuccessStatusCode )
            {
                var locationDetailList = response.Content.ReadAsAsync<List<LocationViewModel>>().Result;

                cityList = new List<SelectListItem>();
                foreach (var city in locationDetailList)
                {
                    cityList.Add(new SelectListItem
                    {
                        Text = city.City.ToString(),
                        Value = city.Location_Id.ToString()
                    });
                }
               return cityList;
            }
            else
            {
                var statusCode = response.ReasonPhrase;
                ModelState.AddModelError(string.Empty, statusCode + "...Server Error. Please contact administrator.");
                return null;
            }
        }

       
        public ActionResult Create()
        {
            EventDetailViewModel eventDetailViewModel = new EventDetailViewModel();
            eventDetailViewModel.EventTypeList = PoputaleEventTypes();
            EventLocationViewModel eventLocationViewModel = new EventLocationViewModel();
            eventLocationViewModel.Cities = PopulateCityList();
            eventDetailViewModel.EventList.Add(eventLocationViewModel);
            return View(eventDetailViewModel);
        }


        [HttpPost]
        public async Task<ActionResult> AddLocationEvents(EventDetailViewModel eventDetailViewModel,List<EventLocationViewModel> objloc)
        {

            if (!ModelState.IsValid)
            {
                var errors = ModelState
                                 .Where(x => x.Value.Errors.Count > 0)
                                 .Select(x => new { x.Key, x.Value.Errors })
                                 .ToArray();
               
                ModelState.AddModelError(string.Empty,"...Server Error. Please contact administrator.");
                return RedirectToAction("Create", eventDetailViewModel);
            }
            eventDetailViewModel.EventList = objloc;
            eventDetailViewModel.Event_Picture = "~/Images/" + eventDetailViewModel.Event_Picture;
            var consume = await OnlineEventBookingSystem.GlobalVariables.WebApiClient.PostAsJsonAsync<EventDetailViewModel>(controller + "/PostEventDetail", eventDetailViewModel);
            var displayRecord = consume;
            if (ModelState.IsValid && consume.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var errors = ModelState
                 .Where(x => x.Value.Errors.Count > 0)
                 .Select(x => new { x.Key, x.Value.Errors })
                 .ToArray();
                var statusCode = consume.ReasonPhrase;
                ModelState.AddModelError(string.Empty, statusCode + "...Server Error. Please contact administrator.");
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            int locationId = 0;
            EventDetailViewModel eventDetailViewModel = new EventDetailViewModel();

            var consume = GlobalVariables.WebApiClient.GetAsync(controller + "/GetEventDetail/" + id + "/" + locationId).Result;

            if (consume.IsSuccessStatusCode == false)
            {
                var statusCode = consume.ReasonPhrase;
                ModelState.AddModelError(string.Empty, statusCode + "...Server Error. Please contact administrator.");
                View(eventDetailViewModel);

            }
            var list = consume.Content.ReadAsAsync<EventDetailViewModel>().Result;
            eventDetailViewModel = list;
            foreach (var eventList in eventDetailViewModel.EventList)
            {
                eventList.Cities = PopulateCityList();
                foreach(var city in eventList.Cities)
                {
                    if(city.Text.Equals(eventList.City))
                    {
                        city.Selected = true;

                    }
                }
            }
            
            eventDetailViewModel.EventTypeList = PoputaleEventTypes();
            
            return View(eventDetailViewModel);
        }

        public ActionResult Delete(int id, int locationId)
        {
            EventDetailViewModel eventDetailViewModel = new EventDetailViewModel();
            if (id == 0 || locationId ==0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var consume = GlobalVariables.WebApiClient.GetAsync(controller + "/GetEventDetail/" + id + "/"+locationId).Result;

            if (consume.IsSuccessStatusCode == false)
            {
                var statusCode = consume.ReasonPhrase;
                ModelState.AddModelError(string.Empty, statusCode + "...Server Error. Please contact administrator.");
                View(eventDetailViewModel);
            }           
           var list = consume.Content.ReadAsAsync<EventDetailViewModel>().Result;
            eventDetailViewModel = list;
            return View(eventDetailViewModel);
        }

        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id,int locationId)
        {
            var consume = GlobalVariables.WebApiClient.PostAsJsonAsync(controller + "/Delete/" + id  + "/" + locationId, id & locationId);
            if (ModelState.IsValid && consume.Result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            else
            {
                ModelState.AddModelError(string.Empty, consume.Result.StatusCode + "...Server Error. Please contact administrator.");
               // return RedirectToAction("Delete" , new { id, locationId});
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<ActionResult> UpdateLocationEvents(EventDetailViewModel eventDetailViewModel, List<EventLocationViewModel> objloc)
        {
            eventDetailViewModel.EventList = objloc;
            var consume = await GlobalVariables.WebApiClient.PostAsJsonAsync<EventDetailViewModel>(controller + "/UpdateEventDetail", eventDetailViewModel);
            var displayRecord = consume;
            if (ModelState.IsValid && consume.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var errors = ModelState
                 .Where(x => x.Value.Errors.Count > 0)
                 .Select(x => new { x.Key, x.Value.Errors })
                 .ToArray();
                var statusCode = consume.ReasonPhrase;
                ModelState.AddModelError(string.Empty, statusCode + "...Server Error. Please contact administrator.");
                return View();
            }
        }
     
        public ActionResult Index()
        {
            List<EventDetailViewModel> eventDetailViewModels;
           var response = OnlineEventBookingSystem.GlobalVariables.WebApiClient.GetAsync(controller + "/GetEventDetailList/").Result;
            if (response.IsSuccessStatusCode && ModelState.IsValid)
            {
                eventDetailViewModels = response.Content.ReadAsAsync<List<EventDetailViewModel>>().Result;
                ViewBag.EventList = eventDetailViewModels;
                return View(eventDetailViewModels);
            }
            else
            {
                var statusCode = response.ReasonPhrase;
                ModelState.AddModelError(string.Empty, statusCode + "...Server Error. Please contact administrator.");
                return View();

            }
        }

        public ActionResult Details(int id, int locationId)
        {

            EventDetailViewModel eventDetailViewModel = new EventDetailViewModel();
            if (id == 0 || locationId == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var consume = GlobalVariables.WebApiClient.GetAsync(controller + "/GetEventDetail/" + id + "/" + locationId).Result;

            if (consume.IsSuccessStatusCode == false)
            {
                var statusCode = consume.ReasonPhrase;
                ModelState.AddModelError(string.Empty, statusCode + "...Server Error. Please contact administrator.");
                View(eventDetailViewModel);
            }
            var list = consume.Content.ReadAsAsync<EventDetailViewModel>().Result;
            eventDetailViewModel = list;
            return View(eventDetailViewModel);
        }

        public ActionResult DisplayBookedEventsList()
        {
            List<BookingDetailViewModel> bookedEventDetailViewModels = new List<BookingDetailViewModel>();
            var response = GlobalVariables.WebApiClient.GetAsync(controller + "/GetBookedEventsDetailList/").Result;
            if (response.IsSuccessStatusCode)
            {
                bookedEventDetailViewModels = response.Content.ReadAsAsync<List<BookingDetailViewModel>>().Result;
                return View(bookedEventDetailViewModels);
            }
            else
            {
                var statusCode = response.ReasonPhrase;
                ModelState.AddModelError(string.Empty, statusCode + "...Server Error. Please contact administrator.");
                return View();
            }
        }
    }
}