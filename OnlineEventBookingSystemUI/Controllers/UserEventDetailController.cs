using OnlineEventBookingSystem;
using OnlineEventBookingSystemUI.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Web.Mvc;
using System.Web.Helpers;
using System.Linq;
using System;

namespace OnlineEventBookingSystemUI.Controllers
{
    public class UserEventDetailController : Controller
    {
        private string controller = "api/UserEventDetails";
        // GET: UserEventDetail
        public ActionResult Index()
        {
            UserEventDetailViewModel userEventDetailViewModel = new UserEventDetailViewModel();
            userEventDetailViewModel.EventTypeList = PoputaleEventTypes();
            userEventDetailViewModel.CityList = PopulateCityList();
            return View(userEventDetailViewModel);
        }
        public List<SelectListItem> PoputaleEventTypes()
        {
            List<SelectListItem> eventTypes;

            if (ModelState.IsValid)
            {

                eventTypes = new List<SelectListItem>();
                foreach (var item in Enum.GetNames(typeof(EventTypes)))
                {
                    eventTypes.Add(new SelectListItem
                    {
                        Text = item,
                        Value = item
                    });
                }
                ViewBag.EventTypeList = eventTypes;
                return eventTypes;

            }
            else
            {
                ModelState.AddModelError(string.Empty, "...Server Error. Please contact administrator.");
                return null;
            }
        }

        public List<SelectListItem> PopulateCityList()
        {
            List<SelectListItem> cityList;
            var response = OnlineEventBookingSystem.GlobalVariables.WebApiClient.GetAsync(controller + "/GetLocationDetailList").Result;
            if (response.IsSuccessStatusCode && ModelState.IsValid)
            {
                var locationDetailList = response.Content.ReadAsAsync<List<LocationViewModel>>().Result;

                cityList = new List<SelectListItem>();
                foreach (var city in locationDetailList)
                {
                    cityList.Add(new SelectListItem
                    {
                        Text = city.City.ToString(),
                        Value = city.City.ToString()
                    });
                }
                ViewBag.CityList = cityList;
                return cityList;
            }
            else
            {
                var statusCode = response.ReasonPhrase;
                ModelState.AddModelError(string.Empty, statusCode + "...Server Error. Please contact administrator.");
                return null;
            }
        }


        public ActionResult DisplayUserEvents()
        {
            List<EventDetailViewModel> eventDetailViewModels = new List<EventDetailViewModel>();
            return PartialView(eventDetailViewModels);
        }


        [HttpPost]
        public ActionResult DisplayUserEvents(UserEventDetailViewModel model)
        {
            List<EventDetailViewModel> eventDetailViewModels = new List<EventDetailViewModel>();

            if(model.City == "All")
            {
                model.City = String.Empty;
            }
            if (model.Event_Type == "All")
            {
                model.Event_Type = String.Empty;
            }
            var response = GlobalVariables.WebApiClient.PostAsJsonAsync(controller + "/UserEventDetails", model).Result;
            if (response.IsSuccessStatusCode)
            {
                eventDetailViewModels = response.Content.ReadAsAsync<List<EventDetailViewModel>>().Result;
                return PartialView(@"~/Views/UserEventDetail/_DisplayUserEvents.cshtml", eventDetailViewModels);
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