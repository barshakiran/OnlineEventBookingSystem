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
    [Authorize]
    public class UserEventDetailController : Controller
    {
        private string controller = "api/UserEventDetails";
        // GET: UserEventDetail
        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            UserEventDetailViewModel userEventDetailViewModel = new UserEventDetailViewModel();
            userEventDetailViewModel.EventTypeList = PoputaleEventTypes();
            userEventDetailViewModel.CityList = PopulateCityList();
            userEventDetailViewModel.Booking_Loc = String.Empty;
            userEventDetailViewModel.Event_Type = String.Empty;

            var response = await GlobalVariables.WebApiClient.PostAsJsonAsync(controller + "/GetUserEventsDetailList", userEventDetailViewModel);
            if (response.IsSuccessStatusCode)
            {
                userEventDetailViewModel.Events = response.Content.ReadAsAsync<List<EventDetailViewModel>>().Result;
                return View(userEventDetailViewModel);
            }
            else
            {
                var statusCode = response.ReasonPhrase;
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, statusCode+"No data found. Please contact administrator.");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> DisplayUserEvents(string Event_Type, string Booking_Loc)
        {
            List<EventDetailViewModel> eventDetailViewModels = new List<EventDetailViewModel>();
            UserEventDetailViewModel userEventDetailViewModel = new UserEventDetailViewModel();
            userEventDetailViewModel.Event_Type = Event_Type;
            userEventDetailViewModel.Booking_Loc = Booking_Loc;
            if (userEventDetailViewModel.Booking_Loc == "All")
            {
                userEventDetailViewModel.Booking_Loc = String.Empty;
            }
            if (userEventDetailViewModel.Event_Type == "All")
            {
                userEventDetailViewModel.Event_Type = String.Empty;
            }
            var response =await GlobalVariables.WebApiClient.PostAsJsonAsync(controller + "/GetUserEventsDetailList", userEventDetailViewModel);
            if (response.IsSuccessStatusCode)
            {
                eventDetailViewModels = response.Content.ReadAsAsync<List<EventDetailViewModel>>().Result;
                return PartialView(@"~/Views/UserEventDetail/_DisplayUserEvents.cshtml", eventDetailViewModels);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound,response.ReasonPhrase +"No data found. Please contact administrator.");
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

       
        public async Task<ActionResult> DisplayBookingEventDetails(int id,int locationId)
        {
            
            BookingDetailViewModel bookingDetailViewModel = new BookingDetailViewModel();
            
            if (id == 0 || locationId == 0 || string.IsNullOrEmpty(User.Identity.Name))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var consume = await GlobalVariables.WebApiClient.GetAsync(controller + "/GetUserBookingEventDetail/" + id + "/" + locationId);

            if (consume.IsSuccessStatusCode == false)
            {
                var statusCode = consume.ReasonPhrase;
                ModelState.AddModelError(string.Empty, statusCode + "...Server Error. Please contact administrator.");
                View(bookingDetailViewModel);
            }
            bookingDetailViewModel = consume.Content.ReadAsAsync<BookingDetailViewModel>().Result;
            bookingDetailViewModel.PaymentModeList = PoputaleModeOfPayment();
           bookingDetailViewModel.userName = User.Identity.Name;
            return View(bookingDetailViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> AddUserBookingEventDetail(BookingDetailViewModel bookingDetailViewModel)
        {
            int bookingId;
            bookingDetailViewModel.ErrorMessage = null;
            if (bookingDetailViewModel != null)
            {
                bookingDetailViewModel.userName = User.Identity.Name;
            }
             var consume = await OnlineEventBookingSystem.GlobalVariables.WebApiClient.PostAsJsonAsync<EventDetailViewModel>(controller + "/AddUserBookingEventDetail", bookingDetailViewModel);
            if (consume.IsSuccessStatusCode)
            {
                bookingId = consume.Content.ReadAsAsync<int>().Result;
                if(bookingId == 0)
                {
                    UserEventDetailViewModel userEventDetailViewModel = new UserEventDetailViewModel();
                    userEventDetailViewModel.EventTypeList = PoputaleEventTypes();
                    userEventDetailViewModel.CityList = PopulateCityList();
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "The Event already booked or event date is expired");
                }
                return RedirectToAction("DisplayBookedEventDetails", new { @id = bookingId });
            }
            else
            {
               
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Unable to book event. Please contact administrator.");
            }
        }

        public async Task<ActionResult> DisplayBookedEventDetails(int id)
        {

            BookingDetailViewModel bookingDetailViewModel = new BookingDetailViewModel();
            if (id == 0 )
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, "Please provide a valid booking id.");
            }
            var consume = await GlobalVariables.WebApiClient.GetAsync(controller + "/GetUserBookedEventDetail/" + id) ;

            if (consume.IsSuccessStatusCode == false)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, consume.ReasonPhrase+"No data found. Please contact administrator.");
            }
            else
            {
                bookingDetailViewModel = consume.Content.ReadAsAsync<BookingDetailViewModel>().Result;
                return View(bookingDetailViewModel);
            }
            
        }


        public async Task<ActionResult> DisplayUserBookedEventsList()
        {
            List<BookingDetailViewModel> bookedEventDetailViewModels = new List<BookingDetailViewModel>();

            string id = User.Identity.Name;
            var response = await GlobalVariables.WebApiClient.GetAsync(controller + "/GetUserBookedEventsDetailList/" + id) ;
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

        [HttpDelete, ActionName("Delete")]

        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            string name = User.Identity.Name;
            var consume = await GlobalVariables.WebApiClient.DeleteAsync(controller + "/Delete/" + id);
            if (ModelState.IsValid && consume.IsSuccessStatusCode)
            {
                name=User.Identity.Name;
                return RedirectToAction("DisplayUserBookedEventsList",new { @id = name });
            }

            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Unable to delete booking. Please contact administrator");
            }
        }       
    }
       
}