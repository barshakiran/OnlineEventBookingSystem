using OnlineEventBookingSystem;
using OnlineEventBookingSystemUI.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Web.Mvc;
using System.Linq;
using System;
using System.Threading.Tasks;

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

        [HttpPost]
        public ActionResult DisplayUserEvents(UserEventDetailViewModel model)
        {
            List<EventDetailViewModel> eventDetailViewModels = new List<EventDetailViewModel>();

            if(model.Booking_Loc == "All")
            {
                model.Booking_Loc = String.Empty;
            }
            if (model.Event_Type == "All")
            {
                model.Event_Type = String.Empty;
            }
            var response = GlobalVariables.WebApiClient.PostAsJsonAsync(controller + "/GetUserEventsDetailList", model).Result;
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
            List<SelectListItem> cityList;
            var response = GlobalVariables.WebApiClient.GetAsync(controller + "/GetLocationDetailList").Result;
            if (response.IsSuccessStatusCode)
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
                return cityList;
            }
            else
            {
                var statusCode = response.ReasonPhrase;
                ModelState.AddModelError(string.Empty, statusCode + "...Server Error. Please contact administrator.");
                return null;
            }
        }

        public List<SelectListItem> PoputaleModeOfPayment()
        {
            List<SelectListItem> paymentModes;

            if (ModelState.IsValid)
            {

                paymentModes = new List<SelectListItem>();
                foreach (var item in Enum.GetNames(typeof(PaymentModes)))
                {
                    paymentModes.Add(new SelectListItem
                    {
                        Text = item,
                        Value = item
                    });
                }
                return paymentModes;

            }
            else
            {
                ModelState.AddModelError(string.Empty, "...Server Error. Please contact administrator.");
                return null;
            }
        }

        public ActionResult DisplayBookingEventDetails(int id,int locationId ,string  userName)
        {
            
            BookingDetailViewModel bookingDetailViewModel = new BookingDetailViewModel();
            
            if (id == 0 || locationId == 0 || string.IsNullOrEmpty(userName))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var consume = GlobalVariables.WebApiClient.GetAsync(controller + "/GetUserBookingEventDetail/" + id + "/" + locationId).Result;

            if (consume.IsSuccessStatusCode == false)
            {
                var statusCode = consume.ReasonPhrase;
                ModelState.AddModelError(string.Empty, statusCode + "...Server Error. Please contact administrator.");
                View(bookingDetailViewModel);
            }
            bookingDetailViewModel = consume.Content.ReadAsAsync<BookingDetailViewModel>().Result;
            bookingDetailViewModel.PaymentModeList = PoputaleModeOfPayment();
            bookingDetailViewModel.userName = userName;
            return View(bookingDetailViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> AddUserBookingEventDetail(BookingDetailViewModel bookingDetailViewModel)
        {
            int bookingId;
             var consume = await OnlineEventBookingSystem.GlobalVariables.WebApiClient.PostAsJsonAsync<EventDetailViewModel>(controller + "/PostUserBookingEventDetail", bookingDetailViewModel);
            var displayRecord = consume;
            if (consume.IsSuccessStatusCode)
            {
                bookingId = consume.Content.ReadAsAsync<int>().Result;
                if(bookingId == 0)
                {
                    UserEventDetailViewModel userEventDetailViewModel = new UserEventDetailViewModel();
                    userEventDetailViewModel.EventTypeList = PoputaleEventTypes();
                    userEventDetailViewModel.CityList = PopulateCityList();
                    ModelState.AddModelError(string.Empty, "Event already booked.");
                    return View("Index", userEventDetailViewModel);
                }
                return RedirectToAction("DisplayBookedEventDetails", new { @id = bookingId });
            }
            else
            {
                var errors = ModelState
                 .Where(x => x.Value.Errors.Count > 0)
                 .Select(x => new { x.Key, x.Value.Errors })
                 .ToArray();
                var statusCode = consume.ReasonPhrase;
                ModelState.AddModelError(string.Empty, statusCode + "...Server Error. Please contact administrator.");
                return View(bookingDetailViewModel);
            }
        }

        public ActionResult DisplayBookedEventDetails(int id)
        {

            BookingDetailViewModel bookingDetailViewModel = new BookingDetailViewModel();
            if (id == 0 )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var consume = GlobalVariables.WebApiClient.GetAsync(controller + "/GetUserBookedEventDetail/" + id).Result;

            if (consume.IsSuccessStatusCode == false)
            {
                var statusCode = consume.ReasonPhrase;
                ModelState.AddModelError(string.Empty, statusCode + "...Server Error. Please contact administrator.");
                View(bookingDetailViewModel);
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            bookingDetailViewModel = consume.Content.ReadAsAsync<BookingDetailViewModel>().Result;
            return View(bookingDetailViewModel);
        }

       
        public ActionResult DisplayUserBookedEventsList(string id)
        {
            List<BookingDetailViewModel> bookedEventDetailViewModels = new List<BookingDetailViewModel>();


            var response = GlobalVariables.WebApiClient.GetAsync(controller + "/GetUserBookedEventsDetailList/"+id).Result;
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

        public ActionResult Delete(int id)
        {
            BookingDetailViewModel bookedEventDetailViewModel = new BookingDetailViewModel();
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var consume = GlobalVariables.WebApiClient.GetAsync(controller + "/GetUserBookedEventDetail/" + id).Result;

            if (consume.IsSuccessStatusCode == false)
            {
                var statusCode = consume.ReasonPhrase;
                ModelState.AddModelError(string.Empty, statusCode + "...Server Error. Please contact administrator.");
                return RedirectToAction("DisplayUserBookedEventsList", new { @id = id });
            }
            var list = consume.Content.ReadAsAsync<BookingDetailViewModel>().Result;
            bookedEventDetailViewModel = list;
            return View(bookedEventDetailViewModel);
        }

        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id)
        {
            string name = User.Identity.Name;
            var consume = GlobalVariables.WebApiClient.PostAsJsonAsync(controller + "/Delete/" + id , id);
            if (ModelState.IsValid && consume.Result.IsSuccessStatusCode)
            {
                name=User.Identity.Name;
                return RedirectToAction("DisplayUserBookedEventsList",new { @id = name });
            }

            else
            {
                ModelState.AddModelError(string.Empty, consume.Result.StatusCode + "...Server Error. Please contact administrator.");
                return RedirectToAction("DisplayUserBookedEventsList", new { @id = name });
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

    }
       
}