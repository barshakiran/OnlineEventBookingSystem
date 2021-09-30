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
            HttpResponseMessage consume = new HttpResponseMessage();
            if (eventDetailViewModel != null)
            {                
                eventDetailViewModel.EventList = objloc;
                eventDetailViewModel.Event_Picture = "~/Images/" + eventDetailViewModel.Event_Picture;
                consume = await OnlineEventBookingSystem.GlobalVariables.WebApiClient.PostAsJsonAsync<EventDetailViewModel>(controller + "/AddEventDetail", eventDetailViewModel);
            }
            
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
                return RedirectToAction("Create", eventDetailViewModel);
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            int locationId = 0;
            EventDetailViewModel eventDetailViewModel = new EventDetailViewModel();
            var consume = await GlobalVariables.WebApiClient.GetAsync(controller + "/GetEventDetail/" + id + "/" + locationId);

            if (consume.IsSuccessStatusCode == false)
            {
                var statusCode = consume.ReasonPhrase;
                ModelState.AddModelError(string.Empty, statusCode + "...Server Error. Please contact administrator.");
                return View(eventDetailViewModel);
            }
            else
            {
                eventDetailViewModel = consume.Content.ReadAsAsync<EventDetailViewModel>().Result;
                if (eventDetailViewModel != null)
                {
                    foreach (var eventList in eventDetailViewModel.EventList)
                    {
                        eventList.Cities = PopulateCityList();
                        foreach (var city in eventList.Cities)
                        {
                            if (city.Text.Equals(eventList.City))
                            {
                                city.Selected = true;
                            }
                        }
                        eventDetailViewModel.EventTypeList = PoputaleEventTypes();
                    }
                }
                else
                {
                    return HttpNotFound();
                }
                return View(eventDetailViewModel);
            }           
        }

        public async Task<ActionResult> Delete(int id, int locationId)
        {
            EventDetailViewModel eventDetailViewModel = new EventDetailViewModel();
            if (id == 0 || locationId ==0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, "No data found.");
            }
            var consume = await GlobalVariables.WebApiClient.GetAsync(controller + "/GetEventDetail/" + id + "/" + locationId);

            if (consume.IsSuccessStatusCode == false)
            {
                var statusCode = consume.ReasonPhrase;
                ModelState.AddModelError(string.Empty, statusCode + "...Server Error. Please contact administrator.");
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, "Date not found. Please contact administrator.");
            }           
            var eventDetailViewModelList = consume.Content.ReadAsAsync<EventDetailViewModel>().Result;
            if(eventDetailViewModelList == null)
            {
                return HttpNotFound();
            }
            else
            {
                eventDetailViewModel = eventDetailViewModelList;
                return View(eventDetailViewModel);
            }            
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id,int locationId)
        {
            var consume = await GlobalVariables.WebApiClient.DeleteAsync(controller + "/Delete/" + id + "/" + locationId);
            if (ModelState.IsValid && consume.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Unable to delete record. Please contact administrator.");
            }
        }
        [HttpPost]
        public async Task<ActionResult> UpdateLocationEvents(EventDetailViewModel eventDetailViewModel, List<EventLocationViewModel> objloc)
        {
            HttpResponseMessage consume = new HttpResponseMessage();
            if (eventDetailViewModel != null)
            {
                eventDetailViewModel.EventList = objloc;
                 consume = await GlobalVariables.WebApiClient.PutAsJsonAsync<EventDetailViewModel>(controller + "/UpdateEventDetail", eventDetailViewModel);
            }
           
            if (ModelState.IsValid && consume.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Unable to update. Please contact administrator.");
            }
        }

        public async Task<ActionResult> Index()
        {
            List<EventDetailViewModel> eventDetailViewModels;
           var response = await GlobalVariables.WebApiClient.GetAsync(controller + "/GetEventDetailList/");
            if (response.IsSuccessStatusCode && ModelState.IsValid)
            {
                eventDetailViewModels = response.Content.ReadAsAsync<List<EventDetailViewModel>>().Result;
                ViewBag.EventList = eventDetailViewModels;
                return View(eventDetailViewModels);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, "No data found. Please contact administrator.");
            }
        }

        public async Task<ActionResult> Details(int id, int locationId)
        {

            EventDetailViewModel eventDetailViewModel = new EventDetailViewModel();
            if (id == 0 || locationId == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, "No data found. Please enter valid event and location id");
            }
            var consume = await GlobalVariables.WebApiClient.GetAsync(controller + "/GetEventDetail/" + id + "/" + locationId) ;
            if (consume.IsSuccessStatusCode == false)
            {
                var statusCode = consume.ReasonPhrase;
                ModelState.AddModelError(string.Empty, statusCode + "...Server Error. Please contact administrator.");
                return View(eventDetailViewModel);
            }
            else
            {
                eventDetailViewModel = consume.Content.ReadAsAsync<EventDetailViewModel>().Result;
                if (eventDetailViewModel != null)
                {
                    return View(eventDetailViewModel);
                }
                else
                {
                    return HttpNotFound();
                }
            }            
        }

        public async Task<ActionResult> DisplayBookedEventsList()
        {
            List<BookingDetailViewModel> bookedEventDetailViewModels = new List<BookingDetailViewModel>();
            var response = await GlobalVariables.WebApiClient.GetAsync(controller + "/GetBookedEventsDetailList/") ;
            if (response.IsSuccessStatusCode)
            {
                bookedEventDetailViewModels = response.Content.ReadAsAsync<List<BookingDetailViewModel>>().Result;
                return View(bookedEventDetailViewModels);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, "No data found. Please contact administrator.");
            }
        }
    }
}