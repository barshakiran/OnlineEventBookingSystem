using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineEventBookingSystem.Models;
using System.Net.Http;
using OnlineEventBookingSystem;
using System.Web.Security;
using System.Net;
using OnlineEventBookingSystemUI.Filters;
using System.Threading.Tasks;

namespace OnlineEventBookingSystem.Controllers
{
   
    public class UserController : Controller
    {
        private string controller = "api/UserDetails";
        private HttpResponseMessage response;

        public async Task<ActionResult> Index()
        {
            List<RegistrationViewModel> listViewModel;
            response = await GlobalVariables.WebApiClient.GetAsync(controller + "/GetUserDetails");
            if (response.IsSuccessStatusCode)
            {
                listViewModel = response.Content.ReadAsAsync<List<RegistrationViewModel>>().Result;
                ViewBag.UserList = listViewModel;
                return View();
            }
            else
            {
                var statusCode = response.ReasonPhrase;
                ModelState.AddModelError(string.Empty, statusCode + "...Server Error. Please contact administrator.");
                return View();

            }

        }  
 
        public ActionResult Registration()
        {

            return View(new RegistrationViewModel { });

        }       

        [HttpPost]
        public async Task<ActionResult> Registration(RegistrationViewModel model)
        {
            var consume =await GlobalVariables.WebApiClient.PostAsJsonAsync<RegistrationViewModel>("api/Login" + "/AddUserDetail", model);
            if (ModelState.IsValid)
            {
                if (consume.IsSuccessStatusCode)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User already exists.");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, consume.StatusCode + "...Server Error. Please contact administrator.");
                return View(model);
            }

        }
       
        public ActionResult Login()
        {

            return View(new LoginViewModel { });

        }      

        [HttpPost]        
        public async Task<ActionResult> Login(LoginViewModel loginViewModel)
        {
            var consume = await GlobalVariables.WebApiClient.PostAsJsonAsync<LoginViewModel>("api/Login" + "/UserLogin", loginViewModel);
            
            if (consume.IsSuccessStatusCode && ModelState.IsValid)
            {
                loginViewModel = consume.Content.ReadAsAsync<LoginViewModel>().Result;
                if (loginViewModel != null)
                {
                    FormsAuthentication.SetAuthCookie(loginViewModel.User_Name, false);
                   // return RedirectToAction("Index", "EventDetail");
                    if (loginViewModel.IsAdmin == true)
                    {
                        return RedirectToAction("Index", "EventDetail");

                    }
                    else
                    {
                        return RedirectToAction("Index", "UserEventDetail");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid User Name or Password");
                    return View(loginViewModel);
                }
            }
            else
            {
                var statusCode = consume.ReasonPhrase;
                ModelState.AddModelError(string.Empty, statusCode + ".Server Error. Please contact administrator.");
                return View(loginViewModel);

            }

        }

        // GET: UserDetails/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            RegistrationViewModel registrationVM = new RegistrationViewModel();
            if (id == 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var consume = await GlobalVariables.WebApiClient.GetAsync(controller + "/GetDetails/" + id) ;
            if (consume.IsSuccessStatusCode == false)
            {
                var statusCode = consume.ReasonPhrase;
                ModelState.AddModelError(string.Empty, statusCode + "...Server Error. Please contact administrator.");
                View(registrationVM);
            }
            registrationVM = consume.Content.ReadAsAsync<RegistrationViewModel>().Result;
            return View(registrationVM);
        }

        // POST: UserDetails/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(RegistrationViewModel userDetail)
        {
            var consume =await  GlobalVariables.WebApiClient.PutAsJsonAsync<RegistrationViewModel>(controller + "/UpdateUserDetail", userDetail);

                if (ModelState.IsValid && consume.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, consume.ReasonPhrase + "...Server Error. Please contact administrator.");
                    return View(userDetail);
                }
            
        }
        
        public async Task<ActionResult> Details(int id)
        {
            RegistrationViewModel registrationVM = new RegistrationViewModel();
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var consume = await GlobalVariables.WebApiClient.GetAsync(controller + "/GetDetails/" + id);
            if (consume.IsSuccessStatusCode == false)
            {
                var statusCode = consume.ReasonPhrase;
                ModelState.AddModelError(string.Empty, statusCode + "...Server Error. Please contact administrator.");
                View(registrationVM);

            }
            registrationVM = consume.Content.ReadAsAsync<RegistrationViewModel>().Result;
            return View(registrationVM);
        }

        // POST: UserDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var consume = await GlobalVariables.WebApiClient.DeleteAsync(controller + "/Delete/" + id);
            if (ModelState.IsValid)
            {
                if (consume.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, string.Empty);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, consume.ReasonPhrase +"Server Error. Please contact administrator.");
                return RedirectToAction("Index");
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            RegistrationViewModel registrationVM = new RegistrationViewModel();
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var consume = await GlobalVariables.WebApiClient.GetAsync(controller + "/GetDetails/" + id);           
            if (consume.IsSuccessStatusCode == false)
            {
                var statusCode = consume.ReasonPhrase;
                ModelState.AddModelError(string.Empty, statusCode + "...Server Error. Please contact administrator.");
                View(registrationVM);

            }
           
            registrationVM = consume.Content.ReadAsAsync<RegistrationViewModel>().Result;
            return View(registrationVM);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

    }
}