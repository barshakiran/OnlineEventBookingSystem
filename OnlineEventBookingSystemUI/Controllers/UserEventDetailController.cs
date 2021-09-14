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
        List<UserEventDetailViewModel> listViewModel;
        // GET: UserEventDetail
        public ActionResult Index()
        {
            PoputaleEventTypes();
            PopulateCityList();
            return View();
        }
        public ActionResult FetchEventDetails(UserEventDetailViewModel model)
        {
            model.City = Request["CityList"];
            model.Event_Type = Request["EventTypeList"];
            var response = GlobalVariables.WebApiClient.PostAsJsonAsync(controller + "/UserEventDetails", model).Result;
            if (response.IsSuccessStatusCode && ModelState.IsValid)
            {
                listViewModel = response.Content.ReadAsAsync<List<UserEventDetailViewModel>>().Result;
                PoputaleEventTypes();
                PopulateCityList();
                ViewBag.EventList = listViewModel;
                return View("Index");
            }
            else
            {
                var statusCode = response.ReasonPhrase;
                ModelState.AddModelError(string.Empty, statusCode + "...Server Error. Please contact administrator.");
                return View();

            }
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


        //public ActionResult AddLocationEvents()
        //{
        //    EventDetailController eventDetail = new EventDetailController();

        //    return View(new UserEventDetailViewModel { });
        //}



        //[HttpPost]
        //public ActionResult AddLocationEvents(UserEventDetailViewModel userEventDetailViewModel)
        //{
        //    EventDetailViewModel eventDetailViewModel =new EventDetailViewModel();

        //    //Set the Image File Path.
        //    eventDetailViewModel.Event_Picture = "~/Images/" + eventDetailViewModel.Event_Picture;

        //    var consume = GlobalVariables.WebApiClient.PostAsJsonAsync<UserEventDetailViewModel>(controller + "/PostEventDetail", eventDetailViewModel);

        //    // var displayRecord = consume.Result;
        //    if (ModelState.IsValid && consume.Result.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        var statusCode = consume.Result.ReasonPhrase;
        //        ModelState.AddModelError(string.Empty, statusCode + "...Server Error. Please contact administrator.");
        //        return View(eventDetailViewModel);
        //    }
        //}
    }
}