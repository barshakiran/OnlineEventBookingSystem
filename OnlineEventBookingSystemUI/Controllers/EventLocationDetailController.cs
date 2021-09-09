using OnlineEventBookingSystemUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OnlineEventBookingSystemUI.Controllers
{
    public class EventLocationDetailController : Controller
    {
        // GET: EventLocationDetail
        private string controller = "api/EventDetails";

        //public ActionResult Index()
        //{
        //    //GetEventTypes();
        //    return View();

        //}

        public void GetEventTypes()
        {
            List<string> eventTypes = new List<string>();
            foreach (var item in Enum.GetNames(typeof(EventTypes)))
            {
                eventTypes.Add(item);
            }

            if (ModelState.IsValid)
            {
                ViewBag.EventTypeList = eventTypes.Select(x => new SelectListItem()
                {
                    Text = x.ToString(),
                    Selected = Request["EventTypeList"] == x.ToString() ? true : false
                });

            }
            else
            {
                ModelState.AddModelError(string.Empty, "...Server Error. Please contact administrator.");

            }
        }

        public void  GetCities()
        {
            List<string> cityList = new List<string>();
            var response = OnlineEventBookingSystem.GlobalVariables.WebApiClient.GetAsync("api/UserEventDetails" + "/GetCityList").Result;
            if (response.IsSuccessStatusCode && ModelState.IsValid)
            {
                var cities = response.Content.ReadAsAsync<List<EventLocationViewModel>>().Result;

                // ViewBag.CityList = new SelectList(city, "Location_Id", "City");
                foreach(var city in cities)
                {
                    cityList.Add(city.City);
                }
                ViewBag.CityList = cityList.Select(x => new SelectListItem()
                {
                    Text = x.ToString(),
                    Selected = Request["CityList"] == x.ToString() ? true : false
                });
               // return city;
            }
            else
            {
                var statusCode = response.ReasonPhrase;
                ModelState.AddModelError(string.Empty, statusCode + "...Server Error. Please contact administrator.");
               // return null;
            }
        }

        public ActionResult AddLocationEvents()
        {

            return View(new EventDetailViewModel { });
        }
     
        public ActionResult EventLocationDetails(int? i)
        {
           GetCities();
            ViewBag.i = i;
            return PartialView(new EventLocationViewModel{ });
    

        }

        [HttpPost]
        public async Task<ActionResult> AddLocationEvents(EventDetailViewModel userEventDetailViewModel,List<EventLocationViewModel> objloc)
        {

            userEventDetailViewModel.Event_Type = Request["EventTypeList"];
            userEventDetailViewModel.EventList = objloc;


            //Set the Image File Path.
            userEventDetailViewModel.Event_Picture = "~/Images/" + userEventDetailViewModel.Event_Picture;

            var consume = await OnlineEventBookingSystem.GlobalVariables.WebApiClient.PostAsJsonAsync<EventDetailViewModel>(controller + "/PostEventDetail", userEventDetailViewModel);
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


        public ActionResult Create()
        {
            GetEventTypes();
            return View(new EventDetailViewModel { });
        }

        public ActionResult Index()
        {
            //GetEventTypes();
            //GetCities();
            List<EventDetailViewModel> listViewModel;
            var response = OnlineEventBookingSystem.GlobalVariables.WebApiClient.GetAsync(controller + "/GetEventDetails").Result;
            if (response.IsSuccessStatusCode && ModelState.IsValid)
            {
                listViewModel = response.Content.ReadAsAsync<List<EventDetailViewModel>>().Result;
                ViewBag.EventList = listViewModel;
                return View(listViewModel);
            }
            else
            {
                var statusCode = response.ReasonPhrase;
                ModelState.AddModelError(string.Empty, statusCode + "...Server Error. Please contact administrator.");
                return View();

            }
           // return View(new List<EventDetailViewModel> { });
        }
    }
}