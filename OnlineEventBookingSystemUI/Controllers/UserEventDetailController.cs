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
            GetEventTypes();
            GetCities();
            return View();            
        }
        public ActionResult FetchEventDetails(UserEventDetailViewModel model)
        {
           model.City = Request["CityList"];
            model.Event_Type = Request["EventTypeList"];
            var response = GlobalVariables.WebApiClient.PostAsJsonAsync(controller+ "/UserEventDetails",model).Result;
            if (response.IsSuccessStatusCode && ModelState.IsValid)
            {
                listViewModel = response.Content.ReadAsAsync<List<UserEventDetailViewModel>>().Result;
                GetEventTypes();
                GetCities();
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


        public void GetCities()
        {
            List<string> city = new List<string>();
            var response = GlobalVariables.WebApiClient.GetAsync(controller + "/GetCityList").Result;
            if (response.IsSuccessStatusCode && ModelState.IsValid)
            {
                 city = response.Content.ReadAsAsync<List<string>>().Result;

                ViewBag.CityList = city.Select(x => new SelectListItem()
                {
                    Text = x.ToString(),
                    Selected = Request["CityList"]== x.ToString()?true:false
                }); 
            }
            else
            {
                var statusCode = response.ReasonPhrase;
                ModelState.AddModelError(string.Empty, statusCode + "...Server Error. Please contact administrator.");

            }
        }

        public void GetEventTypes()
        {
            List<string> eventTypes = new List<string>();
            foreach (var item in Enum.GetNames(typeof(EventTypes)))
            {
                eventTypes.Add(item);
            }
           
            if ( ModelState.IsValid)
            {
                // list.Select(x => new SelectListItem() { Value = x, Text = x }).ToList();
                ViewBag.EventTypeList = eventTypes.Select(x => new SelectListItem()
                {
                    Text = x.ToString(),
                    Selected = Request["EventTypeList"] == x.ToString() ? true : false
                });

            }
            else
            {
                ModelState.AddModelError(string.Empty ,"...Server Error. Please contact administrator.");

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